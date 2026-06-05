using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private float timerSeconds= 125.675f;

    void Update()
    {
        if(timerSeconds >= 0.0f)
        {
            timerSeconds -= Time.deltaTime;            
        }

        int minutes = (int)timerSeconds / 60;
        int seconds = (int)timerSeconds % 60;
        int mms = (int)((timerSeconds - (int)timerSeconds) * 1000);

        TimeSpan t = TimeSpan.FromSeconds(timerSeconds);
        string shortPreciseTime = t.ToString(@"mm\:ss\:ff");

        // timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, mms);
        timerText.text = shortPreciseTime;
    }

    public void SetTimerSeconds(float seconds)
    {
        timerSeconds = seconds;
    }

    public void IncDecTimerSeconds(float seconds)
    {
        timerSeconds += seconds;
    }
}
