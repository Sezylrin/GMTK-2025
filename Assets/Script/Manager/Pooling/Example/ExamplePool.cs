using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is the example class that spawns a poolable object
public class ExamplePool : MonoBehaviour
{
    //Must contain a gameobject reference for the object to spawn.
    //The object must have a script with the IPoolable interface on it.
    public GameObject objToSpawnForPool;
    //This is the instance of the pool generated locally on this object
    public Pool<ExampleObject> exampleObjectPool;
    void Start()
    {
        //This assigns a pool for this spawner to use
        PoolingManager.FindPool(objToSpawnForPool, out exampleObjectPool, "CustomName");
        // You may now spawn an object from the pool and instantly get access to the script with the IPoolable interface
        ExampleObject obj = exampleObjectPool.GetPooledObj();
        obj.HelloWorld();
    }
}
