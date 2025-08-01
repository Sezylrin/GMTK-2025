using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBuilding : MonoBehaviour, IKillable
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void KillEnemy()
    {
        Destroy(gameObject);
    }



}
