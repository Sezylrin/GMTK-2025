using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using KevinCastejon.MissingFeatures;
using KevinCastejon.MissingFeatures.MissingAttributes;
[Serializable]
public class Timer
{
    [field: SerializeField]
    public Times[] times { get; private set; }

    [Serializable]
    public struct Times
    {
        [SerializeField]
        [HideInInspector]
        private string name;
        [ReadOnlyProp]
        public float time;
        public EventHandler OnTimeIsZero;
        [ReadOnlyProp]
        public float setTime;
        [ReadOnlyProp]
        public float overflowTime;
        [ReadOnlyProp]
        public bool isPaused;
        public void SetName(string name)
        {
            this.name = name;
        }
    }

    [ReadOnlyProp]
    public GameObject owner;
    /// <summary>
    /// Generate a timer using ints
    /// </summary>
    /// <param name="amountOfTimers"></param>
    /// <param name="owner"></param>
    public Timer(int amountOfTimers, GameObject owner)
    {
        this.owner = owner;
        times = new Times[amountOfTimers];
        for (int i = 0; i < times.Length; i++)
        {
            times[i].SetName("timer " + i.ToString());
        }
    }
    /// <summary>
    /// Generate Timer using an Enum
    /// </summary>
    /// <param name="enumName"></param>
    /// <param name="owner"></param>
    public Timer(Type enumName, GameObject owner)
    {
        this.owner = owner;
        int length = Enum.GetValues(enumName).Length;
        times = new Times[length];
        for (int i = 0; i < length; i++)
        {
            times[i].SetName(Enum.GetName(enumName, i));
        }
    }
    /// <summary>
    /// Generate Timer using a string list
    /// </summary>
    /// <param name="list"></param>
    /// <param name="owner"></param>
    public Timer(List<string> list, GameObject owner)
    {
        this.owner = owner;
        times = new Times[list.Count];
        for (int i = 0; i < times.Length; i++)
        {
            times[i].SetName(list[i]);
        }
    }

    public void SetName(int position, string name)
    {
        if (ErrorPosition(position, "SetName"))
            return;
        times[position].SetName(name);
    }

    public void SetName(string name)
    {
        SetName(0, name);
    }

    public void InvokeOnTimeIsZero(int timeSlot)
    {
        times[timeSlot].OnTimeIsZero?.Invoke(this, EventArgs.Empty);
    }
    /// <summary>
    /// Sets the time in seconds at the int position of the float array
    /// </summary>
    /// <param name="position"></param>
    /// <param name="amount"></param>
    /// <param name="startInstantly">If the timer should begin</param>
    public void SetTime(int position, float amount, bool startInstantly = true)
    {
        if (ErrorPosition(position, "SetTime"))
            return;
        if (!startInstantly)
            PauseTimer(position);
        times[position].time = amount;
        times[position].setTime = amount;
    }
    /// <summary>
    /// Sets the time in seconds at first position of the float array
    /// </summary>
    /// <param name="amount"></param>
    public void SetTime(float amount, bool startInstantly = true)
    {
        SetTime(0, amount, startInstantly);
    }

    /// <summary>
    /// returns the current time at the int position
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public float GetTime(int position)
    {
        if (ErrorPosition(position, "GetTime"))
            return -1;
        return times[position].time;
    }
    /// <summary>
    /// returns the current time at the first position
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public float GetTime()
    {
        return GetTime(0);
    }
    /// <summary>
    /// returns true if the time at the int position is zero
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool IsTimeZero(int position)
    {
        if (ErrorPosition(position, "IsTimeZero"))
            return false;
        return times[position].time == 0;
    }
    /// <summary>
    /// returns true if the time at the first position is zero
    /// </summary>
    /// <returns></returns>
    public bool IsTimeZero()
    {
        return IsTimeZero(0);
    }
    /// <summary>
    /// use to modify the time stored in the index position, can be used to add or remove time.
    /// can restrain the set time value to less than or equal to the setTime
    /// </summary>
    /// <param name="position"></param>
    /// <param name="amount"> duration to add (positive values) or subtract (negative values)</param>
    /// <param name="limitToSet"></param>
    public void ModifyTimeLeft(int position, float amount, bool limitToSet = false)
    {
        if (ErrorPosition(position, "ReduceCoolDown"))
            return;
        times[position].time += amount;
        if (limitToSet)
        {
            if (GetTime(position) > times[position].setTime)
                ResetTime(position);
        }
    }
    /// <summary>
    /// use to modify the time stored in the first position, can be used to add or remove time
    /// </summary>
    /// <param name="amount"> duration to add (positive values) or subtract (negative values)</param>
    /// <param name="limitToSet"></param>
    public void ModifyTimeLeft(float amount, bool limitToSet = false)
    {
        ModifyTimeLeft(0, amount, limitToSet);
    }

