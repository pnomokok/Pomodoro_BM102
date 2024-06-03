using UnityEngine;
using TMPro;

public class ResultController : MonoBehaviour
{
    public TMP_Text heartRateText;
    public TMP_Text stressLevelText;

    void Start()
    {
        string heartRate = PlayerPrefs.GetString("HeartRate");
        heartRateText.text = "Nabýz Deðeriniz: " + heartRate;

        int bpm = int.Parse(heartRate);
        if (bpm < 60)
        {
            stressLevelText.text = "Stres seviyeniz: Düsük";
        }
        else if (bpm >= 60 && bpm < 90)
        {
            stressLevelText.text = "Stres seviyeniz: Orta";
        }
        else
        {
            stressLevelText.text = "Stres seviyeniz: Yüksek";
        }
    }
}
