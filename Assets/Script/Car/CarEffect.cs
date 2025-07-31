using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffect : MonoBehaviour
{
    [SerializeField]
    private BoolSO isDrawingLine;
    [SerializeField]
    private ParticleSystem lightning;
    [SerializeField]
    private FloatSO throttle;
    [SerializeField]
    private ParticleSystem trailA;
    [SerializeField]
    private ParticleSystem trailB;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Lightning();
        Smoke();
    }

    private void Lightning()
    {
        if (isDrawingLine.Bool && !lightning.isPlaying)
        {
            lightning.Play();
        }
        else if (!isDrawingLine.Bool && lightning.isPlaying)
        {
            lightning.Stop();
        }
    }

    private void Smoke()
    {
        if(throttle.Float != 0 && !trailA.isEmitting)
        {
            trailA.Play();
            trailB.Play();
        }
        else if (trailA.isEmitting && throttle.Float == 0)
        {
            trailA.Stop();
            trailB.Stop();
        }
    }
}
