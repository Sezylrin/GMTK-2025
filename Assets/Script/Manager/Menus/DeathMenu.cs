using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{


    public GameObject deathScreenMenu;

    public BoolSO showDeathScreen;

    public Image BgFade;

    [SerializeField]
    public Color BgFadeFinalColor;

    public float fadeInTime = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showDeathScreen.Bool)
        {
            StartCoroutine(DeathScreenRoutine());
            showDeathScreen.Bool = false;
        }
    }


    private IEnumerator DeathScreenRoutine()
    {
        BgFade.DOColor(BgFadeFinalColor, fadeInTime);

        yield return new WaitForSeconds(fadeInTime);

        deathScreenMenu.SetActive(true);

        Time.timeScale = 0f;

    }


    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
