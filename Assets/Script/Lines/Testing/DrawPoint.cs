using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2SO vector2SO;
    public BoxCollider2D boxCollider2D;
    public bool DrawLine;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if(DrawLine)
        vector2SO.Vector2 = new Vector2(transform.position.x, transform.position.z);
        DetectCollision();
    }

    public void DetectCollision()
    {
        Collider2D col = Physics2D.OverlapBox(UtilityFunction.Vector3ToFlatVector2(transform.position), Vector2.one, 0);
    }
}
