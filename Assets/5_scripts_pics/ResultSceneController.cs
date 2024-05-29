using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultSceneController : MonoBehaviour
{
    public TMP_Text bpmText;
    public TMP_Text stressLevelText;

    void Start()
    {
        float averageBpm = PlayerPrefs.GetFloat("AverageBPM");
        bpmText.text = "Nab�z de�eriniz: " + averageBpm.ToString("F2");

        string stressLevel = CalculateStressLevel(averageBpm);
        stressLevelText.text = "Stres seviyeniz: " + stressLevel;
    }

    private string CalculateStressLevel(float bpm)
    {
        if (bpm < 70.0f)
        {
            return "D���k";
        }
        else if (bpm >= 70.0f && bpm < 100.0f)
        {
            return "Orta";
        }
        else
        {
            return "Y�ksek";
        }
    }
}
