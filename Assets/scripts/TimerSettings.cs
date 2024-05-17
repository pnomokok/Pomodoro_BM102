using UnityEngine;
using TMPro;

public class TimerSettings : MonoBehaviour
{
    public TMP_InputField workTimeInput;
    public TMP_InputField breakTimeInput;
    public TMP_InputField setCountInput;
    public timer timerScript; // Timer script referansý

    private int workTime;
    private int breakTime;
    private int setCount;

    public void ApplySettings()
    {
        // InputField'lerden verileri al ve integera çevir
        if (int.TryParse(workTimeInput.text, out workTime) &&
            int.TryParse(breakTimeInput.text, out breakTime) &&
            int.TryParse(setCountInput.text, out setCount))
        {
            // Burada timer scriptine ayarlarý aktarabilirsiniz
            timerScript.timeLimit = workTime * 60; // Çalýþma süresi (saniye cinsinden)
            // Diðer gerekli ayarlamalarý da buradan yapabilirsiniz
            Debug.Log($"Work Time: {workTime} minutes, Break Time: {breakTime} minutes, Set Count: {setCount}");
        }
        else
        {
            Debug.LogError("Invalid input. Please enter numeric values.");
        }
    }
}
