using UnityEngine;
using TMPro;

public class TimerSettings : MonoBehaviour
{
    public TMP_InputField workTimeInput;
    public TMP_InputField breakTimeInput;
    public TMP_InputField setCountInput;
    public timer timerScript; // Timer script referans�

    private int workTime;
    private int breakTime;
    private int setCount;

    public void ApplySettings()
    {
        // InputField'lerden verileri al ve integera �evir
        if (int.TryParse(workTimeInput.text, out workTime) &&
            int.TryParse(breakTimeInput.text, out breakTime) &&
            int.TryParse(setCountInput.text, out setCount))
        {
            // Burada timer scriptine ayarlar� aktarabilirsiniz
            timerScript.timeLimit = workTime * 60; // �al��ma s�resi (saniye cinsinden)
            // Di�er gerekli ayarlamalar� da buradan yapabilirsiniz
            Debug.Log($"Work Time: {workTime} minutes, Break Time: {breakTime} minutes, Set Count: {setCount}");
        }
        else
        {
            Debug.LogError("Invalid input. Please enter numeric values.");
        }
    }
}
