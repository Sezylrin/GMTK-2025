using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLine : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private List<Vector2> points = new List<Vector2>();
    [SerializeField]
    private EdgeCollider2D edgeCollider;
    [SerializeField]
    private Vector2SO newPoint;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShouldAddPoint();
    }
    private void ShouldAddPoint()
    {
        if (points.Count == 0)
            AddPoint();
        else if (points[points.Count - 1] != newPoint.Vector2)
            AddPoint();
    }
    private void AddPoint()
    {        
        points.Add(newPoint.Vector2);
        edgeCollider.points = points.ToArray();
        List<Vector3> vec3 = new List<Vector3>();
        foreach (Vector2 v in points)
            vec3.Add(UtilityFunction.Vector2ToFlatVector3(v));
        lineRenderer.positionCount = vec3.Count;
        lineRenderer.SetPositions(vec3.ToArray());
    }


}
