using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimerManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private List<Timer> timers = new List<Timer>();
    // Update is called once per frame
    private void LateUpdate()
    {
        for (int i = timers.Count - 1; i >= 0; i--)
        {
            Timer timer = timers[i];
            if (timer.owner == null)
            {
                timers.RemoveAt(i);
            }
            else if (timer.owner.activeInHierarchy)
            {
                for (int j = 0; j < timer.times.Length; j++)
                {
                    if (timer.times[j].isPaused)
                        continue;
                    if (timer.times[j].time > 0f)
                    {
                        timer.times[j].time -= Time.deltaTime;
                    }
                    if (timer.times[j].time < 0f)
                    {
                        timer.times[j].overflowTime = timer.times[j].time;
                        timer.times[j].time = 0;
                        timer.InvokeOnTimeIsZero(j);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Generates an timer used to store times using ints, requires owner object to cover deletion of owner
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public Timer GenerateTimers(int amount, GameObject owner)
    {
        if (amount <= 0)
        {
            Debug.Break();
            Debug.LogError("Cannot generate a zero or negative sized timer");
            return null;
        }
        Timer tempTimer = new Timer(amount, owner);
        return GenerateTimers(tempTimer);

    }
    /// <summary>
    /// Generates an timer used to store times using enums, requires owner object to cover deletion of owner
    /// </summary>
    /// <param name="enumName"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public Timer GenerateTimers(Type enumName, GameObject owner)
    {
        Timer tempTimer = new Timer(enumName, owner);
        return GenerateTimers(tempTimer);
    }
    /// <summary>
    /// Generates an timer used to store times using string list, requires owner object to cover deletion of owner
    /// </summary>
    /// <param name="names"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public Timer GenerateTimers(List<string> names, GameObject owner)
    {
        if (names.Count <= 0)
        {
            Debug.Break();
            Debug.LogError("Cannot generate a zero or negative sized timer");
            return null;
        }
        Timer tempTimer = new Timer(names, owner);
        return GenerateTimers(tempTimer);
    }

    private Timer GenerateTimers(Timer timer)
    {
        timers.Add(timer);
        return timer;
    }

}