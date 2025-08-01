using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBehavior : MonoBehaviour
{

    public GameObject enemyPrefab;

    public GameObject beamObj;

    public GameObject GFXScaler;

    public float speed = 4f;

    private int size;

    [SerializeField]
    private FloatSO minSpawn;

    private Vector3 targetPos = Vector3.zero;


    // Start is called before the first frame update
    void Awake()
    {
        size = Mathf.CeilToInt(Random.Range(minSpawn.Float, minSpawn.Float * 2f));

        transform.position = new Vector3(transform.position.x, transform.position.y * (size / 4f), transform.position.z);

        targetPos = transform.position;

        transform.position = transform.position + new Vector3(70 + transform.position.x, transform.position.y, transform.position.z);


        StartCoroutine(UFOFlyRoutine());

        GFXScaler.transform.localScale = new Vector3((float)size / 4f, (float)size / 4f, (float)size / 4f);

    }



    private IEnumerator UFOFlyRoutine()
    {
        AudioManager.Instance.PlaySound(AudioRef.UFOFlyIn, transform, false,0.75f);
        transform.DOMove(targetPos, speed).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(speed);

        // Pause for the spawning
        AudioManager.Instance.PlaySound(AudioRef.UFODeploy, transform);
        beamObj.SetActive(true);
        beamObj.transform.DOScale(new Vector3(5, 30, 5), .5f);
        yield return new WaitForSeconds(1);
        SpawnAliens();

        yield return new WaitForSeconds(.5f);
        beamObj.transform.DOScale(new Vector3(0, 30, 0), .5f);

        yield return new WaitForSeconds(.5f);
        AudioManager.Instance.PlaySound(AudioRef.UFOFlyOut, transform);
        transform.DOMove(new Vector3(-70 + transform.position.x, transform.position.y, transform.position.z), speed).SetEase(Ease.InQuart).OnComplete(() => AnimationDone());

    }




    // Update is called once per frame
    void Update()
    {


    }






    private void SpawnAliens()
    {
        for (int i = 0; i < size; i++)
        {
            Instantiate(enemyPrefab, new Vector3(transform.position.x + Random.Range(-1,1), 1, transform.position.z + Random.Range(-1, 1)), Quaternion.identity, GameObject.Find("Enemy Parent").transform);
        }
    }


    private void AnimationDone()
    {
        Destroy(gameObject);
    }



}
