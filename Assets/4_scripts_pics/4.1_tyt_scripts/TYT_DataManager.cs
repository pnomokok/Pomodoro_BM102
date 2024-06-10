using System.Collections.Generic;
using UnityEngine;

public class TYT_DataManager : MonoBehaviour
{
    // Singleton örneði için property
    public static TYT_DataManager tytInstance { get; private set; }

    // Son beþ net sayýsýný tutan liste
    public List<float> tytLastFiveNets = new List<float>();

    private void Awake()
    {
        // Eðer Singleton örneði yoksa, bu nesneyi Singleton olarak belirler ve yok edilmemesini saðlar
        if (tytInstance == null)
        {
            tytInstance = this;
            DontDestroyOnLoad(gameObject); // Sahne deðiþtiðinde bu nesneyi yok etmemeye yarar
            LoadData(); // Verileri yükler
        }
        else
        {
            // Eðer Singleton örneði zaten varsa, bu nesneyi yok eder
            Destroy(gameObject);
        }
    }

    // Yeni bir net deðeri ekler
    public void AddNet(float net)
    {
        // Eðer net listesi 5'ten fazla elemana sahipse, ilk elemaný siler
        if (tytLastFiveNets.Count >= 5)
        {
            tytLastFiveNets.RemoveAt(0);
        }
        tytLastFiveNets.Add(net); // Yeni net deðerini ekler
        SaveData(); // Verileri kaydeder
    }

    // Verileri kaydeder
    public void SaveData()
    {
        // Net sayýsýný kaydeder
        PlayerPrefs.SetInt("TYT_NetCount", tytLastFiveNets.Count);

        // Her bir net deðerini PlayerPrefs'e kaydeder
        for (int i = 0; i < tytLastFiveNets.Count; i++)
        {
            PlayerPrefs.SetFloat("TYT_Net" + i, tytLastFiveNets[i]);
            Debug.Log("Saved TYT_Net" + i + ": " + tytLastFiveNets[i]);
        }
        PlayerPrefs.Save(); // Deðiþiklikleri kaydeder
        Debug.Log("Data saved");
    }

    // Verileri yükler
    public void LoadData()
    {
        tytLastFiveNets.Clear(); // Mevcut verileri temizler
        int count = PlayerPrefs.GetInt("TYT_NetCount", 0); // Kaydedilen net sayýsýný alýr
        Debug.Log("Loading data. TYT_NetCount: " + count);

        // Her bir net deðerini PlayerPrefs'ten yükler ve listeye ekler
        for (int i = 0; i < count; i++)
        {
            float value = PlayerPrefs.GetFloat("TYT_Net" + i, 0);
            tytLastFiveNets.Add(value);
            Debug.Log("Loaded TYT_Net" + i + ": " + value);
        }

        // Verilerin doðru yüklendiðini doðrulamak için log
        Debug.Log("Total TYT_Nets Loaded: " + string.Join(", ", tytLastFiveNets));
    }
}
