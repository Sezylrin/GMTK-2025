using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IKillable
{


    private Rigidbody rb;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float acceleration;

    private float speed;

    private float speedMod = 1;

    public TransformSO playerPos;

    [SerializeField]
    private FloatSO healthToGive;
    [SerializeField]
    private float healthGain;
    [SerializeField]
    private FloatSO damageToDo;
    [SerializeField]
    private float damage;

    [Header("ragdoll")]
    [SerializeField]
    private float knockbackForce;
    [SerializeField]
    private float deathExplosionForce;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        speed = Random.Range(minSpeed, maxSpeed);



    }

    private void Start()
    {
        AttemptPlaySound();
    }

    private void AttemptPlaySound()
    {
        PlaySound();
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }
    private void PlaySound()
    {
        int chance = Random.Range(0, 100);
        if (chance > 70)
        {
            AudioManager.Instance.PlaySound(AudioRef.AlienGIbberish, transform,false,0.7f);
        }
        Invoke("AttemptPlaySound", Random.Range(1f, 6f));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerPos.transform)
        {
            return;
        }

        rb.transform.LookAt(playerPos.transform);
        Vector3 dir = rb.transform.forward;
        Vector3 normalized = 0.5f * (dir + rb.velocity.normalized);
        float accelerationMultiplier = (1 - (rb.velocity.magnitude / speed));
        if (normalized.magnitude < 0.5f)
            accelerationMultiplier = 1;
        rb.AddForce(dir * acceleration * accelerationMultiplier, ForceMode.Acceleration);


    }


    public void KillEnemy()
    {
        healthToGive.Float += healthGain;
        int chance = Random.Range(0, 100);
        if(chance > 65)
        {
            AudioManager.Instance.PlaySound(AudioRef.AlienDeath, transform);
        }
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Tags.T_Player))
        {
            damageToDo.Float += damage;
            rb.AddExplosionForce(knockbackForce, collision.transform.position, 5f, 1f, ForceMode.Acceleration);
        }
    }

}

public interface IKillable
{
    public void KillEnemy();
}
