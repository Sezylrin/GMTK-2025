using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public TransformSO enemyParent;
    // Start is called before the first frame update
    void Start()
    {
        enemyParent.transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