    /// <summary>
    /// use to reset all timers back to zero when owner should be deactived or necessary
    /// </summary>
    public void ResetToZero()
    {
        for (int i = 0; i < times.Length; i++)
            times[i].time = 0;
    }
    /// <summary>
    /// Reset specific timer to zero, does not result in invoke of the action.
    /// Use TriggerTimer if you wish to invoke an action instantly and stop the timer
    /// </summary>
    /// <param name="position"></param>
    public void ResetSpecificToZero(int position)
    {
        if (ErrorPosition(position, "ResetSpecificToZero"))
            return;
        times[position].time = 0;
    }
    /// <summary>
    /// Reset first timer to zero, does not result in invoke of the action.
    /// Use TriggerTimer if you wish to invoke an action instantly and stop the timer
    /// </summary>
    public void ResetSpecificToZero()
    {
        ResetSpecificToZero(0);
    }
    /// <summary>
    /// Sets timer at position to 0 and invokes the event
    /// </summary>
    /// <param name="position"></param>
    public void TriggerTimer(int position)
    {
        if (ErrorPosition(position, "TriggerTimer"))
            return;
        times[position].time = 0;
        InvokeOnTimeIsZero(position);
    }
    /// <summary>
    /// Sets first timer to 0 and invokes the event
    /// </summary>
    public void TriggerTimer()
    {
        TriggerTimer(0);
    }
    /// <summary>
    /// Reset the timer at the given position to the initial set time.
    /// Set time must have been used initially else timer will be set to 0 without invoking event.
    /// Can ResetTime with extra precision if UseOverflow is true based on time passed before timer invoked.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="UseOverflow"></param>
    public void ResetTime(int position, bool UseOverflow = false)
    {
        if (ErrorPosition(position, "ResetTime"))
            return;
        times[position].time = times[position].setTime;
        if (UseOverflow)
            ModifyTimeLeft(position, times[position].overflowTime);
    }

    /// <summary>
    /// Reset the timer at the first position to the initial set time.
    /// Set time must have been used initially else timer will be set to 0 without invoking event.
    /// Can ResetTime with extra precision if UseOverflow is true based on time passed before timer invoked.
    /// </summary>
    /// <param name="UseOverflow"></param>
    public void ResetTime(bool UseOverflow = false)
    {
        ResetTime(0, UseOverflow);
    }
    /// <summary>
    /// Pause the timer at int position
    /// </summary>
    /// <param name="position"></param>
    public void PauseTimer(int position)
    {
        if (ErrorPosition(position, "PauseTimer"))
            return;
        times[position].isPaused = true;
    }
    /// <summary>
    /// Pause the first timer
    /// </summary>
    public void PauseTimer()
    {
        PauseTimer(0);
    }
    /// <summary>
    /// Resume the timer at int position
    /// </summary>
    /// <param name="position"></param>
    public void ResumeTimer(int position)
    {
        if (ErrorPosition(position, "ResumeTimer"))
            return;
        times[position].isPaused = false;
    }
    /// <summary>
    /// Resume the first timer
    /// </summary>
    public void ResumeTimer()
    {
        ResumeTimer(0);
    }

    /// <summary>
    /// For the timer at the given position,
    /// gives a ratio in decimals based on the current time compared to the initial setTime
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public float RatioOfTimePassed(int position)
    {
        if (ErrorPosition(position, "RatioOfTimePassed"))
            return 0;
        if (times[position].setTime == 0)
            return 1;
        else
            return 1 - (times[position].time / times[position].setTime);
    }
    /// <summary>
    /// Check if timer at position is paused
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool IsPaused(int position)
    {
        if (ErrorPosition(position, "IsPaused"))
            return false;
        return times[position].isPaused;
    }
    /// <summary>
    /// Check if first timer is paused
    /// </summary>
    /// <returns></returns>
    public bool IsPaused()
    {
        return IsPaused(0);
    }
    /// <summary>
    /// For the timer at the first position,
    /// gives a ratio in decimals based on the current time compared to the initial setTime
    /// </summary>
    /// <returns></returns>
    public float RatioOfTimePassed()
    {
        return RatioOfTimePassed(0);
    }

    public void DeleteTimer()
    {
        owner = null;
    }

    private bool ErrorPosition(int position, string var)
    {
        if (position >= times.Length || position < 0)
        {
#if UNITY_EDITOR
            Debug.Break();
            Debug.LogWarning(var + " Call's position is out of bound, check the position value compared to amount of timers");
#endif
            return true;
        }
        return false;
    }
}