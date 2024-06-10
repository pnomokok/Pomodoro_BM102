using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class SensorController : MonoBehaviour
{
    public Button measureButton; // �l��m yapmak i�in kullan�lan buton
    public TMP_Text statusText; // Durum mesajlar�n� g�stermek i�in kullan�lan text
    private string deviceName = "esp32"; // Deneyap Kart'�n mDNS ad�
    private int port = 80; // Kart�n port numaras�

    void Start()
    {
        measureButton.onClick.AddListener(OnMeasureButtonClick);
    }

    // �l��m butonuna t�klan�ld���nda �al��acak olan fonksiyon
    public void OnMeasureButtonClick()
    {
        statusText.text = "�l��m yap�l�yor...";
        Debug.Log("�l��m butonuna bas�ld�.");

        // Karta HTTP iste�i g�ndermek i�in URL olu�turur
        string url = "http://" + deviceName + ".local:" + port + "/start";
        Debug.Log("URL: " + url);

        // �l��m butonunu pasif yapar
        measureButton.interactable = false;

        // HTTP iste�ini ba�lat�r
        StartCoroutine(GetHeartRate(url));
    }

    // HTTP iste�ini yapan ve yan�t� i�leyen coroutine
    IEnumerator GetHeartRate(string url)
    {
        // HTTP GET iste�i olu�turur
        UnityWebRequest www = UnityWebRequest.Get(url);
        Debug.Log("HTTP iste�i g�nderildi.");

        // �ste�in tamamlanmas�n� bekler
        yield return www.SendWebRequest();

        // �stek ba�ar�s�z olduysa
        if (www.result != UnityWebRequest.Result.Success)
        {
            // Kullan�c�ya hata mesaj� g�sterir
            statusText.text = "Hata: Sens�re ba�lan�lamad�.";
            // �l��m butonunu aktif hale getirir
            measureButton.interactable = true;
            Debug.LogError("HTTP iste�i hatas�: " + www.error); // Hata logu
        }
        else
        {
            // �stek ba�ar�l� olduysa yan�t� al�r
            string response = www.downloadHandler.text;
            Debug.Log("HTTP yan�t� al�nd�: " + response);

            // Yan�t� bir tamsay�ya d�n��t�rmeye �al���r
            if (int.TryParse(response, out int heartRate))
            {
                // Ba�ar�l�ysa nab�z verisini kaydeder
                PlayerPrefs.SetInt("HeartRate", heartRate);
                PlayerPrefs.Save();
                Debug.Log("Nab�z verisi kaydedildi: " + heartRate);

                // Belirtilen sahneyi y�kler
                UnityEngine.SceneManagement.SceneManager.LoadScene("RENK5.4_ssonuc");
            }
            else
            {
                // Yan�t ge�ersizse kullan�c�ya hata mesaj� g�sterir
                statusText.text = "Ge�ersiz �l��m sonucu.";
                // �l��m butonunu tekrar aktif hale getirir
                measureButton.interactable = true;
                Debug.LogError("Ge�ersiz �l��m sonucu: " + response);
            }
        }
    }
}
