using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOperations : MonoBehaviour
{

    //public Slider volumeSlider;


    public GameObject mainMenu;

    public GameObject settingsMenu;

    public GameObject creditsMenu;

    public string mainLevelToStart;

    public void StartButton()
    {
        SceneManager.LoadScene(mainLevelToStart);
    }

    public void SettingsButton()
    {
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
