using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GenerateLine : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Queue<Vector2> points = new Queue<Vector2>();
    [SerializeField]
    private Queue<Vector2> collisionPoint = new Queue<Vector2>();
    [SerializeField]
    private EdgeCollider2D edgeCollider;
    [SerializeField]
    private float collisionResolution;
    private float resolutionRatio;
    [SerializeField]
    private float lineResolution;
    private float lineResoRatio;
    [SerializeField]
    private float trailDuration;
    private int maxQueueSize;
    private int maxColSize;
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
    [Header("colour")]
    [SerializeField]
    [ColorUsage(true, true)]
    private Color bloomColor;
    private Color startingColor;
    private MaterialPropertyBlock block;
    


    private Timer eraseTimer;
    
    private Vector2 newPoint;

    private Vector2 lastAddedPoint = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {

        SetTime();
        resolutionRatio = 1 / collisionResolution;
        lineResoRatio = 1 / lineResolution;
        maxQueueSize = Mathf.CeilToInt(trailDuration * lineResolution);
        maxColSize = Mathf.CeilToInt(trailDuration * collisionResolution);
        nextLineCheck = Time.time;
        nextCollisionCheck = Time.time;
        startingColor = lineRenderer.material.GetColor("_Color");
        block = new MaterialPropertyBlock();
        
    }

    private void SetTime()
    {
        if(eraseTimer != null)
        {
            return;
        }
        eraseTimer = timerManager.GenerateTimers(1, gameObject);
        eraseTimer.SetTime(durationTillErase, false);
        eraseTimer.times[0].OnTimeIsZero += RemovePoint;
    }

    // Update is called once per frame
    private float nextCollisionCheck = 0;
    private float nextLineCheck = 0;
    void Update()
    {
        ShouldAddPoint();
        removeLine();
        ModifyBloom();
    }

    private void removeLine()
    {
        if(points.Count > maxQueueSize)
            points.Dequeue();
        if(collisionPoint.Count > maxColSize)
            collisionPoint.Dequeue();
    }
    private void ShouldAddPoint()
    {
        if (points.Count == 0)
            AddPoint();
        else if (lastAddedPoint != newPoint)
            AddPoint();
    }
    private void AddPoint()
    {
        if (nextLineCheck > Time.time)
            return;
        nextLineCheck += lineResoRatio;
        lastAddedPoint = newPoint;
        points.Enqueue(newPoint);
        if(nextCollisionCheck <= Time.time)
        {
            collisionPoint.Enqueue(newPoint);
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

    public void StartDecay(bool isBloom)
    {
        SetTime();
        eraseTimer.ResumeTimer();
        if(isBloom)
            DOVirtual.Color(startingColor, bloomColor, eraseTimer.GetTime(), (value) => { block.SetColor("_Color", value); });
        else
        {
            bloomColor.a = 0;
            DOVirtual.Color(startingColor, bloomColor, eraseTimer.GetTime(), (value) => { block.SetColor("_Color", value); });
        }
        edgeCollider.enabled = false;
    }

    private void ModifyBloom()
    {
        if (!eraseTimer.IsTimeZero() && !eraseTimer.IsPaused())
        {
            lineRenderer.SetPropertyBlock(block);
        }
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
