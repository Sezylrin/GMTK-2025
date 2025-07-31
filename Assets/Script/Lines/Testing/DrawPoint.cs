using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject lineGen;
    [SerializeField]
    private BoolSO DrawLine;
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
            currentLine.DetectAllInternal(pos);
        }
    }

    public void StartGenerateLine()
    {
        if (currentLine == null && DrawLine.Bool)
            currentLine = Instantiate(lineGen, Vector3.zero, Quaternion.identity).GetComponent<GenerateLine>();
        else if (!DrawLine.Bool && currentLine)
        {
            currentLine.StartDecay();
            currentLine = null;
        }
        if (!currentLine)
            return;

        currentLine.SetVector(UtilityFunction.Vector3ToFlatVector2(lineDrawPoint.position));

    }
}
