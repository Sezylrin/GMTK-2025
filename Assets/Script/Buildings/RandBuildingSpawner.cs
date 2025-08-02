using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandBuildingSpawner : MonoBehaviour
{


    public GameObject flatPrefab;

    public GameObject bigHousePrefab;

    public GameObject smallHousePrefab;

    public GameObject factoryPrefab;





    // Start is called before the first frame update
    void Start()
    {

        int randVal = Random.Range(0, 4);

        int angleVal = 0;

        switch(randVal)
        {
            case 0:

                angleVal = Random.Range(0,4);

                Instantiate(flatPrefab, transform.position, Quaternion.Euler(0,angleVal * 90,0), transform.parent);

                break;
            case 1:

                angleVal = Random.Range(2, 4);


                Instantiate(bigHousePrefab, transform.position, Quaternion.Euler(0, angleVal * 90, 0), transform.parent);

                break;
            case 2:

                angleVal = Random.Range(1, 4);


                Instantiate(smallHousePrefab, transform.position, Quaternion.Euler(0, angleVal * 90, 0), transform.parent);

                break;
            default:

                angleVal = Random.Range(0, 4);


                Instantiate(factoryPrefab, transform.position, Quaternion.Euler(0, angleVal * 90, 0), transform.parent);

                break;
        }


        Destroy(this.gameObject);


    }

}
