using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TransformSO camTransform;
    void Start()
    {
        camTransform.transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
