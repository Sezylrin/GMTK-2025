using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioManager audioManager;
    private AudioObj carEngine;
    private AudioObj carIdle;
    private AudioObj carStart;
    [SerializeField]
    private FloatSO maxSpeed;
    [SerializeField]
    private FloatSO currentSpeed;
    [SerializeField]
    private FloatSO isThrottleOn;
    [SerializeField]
    private float maxPitch;
    [SerializeField]
    private float valueToScale;
    void Start()
    {
        carStart = audioManager.PlaySound(AudioRef.EngineStart, transform);
        carStart.OnIsComplete += StartIdle;
        carEngine = audioManager.PlaySound(AudioRef.EngineDriving, transform, true);
        carEngine.PauseSound();
    }

    // Update is called once per frame
    void Update()
    {
        DetermineAudio();
        DeterminePitch();
    }

    private void DetermineAudio()
    {
        if (!carIdle)
            return;
        if(isThrottleOn.Float == 0)
        {
            carIdle.FadeIn(0.44f, 0.8f, 1f);
        }
        else if (currentSpeed.Float <= 5)
        {
            carIdle.FadeIn(0.8f, 0.44f, 1f);
        }
        if(isThrottleOn.Float != 0)
        {
            if (resumeEngine && carEngine.IsPaused())
            {
                resumeEngine = false;
                carEngine.ResumeSound(true, 0.25f);
            }
        }
        else
        {
            if(!carEngine.IsPaused() || !carEngine.IsPausing())
            {
                carEngine.PauseSound(true, 0.25f);
                resumeEngine = true;
            }
        }
    }

    private void DeterminePitch()
    {
        float t = (currentSpeed.Float + valueToScale) / (maxSpeed.Float + valueToScale);
        float ease = MathF.Sqrt(1f - MathF.Pow(t - 1f, 2));
        float pitch = (maxPitch-1) * ease;
        pitch++;
        carEngine.ModifyPitch(pitch);
    }
    private bool resumeEngine = true;

    private void StartIdle(object sender, EventArgs e)
    {
        carIdle = audioManager.PlaySound(AudioRef.EngineIdle, transform, true, 1);
        carIdle.FadeIn(0, 1, 0.2f);
        carStart.OnIsComplete -= StartIdle;
    }
}
