using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonLaser : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 endPoint;
    [SerializeField]
    private List<LineRenderer> lineRenderer = new List<LineRenderer>();
    [SerializeField]
    private TransformSO bossPos;
    [SerializeField]
    private LayerMask bossLayerMask;
    [SerializeField]
    private Transform hitPoint;
    void Start()
    {
        if(bossPos.transform != null)
        {
            if(Physics.Raycast(transform.position, bossPos.transform.position - transform.position, out RaycastHit hit, 1000f, bossLayerMask))
            {
                endPoint = hit.point;
                foreach(LineRenderer lineRenderer in lineRenderer) 
                {
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, endPoint);
                    hitPoint.position = endPoint;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
