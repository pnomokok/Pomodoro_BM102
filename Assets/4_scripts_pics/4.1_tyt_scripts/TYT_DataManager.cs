using System.Collections.Generic;
using UnityEngine;

public class TYT_DataManager : MonoBehaviour
{
    // Singleton �rne�i i�in property
    public static TYT_DataManager tytInstance { get; private set; }

    // Son be� net say�s�n� tutan liste
    public List<float> tytLastFiveNets = new List<float>();

    private void Awake()
    {
        // E�er Singleton �rne�i yoksa, bu nesneyi Singleton olarak belirler ve yok edilmemesini sa�lar
        if (tytInstance == null)
        {
            tytInstance = this;
            DontDestroyOnLoad(gameObject); // Sahne de�i�ti�inde bu nesneyi yok etmemeye yarar
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
        if (tytLastFiveNets.Count >= 5)
        {
            tytLastFiveNets.RemoveAt(0);
        }
        tytLastFiveNets.Add(net); // Yeni net de�erini ekler
        SaveData(); // Verileri kaydeder
    }

    // Verileri kaydeder
    public void SaveData()
    {
        // Net say�s�n� kaydeder
        PlayerPrefs.SetInt("TYT_NetCount", tytLastFiveNets.Count);

        // Her bir net de�erini PlayerPrefs'e kaydeder
        for (int i = 0; i < tytLastFiveNets.Count; i++)
        {
            PlayerPrefs.SetFloat("TYT_Net" + i, tytLastFiveNets[i]);
            Debug.Log("Saved TYT_Net" + i + ": " + tytLastFiveNets[i]);
        }
        PlayerPrefs.Save(); // De�i�iklikleri kaydeder
        Debug.Log("Data saved");
    }

    // Verileri y�kler
    public void LoadData()
    {
        tytLastFiveNets.Clear(); // Mevcut verileri temizler
        int count = PlayerPrefs.GetInt("TYT_NetCount", 0); // Kaydedilen net say�s�n� al�r
        Debug.Log("Loading data. TYT_NetCount: " + count);

        // Her bir net de�erini PlayerPrefs'ten y�kler ve listeye ekler
        for (int i = 0; i < count; i++)
        {
            float value = PlayerPrefs.GetFloat("TYT_Net" + i, 0);
            tytLastFiveNets.Add(value);
            Debug.Log("Loaded TYT_Net" + i + ": " + value);
        }

        // Verilerin do�ru y�klendi�ini do�rulamak i�in log
        Debug.Log("Total TYT_Nets Loaded: " + string.Join(", ", tytLastFiveNets));
    }
}
