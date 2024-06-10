using System.Collections.Generic;
using UnityEngine;
public class AYT_DataManager : MonoBehaviour
{
    // Singleton �rne�i i�in property
    public static AYT_DataManager aytInstance { get; private set; }

    // Son be� net say�s�n� tutan liste
    public List<float> aytLastFiveNets = new List<float>();

    private void Awake()
    {
        // E�er Singleton �rne�i yoksa, bu nesneyi Singleton olarak belirler ve yok edilmemesini sa�lar
        if (aytInstance == null)
        {
            aytInstance = this;
            DontDestroyOnLoad(gameObject); // Sahne de�i�ti�inde bu nesneyi yok etme
            LoadData(); // Verileri y�kler
        }
        else
        {
            // E�er Singleton �rne�i zaten varsa, bu nesneyi yok eder
            Destroy(gameObject);
        }
    }

    // Yeni bir net de�eri ekler
    public void AddNet(float net)
    {
        // E�er net listesi 5'ten fazla elemana sahipse, ilk eleman� siler
        if (aytLastFiveNets.Count >= 5)
        {
            aytLastFiveNets.RemoveAt(0);
        }
        aytLastFiveNets.Add(net); // Yeni net de�erini ekler
        SaveData(); // Verileri kaydeder
    }

    // Verileri kaydeder
    public void SaveData()
    {
        // Net say�s�n� kaydeder
        PlayerPrefs.SetInt("AYT_NetCount", aytLastFiveNets.Count);

        // Her bir net de�erini PlayerPrefs'e kaydeder
        for (int i = 0; i < aytLastFiveNets.Count; i++)
        {
            PlayerPrefs.SetFloat("AYT_Net" + i, aytLastFiveNets[i]);
            Debug.Log("Saved AYT_Net" + i + ": " + aytLastFiveNets[i]);
        }
        PlayerPrefs.Save(); // De�i�iklikleri kaydeder
        Debug.Log("Data saved");
    }

    // Verileri y�kler
    public void LoadData()
    {
        aytLastFiveNets.Clear(); // Mevcut verileri temizler
        int count = PlayerPrefs.GetInt("AYT_NetCount", 0); // Kaydedilen net say�s�n� al�r
        Debug.Log("Loading data. AYT_NetCount: " + count);

        // Her bir net de�erini PlayerPrefs'ten y�kler ve listeye ekler
        for (int i = 0; i < count; i++)
        {
            float value = PlayerPrefs.GetFloat("AYT_Net" + i, 0);
            aytLastFiveNets.Add(value);
            Debug.Log("Loaded AYT_Net" + i + ": " + value);
        }

        // Verilerin do�ru y�klendi�ini do�rulamak i�in log
        Debug.Log("Total AYT_Nets Loaded: " + string.Join(", ", aytLastFiveNets));
    }
}
