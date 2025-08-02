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


    public string[] tutorialTexts;


    public string[] randomTexts;


    public TMP_Text textBoxToShow;


    private Queue<string> textQueue = new Queue<string>();


    // Start is called before the first frame update
    void Start()
    {
        textBox.position = new Vector3(textBox.transform.position.x, -150, textBox.transform.position.z);

        textQueue.Enqueue(tutorialTexts[0]);
        textQueue.Enqueue(tutorialTexts[1]);
        textQueue.Enqueue(tutorialTexts[2]);


        StartCoroutine("TextBoxSlide");

    }

    // Update is called once per frame
    void Update()
    {
        CheckForTextBoxUpdates();
    }



    private void CheckForTextBoxUpdates()
    {










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
