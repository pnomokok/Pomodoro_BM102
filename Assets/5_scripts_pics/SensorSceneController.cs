using UnityEngine;
using UnityEngine.SceneManagement;
using RJCP.IO.Ports;
using System.Collections;

public class SensorSceneController : MonoBehaviour
{
    private SerialPortStream serialPort;
    private bool isMeasuring = false;

    void Start()
    {
        serialPort = new SerialPortStream("COM6", 9600);
        serialPort.Open();
    }

    void Update()
    {
        if (isMeasuring && serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine();
                float averageBpm;
                if (float.TryParse(data, out averageBpm))
                {
                    PlayerPrefs.SetFloat("AverageBPM", averageBpm);
                    SceneManager.LoadScene("5.3_sonuc");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }

    public void StartMeasurement()
    {
        if (!isMeasuring)
        {
            isMeasuring = true;
            serialPort.WriteLine("S"); // Ölçümü baþlat
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
