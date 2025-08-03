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
    private AudioObj nitroStart;
    private AudioObj nitroContinious;
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
    [SerializeField]
    private BoolSO IsNitros;
    [SerializeField]
    private FloatSO currentNitro;
    void Start()
    {
        carStart = AudioManager.Instance.PlaySound(AudioRef.EngineStart, transform, false, 0.15f);
        carStart.OnIsComplete += StartIdle;
        carEngine = AudioManager.Instance.PlaySound(AudioRef.EngineDriving, transform, true, 0.1f);
        carEngine.PauseSound();
        nitroStart = AudioManager.Instance.PlaySound(AudioRef.NitroStart, transform,false,0.35f);
        nitroStart.SetDontReturnAudio(true);
        nitroStart.StopSound();
        nitroStart.OnIsComplete += NitroSoundPlayed;

        nitroContinious = AudioManager.Instance.PlaySound(AudioRef.NitroContinue, transform, true, 0.35f);
        nitroContinious.PauseSound();
    }

    // Update is called once per frame
    void Update()
    {
        DetermineNitro();
        DetermineAudio();
        DeterminePitch();
        if(win.Bool || lose.Bool)
        {
            carIdle.StopSound(true, 0.2f);
            carIdle = null;
            carStart = null;
            carEngine.StopSound(true, 0.2f);
            carEngine = null;
            enabled = false;
        }
    }
    [SerializeField]
    private bool NitroPlayed = false;
    private void DetermineNitro()
    {
        if ((IsNitros.Bool && currentNitro.Float > 0) && !NitroPlayed)
        {
            nitroStart.PlaySound();
            NitroPlayed = true;
            nitroContinious.ResumeSound(true, 0.25f);
        }
        else if ((!IsNitros.Bool || currentNitro.Float == 0) )
        {
            if (nitroStart.IsSourcePlaying())
            {
                nitroStart.StopSound(true, 0.25f);
                NitroPlayed = false;
            }
            nitroContinious.PauseSound(true, 0.25f);
        }
    }
    private void NitroSoundPlayed(object sender, EventArgs e)
    {
        NitroPlayed = false;
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
