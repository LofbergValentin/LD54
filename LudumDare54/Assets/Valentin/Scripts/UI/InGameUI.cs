using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text timer;
    [SerializeField] GameObject UI;

    public void DisplayUI(bool value)
    {
        UI.SetActive(value);
    }

    public void SetTimer(float timerValue)
    {
        TimeSpan ts = TimeSpan.FromSeconds(timerValue);
        timer.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes-0.5, ts.Seconds);
    }
}
