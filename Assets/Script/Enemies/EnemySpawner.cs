using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public bool spawning = true;

    public float spawnDelay = 2;

    public float innerRadius = 3;
    public float outerRadius = 10;

    public GameObject enemyPrefab;

    public TransformSO enemyParent;




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
        Instantiate(enemyPrefab, PickSpawnSpot(), Quaternion.identity, enemyParent.transform);


    }


    private Vector3 PickSpawnSpot()
    {

        float dist = Random.Range(innerRadius, outerRadius);

        float angle = Random.Range(0f, 360f);

        float addX = Mathf.Sin(angle) * dist;

        float addZ = Mathf.Cos(angle) * dist;

        return new Vector3(addX, 10, addZ);

    }



}
