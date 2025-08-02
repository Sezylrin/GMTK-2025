using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public GameObject scaleParent;

    public GameObject Beam;

    public float windupTime = 1.5f;

    public float shockOffset = .15f;

    public GameObject ElecPart;

    public GameObject ExplPart;

    public GameObject ShockPart;

    public GameObject SmokePart;


    public FloatSO minRadius;

    public FloatSO maxRadius;

    public Vector2SO center;


    // Start is called before the first frame update
    void Awake()
    {

        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(200, 10);


        transform.position = UtilityFunction.Vector2ToFlatVector3(center.Vector2);


        ScaleExplosion(minRadius.Float);


        StartCoroutine(ExplosionRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator ExplosionRoutine()
    {


        Beam.transform.DOScale(new Vector3(0, 100, 1), windupTime).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(windupTime - shockOffset);

        ShockPart.SetActive(true);

        yield return new WaitForSeconds(shockOffset);

        Beam.SetActive(false);

        ExplPart.SetActive(true);

        ElecPart.SetActive(true);

        yield return new WaitForSeconds(6f);

        Destroy(gameObject);

    }



    private void ScaleExplosion(float radius)
    {
        scaleParent.transform.localScale = new Vector3(radius / 10, radius / 10, radius / 10);
    }



}
