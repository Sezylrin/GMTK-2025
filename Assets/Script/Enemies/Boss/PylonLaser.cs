using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PylonLaser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<LineRenderer> lineRenderer = new List<LineRenderer>();
    [SerializeField]
    private TransformSO bossPos;
    [SerializeField]
    private Transform hitPoint;
    [SerializeField]
    private Transform visualPos;
    [SerializeField]
    private VisualEffect effect;
    [SerializeField]
    private Transform laserSpawnPoint;
    [SerializeField]
    private float areaClearRadius;
    [SerializeField]
    private LayerMask house;

    private Vector3 position;
    void Start()
    {
        visualPos.DOMove(transform.position, effect.GetFloat("Anticipation")).OnComplete(() => SpawningSequence()).SetEase(Ease.OutCubic);
    }

    private void SpawningSequence()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, areaClearRadius, house);
        foreach (Collider col in cols)
        {
            col.GetComponentInParent<IKillable>().KillEnemy();
        }
        foreach (LineRenderer lineRenderer in lineRenderer)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, laserSpawnPoint.position);
            if (bossPos && bossPos.transform)
            {
                DOVirtual.Vector3(laserSpawnPoint.position,bossPos.transform.position, 1f, (value) =>
                {
                    lineRenderer.SetPosition(1, value);
                    hitPoint.position = value;
                });
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
