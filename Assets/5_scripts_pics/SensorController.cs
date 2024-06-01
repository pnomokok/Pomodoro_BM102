using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class SensorController : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM3", 9600);

    void Start()
    {
        if (!serialPort.IsOpen)
        {
            serialPort.Open();
        }
    }

    void OnDestroy()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    public void StartMeasurement()
    {
        if (serialPort.IsOpen)
        {
            serialPort.WriteLine("start");
            StartCoroutine(ReceiveData());
        }
    }

    private IEnumerator ReceiveData()
    {
        while (true)
        {
            if (serialPort.BytesToRead > 0)
            {
                string data = serialPort.ReadLine();
                ProcessData(data);
                yield break;
            }
            yield return null;
        }
    }

    private void ProcessData(string data)
    {
        if (data == "Error")
        {
            Debug.LogError("Measurement Error!");
        }
        else
        {
            PlayerPrefs.SetString("HeartRate", data);
            SceneManager.LoadScene("5.3_sonuc");
        }
    }
}
