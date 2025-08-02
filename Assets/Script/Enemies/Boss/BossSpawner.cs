using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private int Pylons;
    [SerializeField]
    private float maxSpawnRange;
    [SerializeField]
    private float minSpawnRange;
    [SerializeField]
    private TransformSO playerPos;
    [SerializeField]
    private float areaClear;
    [SerializeField]
    private LayerMask house;

    [SerializeField]
    private FloatSO PylonCount;

    [SerializeField]
    private BoolSO BossSpawned;


    // Start is called before the first frame update
    void Start()
    {
        PylonCount.Float = Pylons;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PylonDestoyed()
    {
        Pylons--;
        PylonCount.Float = Pylons;
        if(Pylons == 0)
        {
            Vector3 spawn = PickSpawnSpot(minSpawnRange, maxSpawnRange);
            if (playerPos.transform)
                spawn += playerPos.transform.position;
            Instantiate(boss,spawn,Quaternion.identity);
            Collider[] cols = Physics.OverlapSphere(spawn, areaClear, house);
            foreach (Collider col in cols)
            {
                col.GetComponentInParent<IKillable>().KillEnemy();
            }
        }
    }

    private Vector3 PickSpawnSpot(float minRadius, float maxRadius)
    {

        float dist = Random.Range(minRadius, maxRadius);

        float angle = Random.Range(0f, 360f);

        float addX = Mathf.Sin(angle) * dist;

        float addZ = Mathf.Cos(angle) * dist;

        return new Vector3(addX, 0, addZ);

    }
}
