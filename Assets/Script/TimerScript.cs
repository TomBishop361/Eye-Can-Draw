using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{
    
    [SerializeField] private Image imageFill;
    [SerializeField] private TMP_Text text;

    public int _duration;
    private int bonustimer;
    private int timeRemaining;

    //starts timer with chosen duration
    public void startTimer(int duration)
    {
        _duration = duration;
        timeRemaining = duration;
        bonustimer = (int)(duration * 0.25f);
        StartCoroutine("timerUpdate");
    }

    //counts timer down and formats text to 00:00
    private IEnumerator timerUpdate()
    {
        while (timeRemaining > -1) {
            text.text = $"{timeRemaining / 60:00} : {timeRemaining % 60:00}";
            imageFill.fillAmount = Mathf.InverseLerp(0,_duration,timeRemaining);
            if (timeRemaining % bonustimer == 0)
            {
                Manager.Instance.bonus -= 5;
                Manager.Instance.AddHint();
            }
            timeRemaining--;
            yield return new WaitForSeconds(1f);
        }
        timerEnd();
    }

    private void timerEnd()
    {
        Manager.Instance.Correct(false);
    }
}
