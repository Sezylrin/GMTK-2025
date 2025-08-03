using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffect : MonoBehaviour
{
    [SerializeField]
    private FloatSO throttle;
    [SerializeField]
    private BoolSO IsNitro;
    [SerializeField]
    private FloatSO nitroAmount;
    [SerializeField]
    private ParticleSystem lightning;
    [SerializeField]
    private ParticleSystem trailA;
    [SerializeField]
    private ParticleSystem trailB;
    [SerializeField]
    private ParticleSystem[] boost;

    AudioObj lightningSound;
    void Start()
    {
        lightningSound = AudioManager.Instance.PlaySound(AudioRef.CarHum, transform, true, 0.15f);
        lightningSound.PauseSound();
    }

    // Update is called once per frame
    void Update()
    {
        Lightning();
        Smoke();
        Boost();
    }

    private void Lightning()
    {
        if (throttle.Float > 0 && !lightning.isPlaying)
        {
            lightning.Play();
        }
        else if (throttle.Float <= 0 && lightning.isPlaying)
        {
            lightning.Stop();
        }
        if(throttle.Float > 0 && (lightningSound.IsPaused() || lightningSound.IsPausing()))
        {
            lightningSound.ResumeSound(true,0.25f);
        }
        else if (throttle.Float <= 0 && (!lightningSound.IsPaused() || !lightningSound.IsPausing()))
        {
            lightningSound.PauseSound(true, 0.25f);
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

    private void Boost()
    {
        if(IsNitro.Bool && nitroAmount.Float > 0 && !boost[0].isEmitting)
        {
            foreach(var b in boost)
                b.Play();
        }
        else if ((!IsNitro.Bool || nitroAmount.Float ==0) && boost[0].isEmitting)
        {
            foreach (var b in boost)
                b.Stop();
        }
    }
}
