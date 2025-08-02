using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInside : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private LayerMask lineLayer;
    [SerializeField]
    private bool IsInside;
    [SerializeField]
    private CircleCollider2D col2D;
    private IKillable ai;
    void Start()
    {
        ai = GetComponentInParent<IKillable>();
    }

    // Update is called once per frame
    void Update()
    {
        col2D.offset = new Vector2(0,transform.position.z);
    }
    public void CheckDetection()
    {
        Vector2 pos = UtilityFunction.Vector3ToFlatVector2(transform.position);
        int hitAmount = 0;
        bool rayHit = true;
        while (rayHit)
        {
            RaycastHit2D col = Physics2D.Raycast(pos, Vector2.right, 1000f, lineLayer);
            if(col.collider != null)
            {
                hitAmount++;
                pos = col.point + Vector2.right;
            }
            else
            {
                rayHit = false;
            }
        }
        if (hitAmount % 2 != 0)
        {
            if (ai != null)
                ai.KillEnemy();
        }

    }
}
