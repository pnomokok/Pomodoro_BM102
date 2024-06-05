using UnityEngine;
using TMPro;

public class ResultController : MonoBehaviour
{
    public TMP_Text heartRateText;
    public TMP_Text stressLevelText;

    void Start()
    {
        int heartRate = PlayerPrefs.GetInt("HeartRate");
        heartRateText.text = heartRate + " BPM";

        if (heartRate > 0)
        {
            string stressLevel = GetStressLevel(heartRate);
            stressLevelText.text = "Stres seviyeniz: " + stressLevel;
        }
        else
        {
            stressLevelText.text = "Ge�ersiz nab�z de�eri.";
        }
    }

    string GetStressLevel(int heartRate)
    {
        if (heartRate < 50)
            return "�l��m esnas�nda parma��n�z� kald�rmay�n ve tekrar deneyin.";
        else if (heartRate < 80)
            return "D���k stres seviyesi";
        else if (heartRate < 100)
            return "Orta stres seviyesi";
        else
            return "Y�ksek stres seviyesi";
    }
}
