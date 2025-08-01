using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class DrawPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject lineGen;
    [SerializeField]
    private FloatSO throttle;
    [SerializeField]
    private LayerMask line;

    private GenerateLine currentLine;
    [SerializeField]
    private Transform collisionCheckPoint;
    [SerializeField]
    private Transform lineDrawPoint;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartGenerateLine();
        DetectCollision();
    }

    public void DetectCollision()
    {
        Vector2 pos = UtilityFunction.Vector3ToFlatVector2(collisionCheckPoint.position);
        Collider2D col = Physics2D.OverlapBox(pos, Vector2.one * 0.75f, transform.eulerAngles.y,line);

        if (col)
        {
            if (currentLine == null)
                return;
            currentLine.DetectAllInternal(pos);
            DecayLine();
        }
    }

    private void DecayLine()
    {
        currentLine.StartDecay();
        currentLine = null;
    }

    public void StartGenerateLine()
    {
        if (throttle.Float <= 0)
        {
            if (currentLine)
                DecayLine();
            return;
        }
        if (currentLine == null)
            currentLine = Instantiate(lineGen, Vector3.zero, Quaternion.identity).GetComponent<GenerateLine>();


        currentLine.SetVector(UtilityFunction.Vector3ToFlatVector2(lineDrawPoint.position));

    }

}


