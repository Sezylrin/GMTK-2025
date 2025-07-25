using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExampleTimer : MonoBehaviour
{
    enum Example : int
    {
        CDOne,
        CDTwo,
    }
    [SerializeField]
    private TimerManager timerManager;
    Timer timerOne;
    Timer timerTwo;
    // Start is called before the first frame update
    void Start()
    {
        timerOne = timerManager.GenerateTimers(typeof(Example),gameObject);
        timerTwo = timerManager.GenerateTimers(1, gameObject);
        timerOne.SetTime((int)Example.CDOne, 2f);
        timerTwo.SetTime(0, 5f);
        timerOne.times[(int)Example.CDOne].OnTimeIsZero += ExampleMethodOne;
    }


    // Update is called once per frame
    void Update()
    {
        if (timerTwo.IsTimeZero(0))
        {
            Debug.Log("timer two has finished and will be reset");
            timerTwo.ResetTime();
        }
    }

    public void ExampleMethodOne(object sender, EventArgs e)
    {
        Debug.Log("timer One has finished");
    }
}
