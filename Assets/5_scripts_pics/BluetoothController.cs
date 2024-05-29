using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BluetoothController : MonoBehaviour
{
    public Button olcButton;
    public GameObject deviceListPanel;
    public GameObject deviceButtonPrefab;

    private bool isConnected = false;
    private string selectedDeviceAddress;
    private Dictionary<string, string> discoveredDevices = new Dictionary<string, string>();

    void Start()
    {
        olcButton.onClick.AddListener(OnOlcButtonClick);
        // Bluetooth baðlantýsýný baþlat
        BluetoothLEHardwareInterface.Initialize(true, false, () => { }, (error) => { });

        // Cihazlarý taramaya baþla
        StartScanningForDevices();
    }

    void StartScanningForDevices()
    {
        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
        {
            if (!discoveredDevices.ContainsKey(address))
            {
                discoveredDevices.Add(address, name);
                AddDeviceButton(address, name);
            }
        }, null, false, false);
    }

    void AddDeviceButton(string address, string name)
    {
        GameObject newButton = Instantiate(deviceButtonPrefab, deviceListPanel.transform);
        newButton.GetComponentInChildren<Text>().text = name;
        newButton.GetComponent<Button>().onClick.AddListener(() => OnDeviceSelected(address, name));
    }

    void OnDeviceSelected(string address, string name)
    {
        selectedDeviceAddress = address;
        Debug.Log("Selected device: " + name + " (" + address + ")");
    }

    void OnOlcButtonClick()
    {
        if (string.IsNullOrEmpty(selectedDeviceAddress))
        {
            Debug.LogError("No device selected.");
            return;
        }

        ConnectToDevice(selectedDeviceAddress);
    }

    void ConnectToDevice(string address)
    {
        BluetoothLEHardwareInterface.ConnectToPeripheral(address, (connectedAddress) => { },
            (address, serviceUUID) => { },
            (address, serviceUUID, characteristicUUID) =>
            {
                isConnected = true;
                Debug.Log("Connected to device");
                StartCoroutine(MeasureHeartRate());
            },
            (address) => { isConnected = false; });
    }

    IEnumerator MeasureHeartRate()
    {
        List<int> bpmReadings = new List<int>();

        for (int i = 0; i < 10; i++) // 10 saniye boyunca veri al
        {
            BluetoothLEHardwareInterface.ReadCharacteristic(selectedDeviceAddress, "YourCharacteristicUUID", (characteristic, data) =>
            {
                if (data != null)
                {
                    int bpm = int.Parse(System.Text.Encoding.ASCII.GetString(data));
                    bpmReadings.Add(bpm);
                }
            });
            yield return new WaitForSeconds(1); // 1 saniye bekle
        }

        if (bpmReadings.Count > 0)
        {
            int averageBPM = (int)bpmReadings.Average();
            string stressLevel = DetermineStressLevel(averageBPM);

            PlayerPrefs.SetInt("Nabiz", averageBPM);
            PlayerPrefs.SetString("StresSeviyesi", stressLevel);

            SceneManager.LoadScene("5.3_sonuc");
        }
    }

    string DetermineStressLevel(int bpm)
    {
        if (bpm > 90 && bpm <= 110)
        {
            return "Orta derecede stresli";
        }
        else if (bpm > 110)
        {
            return "Yüksek derecede stresli";
        }
        else
        {
            return "Normal";
        }
    }
}
