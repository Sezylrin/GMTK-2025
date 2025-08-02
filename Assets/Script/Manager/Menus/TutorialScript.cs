using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{


    public RectTransform textBox;


    public float slideTime = .5f;


    public float readTime = 4f;

    [TextArea(15, 30)]
    public string[] tutorialTexts;

    [TextArea(15, 30)]
    public string[] randomTexts;

    [TextArea(15, 30)]
    public string[] bossTexts;


    public TMP_Text textBoxToShow;


    private Queue<string> textQueue = new Queue<string>();


    public BoolSO bossSpawned;
    private bool bossFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        textBox.position = new Vector3(textBox.transform.position.x, -150, textBox.transform.position.z);

        textQueue.Enqueue(tutorialTexts[0]);
        textQueue.Enqueue(tutorialTexts[1]);


        StartCoroutine("TextBoxSlide");

    }

    // Update is called once per frame
    void Update()
    {
        CheckForTextBoxUpdates();
    }



    private void CheckForTextBoxUpdates()
    {


        if (bossSpawned.Bool && !bossFlag)
        {
            bossFlag = true;


            textQueue.Enqueue(bossTexts[0]);
            textQueue.Enqueue(bossTexts[1]);
            textQueue.Enqueue(bossTexts[1]);

            StartCoroutine("TextBoxSlide");

        }







    }


    public IEnumerator TextBoxSlide()
    {

        Debug.Log("Start textbox slide");

        while (textQueue.Count > 0)
        {
            string text = textQueue.Dequeue();
            textBoxToShow.text = text;
            

            textBox.DOMoveY(150, slideTime).SetEase(Ease.OutCubic);

            yield return new WaitForSeconds(slideTime);

            yield return new WaitForSeconds(readTime);

            textBox.DOMoveY(-150, slideTime).SetEase(Ease.InCubic);

            yield return new WaitForSeconds(slideTime);


            textBoxToShow.text = "";


        }



    }


}
