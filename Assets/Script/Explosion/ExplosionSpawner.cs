using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{


    public BoolSO spawnExplosion;

    public GameObject explosionPrefab;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        SpawnExplosion();


    }


    private void SpawnExplosion()
    {
        if (spawnExplosion.Bool)
        {
            spawnExplosion.Bool = false;

            Instantiate(explosionPrefab);

        }
    }




}
