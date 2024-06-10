using UnityEngine;
using TMPro;
public class ResultController : MonoBehaviour
{
    public TMP_Text heartRateText; // Nab�z de�erini g�stermek i�in kullan�lan Text
    public TMP_Text stressLevelText; // Stres seviyesini g�stermek i�in kullan�lan Text

    void Start()
    {
        // PlayerPrefs'ten kaydedilmi� nab�z de�erini al�r
        int heartRate = PlayerPrefs.GetInt("HeartRate");

        // Nab�z de�erini Text eleman�na atar ve kullan�c�ya g�sterir
        heartRateText.text = heartRate + " BPM";

        // Nab�z de�eri ge�erliyse stres seviyesini hesaplar ve g�sterir
        if (heartRate > 0)
        {
            string stressLevel = GetStressLevel(heartRate);
            stressLevelText.text = stressLevel;
        }
        else
        {
            // Ge�ersiz nab�z de�eri durumunda hata mesaj� g�sterir
            stressLevelText.text = "Ge�ersiz nab�z de�eri.";
        }
    }

    // Nab�z de�erine g�re stres seviyesini belirleyen fonksiyon
    string GetStressLevel(int heartRate)
    {
        // Nab�z de�erine g�re stres seviyesini d�ner
        if (heartRate < 50)
            return "Hata!";
        else if (heartRate < 80)
            return "D���k stres seviyesi";
        else if (heartRate < 100)
            return "Orta stres seviyesi";
        else
            return "Y�ksek stres seviyesi";
    }
}
