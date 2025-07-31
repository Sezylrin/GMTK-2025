using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        HPbar.fillAmount = currentHP.Float / maxHP.Float;

        NitroBar.fillAmount = currentNitro.Float / maxNitro.Float;

    }






}
