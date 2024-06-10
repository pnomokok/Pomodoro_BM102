using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class SensorController : MonoBehaviour
{
    public Button measureButton; // Ölçüm yapmak için kullanýlan buton
    public TMP_Text statusText; // Durum mesajlarýný göstermek için kullanýlan text
    private string deviceName = "esp32"; // Deneyap Kart'ýn mDNS adý
    private int port = 80; // Kartýn port numarasý

    void Start()
    {
        measureButton.onClick.AddListener(OnMeasureButtonClick);
    }

    // Ölçüm butonuna týklanýldýðýnda çalýþacak olan fonksiyon
    public void OnMeasureButtonClick()
    {
        statusText.text = "Ölçüm yapýlýyor...";
        Debug.Log("Ölçüm butonuna basýldý.");

        // Karta HTTP isteði göndermek için URL oluþturur
        string url = "http://" + deviceName + ".local:" + port + "/start";
        Debug.Log("URL: " + url);

        // Ölçüm butonunu pasif yapar
        measureButton.interactable = false;

        // HTTP isteðini baþlatýr
        StartCoroutine(GetHeartRate(url));
    }

    // HTTP isteðini yapan ve yanýtý iþleyen coroutine
    IEnumerator GetHeartRate(string url)
    {
        // HTTP GET isteði oluþturur
        UnityWebRequest www = UnityWebRequest.Get(url);
        Debug.Log("HTTP isteði gönderildi.");

        // Ýsteðin tamamlanmasýný bekler
        yield return www.SendWebRequest();

        // Ýstek baþarýsýz olduysa
        if (www.result != UnityWebRequest.Result.Success)
        {
            // Kullanýcýya hata mesajý gösterir
            statusText.text = "Hata: Sensöre baðlanýlamadý.";
            // Ölçüm butonunu aktif hale getirir
            measureButton.interactable = true;
            Debug.LogError("HTTP isteði hatasý: " + www.error); // Hata logu
        }
        else
        {
            // Ýstek baþarýlý olduysa yanýtý alýr
            string response = www.downloadHandler.text;
            Debug.Log("HTTP yanýtý alýndý: " + response);

            // Yanýtý bir tamsayýya dönüþtürmeye çalýþýr
            if (int.TryParse(response, out int heartRate))
            {
                // Baþarýlýysa nabýz verisini kaydeder
                PlayerPrefs.SetInt("HeartRate", heartRate);
                PlayerPrefs.Save();
                Debug.Log("Nabýz verisi kaydedildi: " + heartRate);

                // Belirtilen sahneyi yükler
                UnityEngine.SceneManagement.SceneManager.LoadScene("RENK5.4_ssonuc");
            }
            else
            {
                // Yanýt geçersizse kullanýcýya hata mesajý gösterir
                statusText.text = "Geçersiz ölçüm sonucu.";
                // Ölçüm butonunu tekrar aktif hale getirir
                measureButton.interactable = true;
                Debug.LogError("Geçersiz ölçüm sonucu: " + response);
            }
        }
    }
}
