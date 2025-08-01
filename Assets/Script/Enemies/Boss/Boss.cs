using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour, IKillable
{
    private enum AttackCD
    {
        attackAttempt,
        meteor,
    }

    private int Phase = 0;
    private bool isShielded;
    [Header("Core")]
    [SerializeField]
    private TransformSO playerPos;
    [SerializeField]
    private TimerManager timerManager;

    [Header("General")]
    [SerializeField]
    private float AttackIntervalMax;
    [SerializeField]
    private float AttackIntervalMin;

    [Header("MeteorAttack")]
    [SerializeField]
    private float mDamage;
    [SerializeField]
    private GameObject meteor;
    [SerializeField]
    private float meteorInterval;
    [SerializeField]
    private int amountToSpawn;
    [SerializeField]
    private float spawnRadius;
    [SerializeField]
    private float spawnDelay;
    private Timer timers;
   
    public void KillEnemy()
    {
        if (isShielded)
            return;
        isShielded = true;
        Phase++;
        if(Phase > 3)
            Destroy(gameObject);
    }

    private void Awake()
    {
        timers = timerManager.GenerateTimers(typeof(AttackCD), gameObject);
        timers.SetTime((int)AttackCD.meteor, meteorInterval);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if(playerPos.transform == null)
            playerPos.transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        attemptMeteorAttack();
    }

    private void attemptMeteorAttack()
    {
        if (!timers.IsTimeZero((int)AttackCD.meteor))
            return;
        timers.ResetTime((int)AttackCD.meteor);
        for (int i = 0; i < amountToSpawn; i++)
        {
            float delay = Random.Range(0, spawnDelay);
            Invoke("SpawnEnemy", delay);
            
        }
    }

    private void SpawnEnemy()
    {
        Meteor m = Instantiate(meteor, new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)) + playerPos.transform.position, Quaternion.identity).GetComponent<Meteor>();
        Vector3 spawnPos = new Vector3(Random.Range(-1,1),25f,Random.Range(0,3));
        m.init(spawnPos,mDamage);
    }
}
