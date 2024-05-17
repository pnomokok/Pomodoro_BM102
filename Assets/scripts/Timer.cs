using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class timer : MonoBehaviour
{
    public TMP_Text timeText;
    public Image slider;
    public float timeLimit = 60f;

    public bool inMinutes;

    [Space]
    public UnityEvent OnStart, OnComplete;
    
    float time;
    bool startTimer;

    float multiplierFactor;
    TimeSpan timeConvertor;

    private void Start()
    {
        timeText.text= timeLimit.ToString();
        time = timeLimit;
        slider.fillAmount = multiplierFactor * time;
        startTimer = false;


        if (inMinutes)
        {
            timeConvertor = TimeSpan.FromSeconds(time);
            float minutes = timeConvertor.Minutes;
            float seconds = timeConvertor.Seconds;

            timeText.text = $"{minutes}:{seconds}";
        }
        else
        {
            timeText.text = Mathf.CeilToInt(time).ToString();
        }

    }
     public void StartTimer()
     {
        multiplierFactor = 1f/timeLimit;
        startTimer = true;

        slider.fillAmount=multiplierFactor*time;

        OnStart?.Invoke();  
     }
    private void Update()
    {
        if (!startTimer) return;
       
        if(time > 0f)
        {
            time -= Time.deltaTime;

            if (inMinutes)
            {
                timeConvertor = TimeSpan.FromSeconds(time);
                float minutes = timeConvertor.Minutes;
                float seconds = timeConvertor.Seconds;

                timeText.text = $"{minutes}:{seconds}";
            }
            else
            {
                timeText.text = Mathf.CeilToInt(time).ToString();
            }
  
            slider.fillAmount = multiplierFactor * time;
        }
        else
        {
            startTimer = false;
            OnComplete?.Invoke();
        }
    }
    public void PauseTimer()
    {
        if(startTimer)
        {
            startTimer= false;
        }
    }
    public void RestartTimer()
    {
        time = timeLimit;
        if (inMinutes)
        {
            timeConvertor = TimeSpan.FromSeconds(time);
            float minutes = timeConvertor.Minutes;
            float seconds = timeConvertor.Seconds;

            timeText.text = $"{minutes}:{seconds}";
        }
        else
        {
            timeText.text = Mathf.CeilToInt(time).ToString();
        }

        timeText.text = time.ToString();
        slider.fillAmount = multiplierFactor * time;

    }
}
