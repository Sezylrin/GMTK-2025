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
    private List<Vector2> collisionPoint = new List<Vector2>();
    [SerializeField]
    private EdgeCollider2D edgeCollider;
    [SerializeField]
    private float collisionResolution;
    private float resolutionRatio;
    [Header("timer")]
    [SerializeField]
    private TimerManager timerManager;
    [SerializeField]
    private float durationTillErase;
    [SerializeField]
    private Vector2SO circleCentre;
    [SerializeField]
    private FloatSO minRadius;
    [SerializeField]
    private FloatSO maxRadius;
    [SerializeField]
    private BoolSO spawnExplosion;


    private Timer eraseTimer;
    
    private Vector2 newPoint;

    // Start is called before the first frame update
    void Start()
    {
        eraseTimer = timerManager.GenerateTimers(1, gameObject);
        eraseTimer.SetTime(durationTillErase,false);
        eraseTimer.times[0].OnTimeIsZero += RemovePoint;
        resolutionRatio = 1 / collisionResolution;
    }

    // Update is called once per frame
    private float nextCollisionCheck = 0;
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
        if(nextCollisionCheck <= Time.time)
        {
            collisionPoint.Add(newPoint);
            edgeCollider.points = collisionPoint.ToArray();
            nextCollisionCheck += resolutionRatio;
        }
        List<Vector3> vec3 = new List<Vector3>();
        foreach (Vector2 v in points)
            vec3.Add(UtilityFunction.Vector2ToVector3(v,0.2f));
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

    public void DetectAllInternal(Vector2 collisionPoint)
    {
        int pointPosition = 0;
        for(int i = edgeCollider.pointCount - 5; i > 0; i--)
        {
            float dist = Vector2.Distance(edgeCollider.ClosestPoint(collisionPoint), edgeCollider.points[i]);
            if(dist <= 1f)
            {
                pointPosition = i;
                break;
            }
        }
        List<Vector2> temp = new List<Vector2>();
        for(int i = pointPosition; i < edgeCollider.pointCount; i++)
        {
            temp.Add(edgeCollider.points[i]);
        }
        edgeCollider.points = temp.ToArray();
        Bounds bound = edgeCollider.bounds;
        circleCentre.Vector2 = bound.center;
        float min;
        float max;
        if(bound.size.x < bound.size.y)
        {
            min = bound.size.x * 0.5f;
            max = bound.size.y * 0.5f;
        }
        else
        {
            min = bound.size.y * 0.5f;
            max = bound.size.x * 0.5f;
        }
        minRadius.Float = min;
        maxRadius.Float = max;
        spawnExplosion.Bool = true;

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
