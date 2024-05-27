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
    }
}
