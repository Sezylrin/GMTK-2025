using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public bool spawning = true;

    public float spawnDelay = 2;
    [SerializeField]
    private bool fixedDelay = true;
    [SerializeField]
    private float minDelay;
    [SerializeField]
    private float maxDelay;
    [SerializeField]
    private float minSpawnDist;
    [SerializeField]
    private TransformSO playerPos;

    public float innerRadius = 3;
    public float outerRadius = 10;

    public GameObject enemyPrefab;

    public TransformSO enemyParent;

    public LayerMask house;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator SpawnRoutine()
    {
        if(!fixedDelay)
            spawnDelay = Random.Range(minDelay, maxDelay);
        while (spawning)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemy();

        }

    }



    private void SpawnEnemy()
    {
        if(enemyParent.transform == null)
        {
            Debug.Break();
            Debug.LogWarning("No enemy parent transform SO in scene");
            return;
        }
        if (!playerPos)
        {
            Instantiate(enemyPrefab, PickSpawnSpot() + transform.position, Quaternion.identity, enemyParent.transform);
            return;
        }
        if(Vector3.Distance(transform.position,playerPos.transform.position) < minSpawnDist)
            Instantiate(enemyPrefab, PickSpawnSpot() + transform.position, Quaternion.identity, enemyParent.transform);


    }


    private Vector3 PickSpawnSpot()
    {

        bool isValid = false;
        Vector3 spawnPoint = Vector3.zero;
        while (!isValid)
        {
            float dist = Random.Range(innerRadius, outerRadius);

            float angle = Random.Range(0f, 360f);

            float addX = Mathf.Sin(angle) * dist;

            float addZ = Mathf.Cos(angle) * dist;
            spawnPoint = new Vector3(addX, 0, addZ);
            Collider[] col = new Collider[2];
            if (Physics.OverlapSphereNonAlloc(spawnPoint,5f, col, house) == 0)
            {
                isValid = true;
                spawnPoint.y = 10;
            }
        }



        return spawnPoint;

    }



}
