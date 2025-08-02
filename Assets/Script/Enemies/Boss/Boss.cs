using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Boss : MonoBehaviour, IKillable
{
    private enum AttackCD
    {
        attackAttempt,
        meteor,
    }

    private int Phase = 0;
    private bool isShielded = true;
    [Header("Core")]
    [SerializeField]
    private TransformSO playerPos;
    [SerializeField]
    private TimerManager timerManager;
    [SerializeField]
    private BoolSO triggerWin;
    [SerializeField]
    private LayerMask house;
    [SerializeField]
    private BoolSO BossSpawned;

    [Header("Spawning")]
    [SerializeField]
    private VisualEffect effect;
    [SerializeField]
    private Transform bossInitial;

    [Header("shield")]
    [SerializeField]
    private GameObject ShieldPylon;
    [SerializeField]
    private float shieldSpawnRadius;
    [SerializeField]
    private FloatSO activeShields;
    [SerializeField]
    private GameObject shield;
    

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
        if(Phase > 2)
        {
            BossSpawned.Bool = false;
            triggerWin.Bool = true;
            Destroy(gameObject);
            return;
        }
        SpawnShield(Phase + 1);
    }

    private void SpawnShield(int amount)
    {
        List<Vector3> spawnPoint = new List<Vector3>();
        while(activeShields.Float < amount)
        {
            Vector3 spawn = Vector3.forward * shieldSpawnRadius;
            spawn = Quaternion.Euler(0f, Random.Range(0, 360), 0f) * spawn;
            foreach (Vector3 p in spawnPoint)
            {
                if (Vector3.Distance(p,spawn) < shieldSpawnRadius)
                {
                    continue;
                }
            }
            activeShields.Float++;
            spawnPoint.Add(spawn);
            Instantiate(ShieldPylon, spawn + transform.position, Quaternion.identity);
            isShielded = true;
            shield.SetActive(true);
        }
    }

    private void CheckShield()
    {
        if(activeShields.Float <= 0 && isShielded)
        {
            shield.SetActive(false);
            isShielded = false;
        }
    }

    [ContextMenu("test")]
    private void test()
    {
        SpawnShield(3);
    }

    private void Awake()
    {
        timers = timerManager.GenerateTimers(typeof(AttackCD), gameObject);
        timers.SetTime((int)AttackCD.meteor, meteorInterval);
        activeShields.Float = 0;

        

    }

    private void SpawningSequence()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, shieldSpawnRadius + 15f, house);
        foreach (Collider col in cols)
        {
            col.GetComponentInParent<IKillable>().KillEnemy();
        }
        BossSpawned.Bool = true;
        SpawnShield(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        bossInitial.DOMove(transform.position, effect.GetFloat("Anticipation")).OnComplete(() => SpawningSequence() ).SetEase(Ease.InCirc);

        if(playerPos.transform == null)
            playerPos.transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        attemptMeteorAttack();
        CheckShield();
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
