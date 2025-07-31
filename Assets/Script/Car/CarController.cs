using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.MissingFeatures;
using Unity.Collections;
using KevinCastejon.MissingFeatures.MissingAttributes;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Core")]
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private Transform COM;
    [SerializeField]
    private TimerManager timerManager;

    [Header("Acceleration")]
    [SerializeField]
    private Transform relativePos;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private FloatSO maxSpeedSO;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private FloatSO velocitySO;

    [Header("Steering")]
    [SerializeField]
    private float turningForce;
    [SerializeField, Range(0, 1)]
    private float minSpeedTurnRate;
    [SerializeField, Range(-1, 3)]
    private float slippiness;

    [Header("Nitros")]
    [SerializeField]
    private float nitrosMaxSpeed;
    [SerializeField]
    private float nitrosBoost;
    [SerializeField]
    private float nitrosMaxDuration;
    [SerializeField]
    private float nitroDrainRate;
    [SerializeField]
    private float nitrosRecoveryDelay;
    [SerializeField]
    private float nitrosRecoveryRate;
    [SerializeField]
    private FloatSO currentNitros;
    [SerializeField]
    private FloatSO maxNitros;
    [SerializeField]
    private Timer recoveryTimer;

    [Header("Suspension")]
    [SerializeField]
    private float maxDist;
    [SerializeField]
    private float maxForce;
    [SerializeField]
    private List<Transform> suspensionPoint = new List<Transform>();

    [Header("Debug")]
    [SerializeField, ReadOnlyProp]
    private bool isGrounded;

    private Vector3 projectedForward;

    private float drag;

    #region Inputs
    private PlayerInputs playerInputs;
    private PlayerInputs.PlayerActions player;
    [Header("Input")]
    [SerializeField]
    private FloatSO throttleSO;
    [SerializeField, ReadOnlyProp]
    private float throttle;
    [SerializeField, ReadOnlyProp]
    private float steering;
    [SerializeField, ReadOnlyProp]
    private bool isNitros;
    [SerializeField]
    private BoolSO drawLine;
    private void OnEnable()
    {
        player.Enable();
        player.Throttle.performed += SetThrottle;
        player.Throttle.canceled += SetThrottle;
        player.Steering.performed += SetSteering;
        player.Steering.canceled += SetSteering;
        player.Nitros.started += SetNitros;
        player.Nitros.canceled += SetNitros;
        player.DrawLine.started += SetDrawLine;
        player.DrawLine.canceled += SetDrawLine;
    }

    private void OnDisable()
    {
        player.Disable();
        player.Steering.performed -= SetSteering;
        player.Steering.canceled -= SetSteering;
        player.Throttle.canceled -= SetThrottle;
        player.Throttle.performed -= SetThrottle;
        player.Nitros.started -= SetNitros;
        player.Nitros.canceled -= SetNitros;
        player.DrawLine.started -= SetDrawLine;
        player.DrawLine.canceled -= SetDrawLine;
    }

    private void SetThrottle(InputAction.CallbackContext context)
    {
        throttle = context.ReadValue<float>();
        throttleSO.Float = throttle;
    }
    private void SetSteering(InputAction.CallbackContext context)
    {
        steering = context.ReadValue<float>();
    }

    private void SetDrawLine(InputAction.CallbackContext context)
    {
        drawLine.Bool = !drawLine.Bool;
    }
    private void SetNitros(InputAction.CallbackContext context)
    {
        isNitros = !isNitros;
        if (!isNitros)
        {
            turningForce *= 0.5f;
            recoveryTimer.ResumeTimer();
        }
        else
        {
            turningForce *= 2;
            recoveryTimer.ResetTime();
            recoveryTimer.PauseTimer();
        }
    }
    #endregion
    private void Awake()
    {
        playerInputs = new PlayerInputs();
        player = playerInputs.Player;
    }
    void Start()
    {
        drag = rb.drag;
        rb.centerOfMass = COM.transform.localPosition;
        maxNitros.Float = nitrosMaxDuration;
        currentNitros.Float = nitrosMaxDuration;
        recoveryTimer = timerManager.GenerateTimers(1, gameObject);
        recoveryTimer.SetTime(nitrosRecoveryDelay, false);
        maxSpeedSO.Float = maxSpeed;
    }

    void Update()
    {
        CalculateRemainingNitros();    
    }

    void FixedUpdate()
    {

        CalculateSuspension();
        CalculateGroundNormal();
        Steering();
        AddCounterCentrifugalForce();
        Throttle();
        ApplyNitros();
        ApplyDrag();
        velocitySO.Float = rb.velocity.magnitude;
    }

    private void ApplyDrag()
    {
        if (!isGrounded)
            rb.drag = 0;
        else
            rb.drag = drag;
    }

    private void CalculateSuspension()
    {
        foreach (Transform t in suspensionPoint)
        {
            bool grounded = false;
            if(Physics.Raycast(t.position, -t.up, out RaycastHit hit, maxDist, ground))
            {
                float compressRatio = Vector3.Distance(t.position, hit.point) / maxDist;
                float force =(1 - compressRatio) * maxForce;
                rb.AddForceAtPosition(force * t.up, t.position);
                grounded = true;
            }
            isGrounded = grounded;
        }
    }

    private void CalculateGroundNormal()
    {
        projectedForward = Vector3.zero;
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 10f, ground))
        {
            projectedForward = Vector3.ProjectOnPlane(transform.forward - transform.up, hit.normal).normalized;
        }
    }

    private void Throttle()
    {
        if (throttle == 0)
            return;
        if (!isGrounded)
            return;
        Vector3 dir = rb.transform.forward * throttle;
        Vector3 normalized = 0.5f * (dir + rb.velocity.normalized);
        float accelerationMultiplier = (1 - (rb.velocity.magnitude / maxSpeed));
        if (normalized.magnitude < 0.5f)
            accelerationMultiplier = 1;
        rb.AddForceAtPosition(projectedForward * acceleration * accelerationMultiplier * throttle,relativePos.position);
    }

    private void Steering()
    {
        if (rb.velocity.magnitude < maxSpeed * minSpeedTurnRate && throttle == 0)
            return;
        rb.AddTorque(Vector3.up * turningForce * steering);
    }

    private void AddCounterCentrifugalForce()
    {
        Vector3 proj = Vector3.Project(rb.velocity, transform.right);
        rb.AddForce(-proj * slippiness);
    }

    private void ApplyNitros()
    {
        if (!isNitros)
            return;
        if (currentNitros.Float == 0)
            return;
        Vector3 boostDir;
        if (isGrounded)
        {
            boostDir = projectedForward;
        }
        else
        {
            boostDir = transform.forward;
        }
        Vector3 dir = rb.transform.forward;
        Vector3 normalized = 0.5f * (dir + rb.velocity.normalized);
        float accelerationMultiplier = (1 - (rb.velocity.magnitude / nitrosMaxSpeed));
        if (normalized.magnitude < 0.5f)
            accelerationMultiplier = 1;
        rb.AddForceAtPosition(boostDir * nitrosBoost * accelerationMultiplier, relativePos.position);
    }

    private void CalculateRemainingNitros()
    {
        if (isNitros)
        {
            currentNitros.Float -= nitroDrainRate * Time.deltaTime;
            if(currentNitros.Float < 0)
                currentNitros.Float = 0;
        }
        else
        {
            if (recoveryTimer.IsTimeZero())
            {
                if(currentNitros.Float < maxNitros.Float)
                    currentNitros.Float += nitrosRecoveryRate * Time.deltaTime;
                if(currentNitros.Float > maxNitros.Float)
                    currentNitros.Float = maxNitros.Float;
            }
        }


    }
}
