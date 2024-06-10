using UnityEngine;
using TMPro;
public class ResultController : MonoBehaviour
{
    public TMP_Text heartRateText; // Nabýz deðerini göstermek için kullanýlan Text
    public TMP_Text stressLevelText; // Stres seviyesini göstermek için kullanýlan Text

    void Start()
    {
        // PlayerPrefs'ten kaydedilmiþ nabýz deðerini alýr
        int heartRate = PlayerPrefs.GetInt("HeartRate");

        // Nabýz deðerini Text elemanýna atar ve kullanýcýya gösterir
        heartRateText.text = heartRate + " BPM";

        // Nabýz deðeri geçerliyse stres seviyesini hesaplar ve gösterir
        if (heartRate > 0)
        {
            string stressLevel = GetStressLevel(heartRate);
            stressLevelText.text = stressLevel;
        }
        else
        {
            // Geçersiz nabýz deðeri durumunda hata mesajý gösterir
            stressLevelText.text = "Geçersiz nabýz deðeri.";
        }
    }

    // Nabýz deðerine göre stres seviyesini belirleyen fonksiyon
    string GetStressLevel(int heartRate)
    {
        // Nabýz deðerine göre stres seviyesini döner
        if (heartRate < 50)
            return "Hata!";
        else if (heartRate < 80)
            return "Düþük stres seviyesi";
        else if (heartRate < 100)
            return "Orta stres seviyesi";
        else
            return "Yüksek stres seviyesi";
    }
}
