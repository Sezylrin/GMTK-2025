using System;
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

    [Header("timer")]
    [SerializeField]
    private TimerManager timerManager;
    [SerializeField]
    private float durationTillErase;

    private Timer eraseTimer;
    
    private Vector2 newPoint;

    // Start is called before the first frame update
    void Start()
    {
        eraseTimer = timerManager.GenerateTimers(1, gameObject);
        eraseTimer.SetTime(durationTillErase,false);
        eraseTimer.times[0].OnTimeIsZero += RemovePoint;
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
        else if (points[points.Count - 1] != newPoint)
            AddPoint();
    }
    private void AddPoint()
    {        
        points.Add(newPoint);
        edgeCollider.points = points.ToArray();
        List<Vector3> vec3 = new List<Vector3>();
        foreach (Vector2 v in points)
            vec3.Add(UtilityFunction.Vector2ToFlatVector3(v));
        lineRenderer.positionCount = vec3.Count;
        lineRenderer.SetPositions(vec3.ToArray());
    }

    private void RemovePoint(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    public void SetVector(Vector2 newPoint)
    {
        this.newPoint = newPoint;
    }

    public void StartDecay()
    {
        eraseTimer.ResumeTimer();
        edgeCollider.enabled = false;
    }

    public void DetectAllInternal()
    {
        Bounds bound = edgeCollider.bounds;
        Collider2D[] cols = Physics2D.OverlapBoxAll(bound.center, bound.size,0);
        foreach (Collider2D col in cols)
        {
            if(col.TryGetComponent(out DetectInside detected))
            {
                detected.CheckDetection();
            }
        }
    }
}
