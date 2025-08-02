using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


    public GameObject pauseMenu;


    public string mainMenuStr;

    public bool gamePaused = false;


    public BoolSO winScreen;

    public BoolSO deathScreen;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !winScreen.Bool && !deathScreen.Bool)
        {
            TogglePauseMenu();
        }
    }


    public void TogglePauseMenu()
    {
        gamePaused = !gamePaused;

        if (gamePaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }


    public void BackToMenu()
    {
        Time.timeScale = 1f;
        DOTween.KillAll();
        SceneManager.LoadScene(mainMenuStr);

    }





}
