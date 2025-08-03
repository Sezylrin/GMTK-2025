using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BriefingScript : MonoBehaviour
{


    [TextArea(5, 30)]
    public string[] briefingTexts;

    public TMP_Text textBox;


    public MenuOperations menuScript;


    private Queue<string> textQueue = new Queue<string>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenBriefing()
    {
        textBox.text = briefingTexts[0];

        for (int i = 1; i < briefingTexts.Length; i++)
        {
            textQueue.Enqueue(briefingTexts[i]);
        }

    }


    public void NextButton()
    {
        if (textQueue.Count > 0)
        {
            textBox.text = textQueue.Dequeue();
            return;
        }

        menuScript.StartButton();

    }



}
