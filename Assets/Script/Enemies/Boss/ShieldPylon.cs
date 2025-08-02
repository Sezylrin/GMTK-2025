using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPylon : MonoBehaviour, IKillable
{
    [SerializeField]
    private FloatSO shieldAmount;
    public void KillEnemy()
    {
        shieldAmount.Float--;
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    [SerializeField]

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
