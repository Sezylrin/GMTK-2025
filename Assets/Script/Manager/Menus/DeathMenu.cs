using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{


    public GameObject deathScreenMenu;

    public GameObject winScreenMenu;

    public BoolSO showDeathScreen;

    public BoolSO showWinScreen;

    public Image BgFade;

    public Image BgFade1;

    [SerializeField]
    public Color BgFadeFinalColor;

    public float fadeInTime = 1;


    public bool deathFlag = false;

    public bool winFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        showDeathScreen.Bool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (showDeathScreen.Bool && !deathFlag)
        {
            StartCoroutine(DeathScreenRoutine());

            deathFlag = true;

        }


        if (showWinScreen.Bool && !winFlag)
        {

            StartCoroutine(WinScreenRoutine());

            winFlag = true;

        }

    }


    private IEnumerator DeathScreenRoutine()
    {
        BgFade.DOColor(BgFadeFinalColor, fadeInTime);

        //BgFade.DOFade(150, fadeInTime);
        
        yield return new WaitForSeconds(fadeInTime + 0.01f);

        deathScreenMenu.SetActive(true);

        Time.timeScale = 0f;

    }

    private IEnumerator WinScreenRoutine()
    {
        BgFade1.DOColor(BgFadeFinalColor, fadeInTime);

        //BgFade.DOFade(150, fadeInTime);

        yield return new WaitForSeconds(fadeInTime + 0.01f);

        winScreenMenu.SetActive(true);

        Time.timeScale = 0f;

    }





    public void TryAgain()
    {
        Time.timeScale = 1f;
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
