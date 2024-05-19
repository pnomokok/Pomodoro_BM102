//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;
//using System;
//using UnityEngine.Events;

//public class Timer : MonoBehaviour
//{
//    public TMP_Text timeText;
//    public Image slider;
//    public float workTimeLimit = 60f; // Çalýþma süresi
//    public float breakTimeLimit = 60f; // Mola süresi
//    public int setCount = 3; // Tekrar sayýsý

//    public bool inMinutes;

//    private float pausedTime;

//    [Space]
//    public UnityEvent OnStart, OnComplete;

//    private float time;
//    private bool startTimer;
//    private bool isWorkTime; // Çalýþma mý yoksa mola zamaný mý olduðunu takip eder

//    private float multiplierFactor;
//    private int currentSetCount;

//    private void Start()
//    {
//        ResetTimer();
//    }

//    public void StartTimer()
//    {
//        if (!startTimer)
//        {
//            if (pausedTime > 0f)
//            {
//                time = pausedTime; // Durdurulan zamaný kullan
//                pausedTime = 0f; // Durdurulan zamaný sýfýrla
//                multiplierFactor = 1f / (isWorkTime ? workTimeLimit : breakTimeLimit); // Doðru limiti kullan
//                slider.fillAmount = time * multiplierFactor; // Slider'ý doðru þekilde güncelle
//            }
//            else
//            {
//                time = isWorkTime ? workTimeLimit : breakTimeLimit;
//                multiplierFactor = 1f / time;
//                slider.fillAmount = 1f; // Yeni süre baþladýðýnda slider'ý tam doldur
//            }
//            startTimer = true;
//            OnStart?.Invoke();
//        }
//    }

//    private void Update()
//    {
//        if (!startTimer) return;

//        if (time > 0f)
//        {
//            time -= Time.deltaTime;
//            UpdateTimeText();
//            slider.fillAmount = time * multiplierFactor;
//        }
//        else
//        {
//            if (isWorkTime)
//            {
//                // Çalýþma süresi tamamlandý, mola süresine geç
//                isWorkTime = false;
//                time = breakTimeLimit;
//            }
//            else
//            {
//                // Mola süresi tamamlandý, set sayýsýný kontrol et
//                currentSetCount--;
//                if (currentSetCount > 0)
//                {
//                    isWorkTime = true;
//                    time = workTimeLimit;
//                }
//                else
//                {
//                    // Tüm setler tamamlandý
//                    startTimer = false;
//                    OnComplete?.Invoke();
//                    ResetTimer();
//                    return;
//                }
//            }
//            multiplierFactor = 1f / time;
//            UpdateTimeText();
//            slider.fillAmount = 1f;
//        }
//    }

//    private void UpdateTimeText()
//    {
//        if (time < 0f) time = 0f; // Zaman negatifse sýfýrlayýn
//        if (inMinutes)
//        {
//            int minutes = Mathf.FloorToInt(time / 60);
//            int seconds = Mathf.FloorToInt(time % 60);
//            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
//        }
//        else
//        {
//            timeText.text = Mathf.CeilToInt(time).ToString();
//        }
//    }

//    public void PauseTimer()
//    {
//        if (startTimer)
//        {
//            startTimer = false;
//            pausedTime = time;
//        }
//    }

//    public void RestartTimer()
//    {
//        startTimer = false;
//        pausedTime = 0f;
//        ResetTimer();
//    }

//    private void ResetTimer()
//    {
//        currentSetCount = setCount;
//        isWorkTime = true;
//        time = workTimeLimit;
//        multiplierFactor = 1f / time;
//        UpdateTimeText();
//        slider.fillAmount = 1f;
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public TMP_Text timeText;
    public Image slider;
    public float workTimeLimit = 60f; // Çalýþma süresi
    public float breakTimeLimit = 60f; // Mola süresi
    public int setCount = 3; // Tekrar sayýsý
    public ChangeColor changeColorScript; // ChangeColor script referansý
    public bool inMinutes;
    private float pausedTime;
    [Space]
    public UnityEvent OnStart, OnComplete;

    private float time;
    private bool startTimer;
    private bool isWorkTime; // Çalýþma mý yoksa mola zamaný mý olduðunu takip eder
    private float multiplierFactor;
    private int currentSetNumber;

    private void Start()
    {
        ResetTimer();
    }

    public void StartTimer()
    {
        if (!startTimer)
        {
            if (pausedTime > 0f)
            {
                time = pausedTime; // Durdurulan zamaný kullan
                pausedTime = 0f; // Durdurulan zamaný sýfýrla
                multiplierFactor = 1f / (isWorkTime ? workTimeLimit : breakTimeLimit); // Doðru limiti kullan
                slider.fillAmount = time * multiplierFactor; // Slider'ý doðru þekilde güncelle
            }
            else
            {
                time = isWorkTime ? workTimeLimit : breakTimeLimit;
                multiplierFactor = 1f / time;
                slider.fillAmount = 1f; // Yeni süre baþladýðýnda slider'ý tam doldur
            }
            startTimer = true;
            OnStart?.Invoke();
            UpdateStatusText();
        }
    }

    private void Update()
    {
        if (!startTimer) return;

        if (time > 0f)
        {
            time -= Time.deltaTime;
            UpdateTimeText();
            slider.fillAmount = time * multiplierFactor;
        }
        else
        {
            if (isWorkTime)
            {
                // Çalýþma süresi tamamlandý, mola süresine geç
                isWorkTime = false;
                time = breakTimeLimit;
            }
            else
            {
                // Mola süresi tamamlandý, set sayýsýný kontrol et
                currentSetNumber++;
                if (currentSetNumber < setCount)
                {
                    isWorkTime = true;
                    time = workTimeLimit;
                }
                else
                {
                    // Tüm setler tamamlandý
                    startTimer = false;
                    OnComplete?.Invoke();
                    ResetTimer();
                    return;
                }
            }
            multiplierFactor = 1f / time;
            UpdateTimeText();
            slider.fillAmount = 1f;
            UpdateStatusText();
        }
    }

    private void UpdateTimeText()
    {
        if (time < 0f) time = 0f; // Zaman negatifse sýfýrlayýn
        if (inMinutes)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timeText.text = Mathf.CeilToInt(time).ToString();
        }
    }

    public void PauseTimer()
    {
        if (startTimer)
        {
            startTimer = false;
            pausedTime = time;
        }
    }

    public void RestartTimer()
    {
        startTimer = false;
        pausedTime = 0f;
        ResetTimer();
    }

    private void ResetTimer()
    {
        currentSetNumber = 0;
        isWorkTime = true;
        time = workTimeLimit;
        multiplierFactor = 1f / time;
        UpdateTimeText();
        slider.fillAmount = 1f;
        UpdateStatusText(); // Ýlk baþta durumu güncelle
    }

    private void UpdateStatusText()
    {
        if (changeColorScript != null)
        {
            if (isWorkTime)
            {
                changeColorScript.UpdateText($"{currentSetNumber + 1}. set Ders");
            }
            else
            {
                changeColorScript.UpdateText($"{currentSetNumber + 1}. set Mola");
            }
        }
    }
}
