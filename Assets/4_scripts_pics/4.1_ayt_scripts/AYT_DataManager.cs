using System.Collections.Generic;
using UnityEngine;

public class AYT_DataManager : MonoBehaviour
{
    public static AYT_DataManager aytInstance { get; private set; }
    public List<float> aytLastFiveNets = new List<float>();

    private void Awake()
    {
        if (aytInstance == null)
        {
            aytInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddNet(float net)
    {
        if (aytLastFiveNets.Count >= 5)
        {
            aytLastFiveNets.RemoveAt(0);
        }
        aytLastFiveNets.Add(net);
    }
}
