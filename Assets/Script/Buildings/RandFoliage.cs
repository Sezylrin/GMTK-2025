using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandFoliage : MonoBehaviour
{

    public GameObject treePrefab;

    public GameObject pinePrefab;

    public GameObject bushPrefab;

    public GameObject rockPrefab;




    // Start is called before the first frame update
    void Start()
    {


        int randVal = Random.Range(0, 4);

        int angleVal = Random.Range(0, 360);


        Vector3 distanceVariation = new Vector3(Random.Range(-2.5f, 2.5f), 0, Random.Range(-2.5f, 2.5f));


        switch (randVal)
        {
            case 0:

                Instantiate(treePrefab, transform.position + distanceVariation, Quaternion.Euler(0, angleVal, 0), transform.parent);

                break;
            case 1:

                Instantiate(pinePrefab, transform.position + distanceVariation, Quaternion.Euler(0, angleVal, 0), transform.parent);

                break;
            case 2:

                Instantiate(bushPrefab, transform.position + distanceVariation, Quaternion.Euler(0, angleVal, 0), transform.parent);

                break;
            default:

                Instantiate(rockPrefab, transform.position + distanceVariation, Quaternion.Euler(0, angleVal, 0), transform.parent);

                break;
        }


        Destroy(this.gameObject);



    }




}
