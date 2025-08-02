using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonLaser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<LineRenderer> lineRenderer = new List<LineRenderer>();
    [SerializeField]
    private TransformSO bossPos;
    [SerializeField]
    private Transform hitPoint;
    void Start()
    {
        if(bossPos.transform != null)
        {
            
            foreach(LineRenderer lineRenderer in lineRenderer) 
            {
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, bossPos.transform.position);
                hitPoint.position = bossPos.transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
