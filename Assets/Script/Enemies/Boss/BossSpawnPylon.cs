using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnPylon : MonoBehaviour,IKillable
{
    // Start is called before the first frame update
    [SerializeField]
    private BossSpawner bossSpawner;

    public void KillEnemy()
    {
        bossSpawner.PylonDestoyed();
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
