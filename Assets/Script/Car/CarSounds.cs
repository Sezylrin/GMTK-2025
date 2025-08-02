using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    // Start is called before the first frame update
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
    [SerializeField]
    private BoolSO win;
    [SerializeField]
    private BoolSO lose;
    void Start()
    {
        carStart = AudioManager.Instance.PlaySound(AudioRef.EngineStart, transform, false, 0.15f);
        carStart.OnIsComplete += StartIdle;
        carEngine = AudioManager.Instance.PlaySound(AudioRef.EngineDriving, transform, true, 0.1f);
        carEngine.PauseSound();
    }

    // Update is called once per frame
    void Update()
    {
        DetermineAudio();
        DeterminePitch();
        if(win.Bool || lose.Bool)
        {
            carIdle.StopSound(true, 0.2f);
            carIdle = null;
            carStart = null;
            carEngine.StopSound(true, 0.2f);
            carEngine = null;
            this.enabled = false;
        }
    }

    private void DetermineAudio()
    {
        if (!carIdle)
            return;
        if(isThrottleOn.Float == 0)
        {
            carIdle.FadeIn(0.1f, 0.2f, 1f);
        }
        else if (currentSpeed.Float <= 5)
        {
            carIdle.FadeIn(0.2f, 0.1f, 1f);
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
        carIdle = AudioManager.Instance.PlaySound(AudioRef.EngineIdle, transform, true, 0.5f);
        carIdle.FadeIn(0, 0.2f, 0.2f);
        carStart.OnIsComplete -= StartIdle;
    }
}
