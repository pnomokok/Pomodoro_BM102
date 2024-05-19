using UnityEngine;
using TMPro;

public class TimerSettings : MonoBehaviour
{
    public TMP_InputField workTimeInput;
    public TMP_InputField breakTimeInput;
    public TMP_InputField setCountInput;
    public Timer timerScript; // Timer script referansý

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
            // Timer scriptine ayarlarý aktar
            timerScript.workTimeLimit = workTime * 60; // Çalýþma süresi (saniye cinsinden)
            timerScript.breakTimeLimit = breakTime * 60; // Mola süresi (saniye cinsinden)
            timerScript.setCount = setCount; // Set sayýsý
            Debug.Log($"Work Time: {workTime} minutes, Break Time: {breakTime} minutes, Set Count: {setCount}");
        }
        else
        {
            Debug.LogError("Invalid input. Please enter numeric values.");
        }
    }
}
