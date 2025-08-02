using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOperations : MonoBehaviour
{

    //public Slider volumeSlider;


    public GameObject mainMenu;

    public GameObject settingsMenu;

    public GameObject creditsMenu;

    public string mainLevelToStart;



    public Slider MasterVol;

    public Slider BGMVol;

    public Slider SFXVol;



    public void StartButton()
    {
        SceneManager.LoadScene(mainLevelToStart);
    }

    public void SettingsButton()
    {

        MasterVol.value = AudioManager.Instance.masterVolume / 100;
        BGMVol.value = AudioManager.Instance.bgmVolume / 100;
        SFXVol.value = AudioManager.Instance.sfxVolume / 100;


        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        settingsMenu.SetActive(!settingsMenu.activeInHierarchy);
    }

    public void CreditsButton()
    {
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        creditsMenu.SetActive(!creditsMenu.activeInHierarchy);
    }

    public void QuitButton()
    {
        Application.Quit();
    }





    public void ChangeMasterVolume()
    {
        float vol = MasterVol.value;
        AudioManager.Instance.ModifyMasterVolume(vol * 100);
    }


    public void ChangeBGMVolume()
    {
        float vol = BGMVol.value;
        AudioManager.Instance.ModifyBGMVolume(vol * 100);
    }


    public void ChangeSFXVolume()
    {
        float vol = SFXVol.value;
        AudioManager.Instance.ModifySFXVolume(vol * 100);
    }





    //public void ChangeVolume()
    //{
    //    AudioListener.volume = volumeSlider.value;
    //    SaveVolume();
    //}


    //public void LoadVolume()
    //{
    //    volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    //}

    //public void SaveVolume()
    //{
    //    PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    //}




}
