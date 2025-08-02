using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioObj normalMusic;
    private AudioObj bossMusic;
    [SerializeField, Min(0)]
    private float fadeDuration;
    [SerializeField, Range(0f, 1f)]
    private float Volume;
    [SerializeField]
    private BoolSO bossSpawned;
    void Start()
    {
        normalMusic = AudioManager.Instance.PlaySound(AudioRef.mainTheme, true, Volume);
        bossMusic = AudioManager.Instance.PlaySound(AudioRef.bossTheme, true, Volume);
        bossMusic.PauseSound();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossSpawned.Bool && (bossMusic.IsPaused() || bossMusic.IsPausing()))
        {
            normalMusic.PauseSound(true,fadeDuration);
            bossMusic.ResumeSound(true,fadeDuration);
        }
        else if (!bossSpawned.Bool && normalMusic.IsPausing() || normalMusic.IsPaused())
        {
            normalMusic.ResumeSound(true, fadeDuration);
            bossMusic.PauseSound(true, fadeDuration);
        }
    }
}
