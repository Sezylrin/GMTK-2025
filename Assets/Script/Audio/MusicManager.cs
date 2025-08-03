using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioObj mainMenuMusic;
    private AudioObj bossMusic;
    private AudioObj levelMusic;
    private AudioObj winMusic;
    [SerializeField, Min(0)]
    private float fadeDuration;
    [SerializeField, Range(0f, 1f)]
    private float Volume;
    [SerializeField]
    private BoolSO bossSpawned;
    [SerializeField]
    private BoolSO triggerWinScreen;

    private int musicState = 0;

    [SerializeField, Range(0f, 1f)]
    private float AmbVolume;

    void Start()
    {

        AudioManager.Instance.PlaySound(AudioRef.cityAmb, true, AmbVolume);

        mainMenuMusic = AudioManager.Instance.PlaySound(AudioRef.mainTheme, true, Volume);
        levelMusic = AudioManager.Instance.PlaySound(AudioRef.levelTheme, true, Volume);
        bossMusic = AudioManager.Instance.PlaySound(AudioRef.bossTheme, true, Volume);
        winMusic = AudioManager.Instance.PlaySound(AudioRef.endTheme, true, Volume);
        bossMusic.PauseSound();
        levelMusic.PauseSound();
        winMusic.PauseSound();
    }

    // Update is called once per frame
    void Update()
    {



        //if (SceneManager.GetActiveScene().buildIndex == 0 && (mainMenuMusic.IsPaused() || mainMenuMusic.IsPausing()))
        //{

        //}
        //else if (SceneManager.GetActiveScene().buildIndex == 1 && (levelMusic.IsPaused() || levelMusic.IsPausing()))
        //{
        //    mainMenuMusic.PauseSound(true, fadeDuration);
        //    levelMusic.ResumeSound(true, fadeDuration);
        //}
        //else if (bossSpawned.Bool && (bossMusic.IsPaused() || bossMusic.IsPausing()))
        //{
        //    levelMusic.PauseSound(true,fadeDuration);
        //    bossMusic.ResumeSound(true,fadeDuration);
        //}
        //else if (!bossSpawned.Bool && mainMenuMusic.IsPausing() || mainMenuMusic.IsPaused())
        //{
        //    bossMusic.PauseSound(true, fadeDuration);
        //    winMusic.ResumeSound(true, fadeDuration);
        //}



        //if (SceneManager.GetActiveScene().buildIndex == 0 && musicState != 0)
        //{
        //    print("main");
        //    musicState = 0;
        //    ResetMusic();
        //}
        //else if (SceneManager.GetActiveScene().buildIndex == 1 && musicState < 1)
        //{
        //    print("level");

        //    musicState = 1;
        //    mainMenuMusic.PauseSound(true, fadeDuration);
        //    levelMusic.ResumeSound(true, fadeDuration);
        //}
        //else if (bossSpawned.Bool && musicState < 2)
        //{
        //    print("boss");

        //    musicState = 2;
        //    levelMusic.PauseSound(true, fadeDuration);
        //    bossMusic.ResumeSound(true, fadeDuration);
        //}
        //else if (triggerWinScreen.Bool && musicState < 3)
        //{
        //    print("end");

        //    musicState = 3;
        //    bossMusic.PauseSound(true, fadeDuration);
        //    winMusic.ResumeSound(true, fadeDuration);
        //}




        if (SceneManager.GetActiveScene().buildIndex == 0 && musicState != 0)
        {
            print("main");
            musicState = 0;
            ResetMusic();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1 && musicState < 1)
        {
            print("level");

            musicState = 1;
            mainMenuMusic.PauseSound(true, fadeDuration);
            levelMusic.ResumeSound(true, fadeDuration);
        }
        else if (triggerWinScreen.Bool && musicState < 3)
        {
            print("end");

            musicState = 3;
            levelMusic.PauseSound(true, fadeDuration);
            winMusic.ResumeSound(true, fadeDuration);
        }








    }



    public void ResetMusic()
    {
        mainMenuMusic.StopSound(false);
        levelMusic.StopSound(false);
        bossMusic.StopSound(false);
        winMusic.StopSound(false);
        mainMenuMusic = AudioManager.Instance.PlaySound(AudioRef.mainTheme, true, Volume);
        levelMusic = AudioManager.Instance.PlaySound(AudioRef.levelTheme, true, Volume);
        bossMusic = AudioManager.Instance.PlaySound(AudioRef.bossTheme, true, Volume);
        winMusic = AudioManager.Instance.PlaySound(AudioRef.endTheme, true, Volume);
        bossMusic.PauseSound();
        levelMusic.PauseSound();
        winMusic.PauseSound();
    }



}
