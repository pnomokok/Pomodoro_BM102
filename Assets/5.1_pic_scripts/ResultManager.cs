using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    void Start()
    {
        int score = Convert.ToInt32(SurveyData.totalScore * 85.0f / 21.0f);// %100 cýkmasý onaylanmadý
         resultText.text = (score).ToString() + "%";
       
    }
}

