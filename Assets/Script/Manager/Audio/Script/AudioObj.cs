using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using System;
using KevinCastejon.MissingFeatures.MissingAttributes;

public class AudioObj : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private bool isPaused = false;
    private bool isPausing = false;
    private AudioManager manager;
    [SerializeField,ReadOnlyProp]
    private float initialVolume;
    private float transitionDist;
    private float minSpatial;
    private float maxSpatial;
    private bool is3D = false;
    private bool isStatic;

    public EventHandler OnIsComplete;
    void Start()
    {
    }
    public void Init(AudioManager manager)
    {
        this.manager = manager;
    }
    // Update is called once per frame
    void Update()
    {
        if(is3D && !isStatic)
        {
            CalculateSpatialBlend();
        }
        if (!source.isPlaying && isPaused == false && !source.loop)
        {
            OnIsComplete?.Invoke(this, EventArgs.Empty);
            OnComplete();
        }
    }

    private void CalculateSpatialBlend()
    {
        Vector3 pos = manager.listener.transform == null ? Vector3.zero : manager.listener.transform.position;
        float dist = Vector3.Distance(pos, transform.position);
        if (dist <= source.minDistance)
            source.spatialBlend = minSpatial;
        else if (dist >= transitionDist)
            source.spatialBlend = maxSpatial;
        else
        {
            float distRatio = (dist - source.minDistance)/(transitionDist - source.maxDistance);
            float spatial = (maxSpatial - minSpatial) * distRatio + minSpatial;
            source.spatialBlend = spatial;
        }
    }
    public void Set3DValues(float maxDist, float minDist, float transitionPoint, float maxSpatial, float minSpatial, AudioRolloffMode rolloff)
    {
        is3D = true;
        isStatic = false;
        source.maxDistance = maxDist;
        source.minDistance = minDist;
        source.rolloffMode = rolloff;
        transitionDist = transitionPoint;
        this.minSpatial = minSpatial;
        this.maxSpatial = maxSpatial;
    }
    public void Set3DValues(float maxDist, float mindist, float spatial, AudioRolloffMode rolloff)
    {
        is3D = true;
        isStatic = true;
        source.maxDistance = maxDist;
        source.minDistance = mindist;
        source.rolloffMode = rolloff;
        source.spatialBlend = spatial;
    }
    public void StartPlaying(AudioClip clipToPlay, AudioMixerGroup group, bool loop, float volume)
    {
        enabled = true;
        source.outputAudioMixerGroup = group;
        source.clip = clipToPlay;
        source.Play();
        source.loop = loop;
        source.volume = volume;
        initialVolume = volume;
    }

    public void PauseSound(bool fade = false, float dur = 1)
    {
        if (!fade)
        {
            source.Pause();
            isPaused = true;
        }
        else
        {
            isPausing = true;
            source.DOFade(0, dur).SetEase(Ease.Linear).OnComplete(() =>
             {
                 if (isPausing == false)
                     return;
                 source.Pause();
                 isPausing = false;
                 isPaused = true;
             });
        }
    }

    public void ResumeSound(bool fade = false, float dur = 1)
    {        
        isPaused = false;
        isPausing = false;
        if (!fade) 
            source.volume = initialVolume;
        source.UnPause();
        if (fade)
        {
            source.DOFade(initialVolume, dur).SetEase(Ease.Linear);
        }
        
    }

    public void ModifyPitch(float newPitch)
    {
        source.pitch = newPitch;
    }

    public void StopSound(bool fade = false, float dur = 1)
    {
        if (!fade)
        {
            source.Stop();
            OnComplete();
        }
        else
        {
            source.DOFade(0, dur).OnComplete(() =>
            {
                source.Stop();
                OnComplete();
            });
        }
    }

    private void OnComplete()
    {
        enabled = false;
        isPaused = false;
        manager.ReAddToStack(this);
    }

    public void FadeIn(float startVol, float endVol, float dur)
    {
        source.volume = startVol;
        source.DOFade(endVol, dur);
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public bool IsPausing()
    {
        return isPausing;
    }

}
