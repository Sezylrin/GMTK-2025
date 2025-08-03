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

    public GameObject briefingMenu;

    public string mainLevelToStart;



    public Slider MasterVol;

    public Slider BGMVol;

    public Slider SFXVol;


    public BriefingScript briefScript;

    public void StartButton()
    {
        AudioManager.Instance.PlaySound(AudioRef.uiAccpet);
        SceneManager.LoadScene(mainLevelToStart);
    }

    public void SettingsButton()
    {


        if (mainMenu.activeInHierarchy)
        {
            AudioManager.Instance.PlaySound(AudioRef.uiAccpet);
        }
        else
        {
            AudioManager.Instance.PlaySound(AudioRef.uiCancel);
        }


        MasterVol.value = AudioManager.Instance.masterVolume / 100;
        BGMVol.value = AudioManager.Instance.bgmVolume / 100;
        SFXVol.value = AudioManager.Instance.sfxVolume / 100;


        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        settingsMenu.SetActive(!settingsMenu.activeInHierarchy);
    }

    public void CreditsButton()
    {
        if (mainMenu.activeInHierarchy)
        {
            AudioManager.Instance.PlaySound(AudioRef.uiAccpet);
        }
        else
        {
            AudioManager.Instance.PlaySound(AudioRef.uiCancel);
        }
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        creditsMenu.SetActive(!creditsMenu.activeInHierarchy);
    }

    public void QuitButton()
    {
        AudioManager.Instance.PlaySound(AudioRef.uiCancel);
        Application.Quit();
    }


    public void OpenBriefing()
    {
        AudioManager.Instance.PlaySound(AudioRef.uiSpy);
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        briefingMenu.SetActive(true);
        briefScript.OpenBriefing();
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
