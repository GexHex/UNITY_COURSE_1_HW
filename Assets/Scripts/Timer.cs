using System;
using UnityEngine;

public class Timer : MonoBehaviour
{   
    public float TotalTimeRound { get; private set; }
    public float TotalTime { get; private set; }  

    public void TimerStart()
    {      
        TotalTime += Time.deltaTime; 
        TotalTimeRound = (float)Math.Round(TotalTime, 2);
    }

    public void TimerReset()
    {
        TotalTimeRound = 0;
    }
}