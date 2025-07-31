using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBehavior : MonoBehaviour
{

    public GameObject enemyPrefab;

    public GameObject beamObj;

    public GameObject GFXScaler;

    public float speed = 2f;

    public int size = 3;



    private Vector3 targetPos = Vector3.zero;


    // Start is called before the first frame update
    void Awake()
    {
        size = Random.Range(1, 8);

        transform.position = new Vector3(transform.position.x, transform.position.y * (size / 4f), transform.position.z);

        targetPos = transform.position;

        transform.position = transform.position + new Vector3(70 + transform.position.x, transform.position.y, transform.position.z);


        StartCoroutine(UFOFlyRoutine());

        GFXScaler.transform.localScale = new Vector3((float)size / 4f, (float)size / 4f, (float)size / 4f);

    }



    private IEnumerator UFOFlyRoutine()
    {
        transform.DOMove(targetPos, speed * size).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(speed * size);

        // Pause for the spawning
        beamObj.SetActive(true);
        beamObj.transform.DOScale(new Vector3(5, 30, 5), .5f);
        yield return new WaitForSeconds(1);
        SpawnAliens();

        yield return new WaitForSeconds(.5f);
        beamObj.transform.DOScale(new Vector3(0, 30, 0), .5f);
        yield return new WaitForSeconds(.5f);


        transform.DOMove(new Vector3(-70 + transform.position.x, transform.position.y, transform.position.z), speed * size).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(speed * size);

        // destroy this UFO
        AnimationDone();
    }




    // Update is called once per frame
    void Update()
    {


    }






    private void SpawnAliens()
    {

        for (int i = 0; i < size; i++)
        {
            Instantiate(enemyPrefab, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity, GameObject.Find("Enemy Parent").transform);
        }

    }


    private void AnimationDone()
    {
        Destroy(this.gameObject);
    }



}
