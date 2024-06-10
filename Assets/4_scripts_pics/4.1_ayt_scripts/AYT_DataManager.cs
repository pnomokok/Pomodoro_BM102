using System.Collections.Generic;
using UnityEngine;
public class AYT_DataManager : MonoBehaviour
{
    // Singleton örneði için property
    public static AYT_DataManager aytInstance { get; private set; }

    // Son beþ net sayýsýný tutan liste
    public List<float> aytLastFiveNets = new List<float>();

    private void Awake()
    {
        // Eðer Singleton örneði yoksa, bu nesneyi Singleton olarak belirler ve yok edilmemesini saðlar
        if (aytInstance == null)
        {
            aytInstance = this;
            DontDestroyOnLoad(gameObject); // Sahne deðiþtiðinde bu nesneyi yok etme
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
        if (aytLastFiveNets.Count >= 5)
        {
            aytLastFiveNets.RemoveAt(0);
        }
        aytLastFiveNets.Add(net); // Yeni net deðerini ekler
        SaveData(); // Verileri kaydeder
    }

    // Verileri kaydeder
    public void SaveData()
    {
        // Net sayýsýný kaydeder
        PlayerPrefs.SetInt("AYT_NetCount", aytLastFiveNets.Count);

        // Her bir net deðerini PlayerPrefs'e kaydeder
        for (int i = 0; i < aytLastFiveNets.Count; i++)
        {
            PlayerPrefs.SetFloat("AYT_Net" + i, aytLastFiveNets[i]);
            Debug.Log("Saved AYT_Net" + i + ": " + aytLastFiveNets[i]);
        }
        PlayerPrefs.Save(); // Deðiþiklikleri kaydeder
        Debug.Log("Data saved");
    }

    // Verileri yükler
    public void LoadData()
    {
        aytLastFiveNets.Clear(); // Mevcut verileri temizler
        int count = PlayerPrefs.GetInt("AYT_NetCount", 0); // Kaydedilen net sayýsýný alýr
        Debug.Log("Loading data. AYT_NetCount: " + count);

        // Her bir net deðerini PlayerPrefs'ten yükler ve listeye ekler
        for (int i = 0; i < count; i++)
        {
            float value = PlayerPrefs.GetFloat("AYT_Net" + i, 0);
            aytLastFiveNets.Add(value);
            Debug.Log("Loaded AYT_Net" + i + ": " + value);
        }

        // Verilerin doðru yüklendiðini doðrulamak için log
        Debug.Log("Total AYT_Nets Loaded: " + string.Join(", ", aytLastFiveNets));
    }
}
