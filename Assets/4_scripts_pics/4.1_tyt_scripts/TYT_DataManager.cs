using System.Collections.Generic;
using UnityEngine;

public class TYT_DataManager : MonoBehaviour
{
    public static TYT_DataManager tytInstance { get; private set; }
    public List<float> tytLastFiveNets = new List<float>();

    private void Awake()
    {
        if (tytInstance == null)
        {
            tytInstance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddNet(float net)
    {
        if (tytLastFiveNets.Count >= 5)
        {
            tytLastFiveNets.RemoveAt(0);
        }
        tytLastFiveNets.Add(net);
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("NetCount", tytLastFiveNets.Count);
        for (int i = 0; i < tytLastFiveNets.Count; i++)
        {
            PlayerPrefs.SetFloat("Net" + i, tytLastFiveNets[i]);
            Debug.Log("Saved Net" + i + ": " + tytLastFiveNets[i]);
        }
        PlayerPrefs.Save();
        Debug.Log("Data saved");
    }

    public void LoadData()
    {
        tytLastFiveNets.Clear();
        int count = PlayerPrefs.GetInt("NetCount", 0);
        Debug.Log("Loading data. NetCount: " + count);
        for (int i = 0; i < count; i++)
        {
            //tytLastFiveNets.Add(PlayerPrefs.GetFloat("Net" + i, 0));

            float value = PlayerPrefs.GetFloat("Net" + i, 0);

            tytLastFiveNets.Add(value);
            Debug.Log("Loaded Net" + i + ": " + value);
        }
        // Verify if data is loaded correctly
        Debug.Log("Total Nets Loaded: " + string.Join(", ", tytLastFiveNets));
    }
}