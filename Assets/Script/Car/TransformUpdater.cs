using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUpdater : MonoBehaviour
{

    public TransformSO carPos;


    // Update is called once per frame
    void Update()
    {
        carPos.transform = transform;
    }




}
