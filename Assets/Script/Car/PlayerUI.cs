using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    public FloatSO currentHP;
    public FloatSO maxHP;

    public FloatSO currentNitro;
    public FloatSO maxNitro;


    public Image HPbar;

    public Image NitroBar;


    public FloatSO beaconsRemaining;

    public BoolSO bossSpawned;

    public bool bossFlag = false;

    public TMP_Text beaconCount;



    public GameObject beaconGroup;

    public GameObject bossGroup;


    // Start is called before the first frame update
    void Start()
    {
        beaconGroup.SetActive(true);
        bossGroup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        HPbar.fillAmount = currentHP.Float / maxHP.Float;

        NitroBar.fillAmount = currentNitro.Float / maxNitro.Float;



        beaconCount.text = Mathf.Abs(beaconsRemaining.Float - 5).ToString() + " / 5";


        if (bossSpawned.Bool && !bossFlag)
        {
            bossFlag = true;

            beaconGroup.SetActive(false);
            bossGroup.SetActive(true);
        }


    }






}
