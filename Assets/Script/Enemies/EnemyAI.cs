using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{


    private Rigidbody rb;

    public float speed = 5f;

    private float speedMod = 1;

    public TransformSO playerPos;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        speed = Random.Range(0.8f, 1.2f);



    }



    // Update is called once per frame
    void Update()
    {
        if (!playerPos.transform)
        {
            return;
        }

        transform.LookAt(playerPos.transform);

        
        rb.velocity = new Vector3(GetPlayerDir().x * GetTrueSpeed(), rb.velocity.y, GetPlayerDir().z * GetTrueSpeed());

    }





    private float GetTrueSpeed()
    {
        return speed * speedMod;
    }


    private Vector3 GetPlayerDir()
    {
        return (playerPos.transform.position - transform.position).normalized;
    }



    public void KillEnemy()
    {
        Destroy(this.gameObject);
    }




}
