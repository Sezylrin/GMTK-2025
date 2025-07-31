using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.MissingFeatures;
using Unity.Collections;
using KevinCastejon.MissingFeatures.MissingAttributes;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Acceleration")]
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float acceleration;

    [Header("Steering")]
    [SerializeField]
    private float turningForce;
    [SerializeField, Range(0, 1)]
    private float minSpeedTurnRate;
    [SerializeField, Range(-1, 3)]
    private float slippiness;

    [Header("Suspension")]
    [SerializeField]
    private float maxDist;
    [SerializeField]
    private float maxForce;
    [SerializeField]
    private List<Transform> suspensionPoint = new List<Transform>();
    [SerializeField]
    private Transform relativePos;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private LayerMask ground;

    private Vector3 groundNormal;

    #region Inputs
    private PlayerInputs playerInputs;
    private PlayerInputs.PlayerActions player;
    [SerializeField,ReadOnlyProp]
    private float throttle;
    private float steering;
    private void OnEnable()
    {
        player.Enable();
        player.Throttle.performed += SetThrottle;
        player.Throttle.canceled += SetThrottle;
        player.Steering.performed += SetSteering;
        player.Steering.canceled += SetSteering;
    }

    private void OnDisable()
    {
        player.Steering.performed -= SetSteering;
        player.Steering.canceled -= SetSteering;
        player.Throttle.canceled -= SetThrottle;
        player.Throttle.performed -= SetThrottle;
        player.Disable();
    }

    private void SetThrottle(InputAction.CallbackContext context)
    {
        throttle = context.ReadValue<float>();
    }
    private void SetSteering(InputAction.CallbackContext context)
    {
        steering = context.ReadValue<float>();
    }
    #endregion
    private void Awake()
    {
        playerInputs = new PlayerInputs();
        player = playerInputs.Player;
    }
    void Start()
    {
    }

    void FixedUpdate()
    {
        Steering();
        AddCounterCentrifugalForce();
        Throttle();
        CalculateSuspension();
    }

    private void CalculateSuspension()
    {
        foreach (Transform t in suspensionPoint)
        {
            Vector3 normal = Vector3.zero;
            if(Physics.Raycast(t.position, -t.up, out RaycastHit hit, maxDist, ground))
            {
                float compressRatio = Vector3.Distance(t.position, hit.point) / maxDist;
                float force =(1 - compressRatio) * maxForce;
                rb.AddForceAtPosition(force * t.up, t.position);
                normal = hit.normal;
            }
            groundNormal = normal;
        }
    }

    private void Throttle()
    {
        if (throttle == 0)
            return;
        if (groundNormal == Vector3.zero)
            return;
        Vector3 dir = rb.transform.forward * throttle;
        Vector3 normalized = 0.5f * (dir + rb.velocity.normalized);
        float accelerationMultiplier = (1 - (rb.velocity.magnitude / maxSpeed));
        if (normalized.magnitude < 0.5f)
            accelerationMultiplier = 1;
        Vector3 forceDir = Vector3.ProjectOnPlane(transform.forward - transform.up, groundNormal).normalized;
        rb.AddForceAtPosition(forceDir * acceleration * accelerationMultiplier * throttle,relativePos.position);
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
}
