using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public List<float> lastFiveNets = new List<float>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddNet(float net)
    {
        if (lastFiveNets.Count >= 5)
        {
            lastFiveNets.RemoveAt(0);
        }
        lastFiveNets.Add(net);
    }
}
