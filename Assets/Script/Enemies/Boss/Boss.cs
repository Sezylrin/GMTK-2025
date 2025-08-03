using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;


public class Boss : MonoBehaviour, IKillable
{
    private enum AttackCD
    {
        spawning,
        attackAttempt,
        meteor,
    }

    private int Phase = 0;
    private bool isShielded = false;
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
    [SerializeField]
    private float areaClearRadius;
    [SerializeField]
    private Transform model;

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
    private Transform shield;
    

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
            bossHum.StopSound(true,0.25f);
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
            shield.DOScale(Vector3.one, 3f).SetEase(Ease.OutCubic);
        }
    }

    private void CheckShield()
    {
        if(activeShields.Float <= 0 && isShielded)
        {
            shield.DOScale(Vector3.zero, 4f).SetEase(Ease.InElastic);
            isShielded = false;
            AudioManager.Instance.PlaySound(AudioRef.BossShieldBreak,false,0.5f);
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
        timers.SetTime((int)AttackCD.meteor, meteorInterval,false);
        activeShields.Float = 0;
        timers.SetTime((int)AttackCD.spawning, 1f, false);
        

    }
    private AudioObj bossHum;
    private void SpawningSequence()
    {
        AudioManager.Instance.PlaySound(AudioRef.BossMeteor);
        bossHum = AudioManager.Instance.PlaySound(AudioRef.BossHum,true,0.5f);
        Collider[] cols = Physics.OverlapSphere(transform.position, areaClearRadius, house);
        foreach (Collider col in cols)
        {
            col.GetComponentInParent<IKillable>().KillEnemy();
        }
        BossSpawned.Bool = true;
        timers.ResumeTimer((int)AttackCD.spawning);
        SpawnShield(1);
        timers.times[(int)AttackCD.spawning].OnTimeIsZero += StartAttacking;

    }

    private void StartAttacking(object sender, EventArgs e)
    {
        timers.ResumeTimer((int)AttackCD.meteor);
        timers.times[(int)AttackCD.spawning].OnTimeIsZero -= StartAttacking;
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
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        model.rotation = Quaternion.LookRotation(playerPos.transform.position - model.transform.position, Vector3.up);
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
