using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{
    [SerializeField] Manager manager;
    [SerializeField] private Image imageFill;
    [SerializeField] private TMP_Text text;

    public int duration;

    private int timeRemaining;


    public void startTimer()
    {
        timeRemaining = duration;
        StartCoroutine("timerUpdate");
    }

    private IEnumerator timerUpdate()
    {
        while (timeRemaining > -1) {
            text.text = $"{timeRemaining / 60:00} : {timeRemaining % 60:00}";
            imageFill.fillAmount = Mathf.InverseLerp(0,duration,timeRemaining);
            timeRemaining--;
            yield return new WaitForSeconds(1f);
        }
        timerEnd();
    }

    private void timerEnd()
    {
        Debug.Log("times up!");
        manager.Correct(false);
    }
}
