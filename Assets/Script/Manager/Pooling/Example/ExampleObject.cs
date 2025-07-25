using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Poolable object must contain IPoolable interface declaring the script you wish to pool
public class ExampleObject : MonoBehaviour , IPoolable<ExampleObject>
{

    // Implement the Interface
    #region Pool Interface
    public Pool<ExampleObject> Pool { get; set; }
    public bool IsNewSpawn { get; set; }
    public bool IsPooled { get; set; }

    public void PoolSelf()
    {
        //This line is required to add the object back to the pool, you may have other functionality before this line
        Pool.PoolObj(this);
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    //An example Function
    public void HelloWorld()
    {
        Debug.Log("Hello World");
        Debug.Log("I will pool myself in 2 seconds");
        Invoke("PoolSelf", 2);
    }
}
