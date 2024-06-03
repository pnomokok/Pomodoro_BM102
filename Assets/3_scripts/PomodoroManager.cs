


using UnityEngine.UI;
using UnityEngine;

public class PomodoroManager : MonoBehaviour
{
    public static PomodoroManager Instance;

    public float workTime;
    public float breakTime;
    public int cycleCount;

    public float currentTime;
    public int currentCycle;
    public bool isWorking;
    public bool timerRunning;

    public float totalWorkTime; // Toplam çalýþma süresi

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

    public void SaveSettings(float workTime, float breakTime, int cycleCount)
    {
        PlayerPrefs.SetFloat("WorkTime", workTime);
        PlayerPrefs.SetFloat("BreakTime", breakTime);
        PlayerPrefs.SetInt("CycleCount", cycleCount);
    }

    public void LoadSettings()
    {
        workTime = PlayerPrefs.GetFloat("WorkTime", 25 * 60); // Varsayýlan 25 dakika
        breakTime = PlayerPrefs.GetFloat("BreakTime", 5 * 60); // Varsayýlan 5 dakika
        cycleCount = PlayerPrefs.GetInt("CycleCount", 4); // Varsayýlan 4 döngü
    }

    public void SaveState()
    {
        PlayerPrefs.SetFloat("CurrentTime", currentTime);
        PlayerPrefs.SetInt("CurrentCycle", currentCycle);
        PlayerPrefs.SetInt("IsWorking", isWorking ? 1 : 0);
        PlayerPrefs.SetInt("TimerRunning", timerRunning ? 1 : 0);
    }

    public void LoadState()
    {
        currentTime = PlayerPrefs.GetFloat("CurrentTime", workTime);
        currentCycle = PlayerPrefs.GetInt("CurrentCycle", 0);
        isWorking = PlayerPrefs.GetInt("IsWorking", 1) == 1;
        timerRunning = PlayerPrefs.GetInt("TimerRunning", 0) == 1;
        totalWorkTime = PlayerPrefs.GetFloat("TotalWorkTime", 0); // Toplam çalýþma süresini yükle

    }
}
