//using UnityEngine.UI;
//using UnityEngine;
//public class PomodoroManager : MonoBehaviour
//{
//    public static PomodoroManager Instance;

//    public float workTime;
//    public float breakTime;
//    public int cycleCount;

//    public float currentTime;
//    public int currentCycle;
//    public bool isWorking;
//    public bool timerRunning;

//    public float totalWorkTime; // Toplam �al��ma s�resi

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    public void SaveSettings(float workTime, float breakTime, int cycleCount)
//    {
//        PlayerPrefs.SetFloat("WorkTime", workTime);
//        PlayerPrefs.SetFloat("BreakTime", breakTime);
//        PlayerPrefs.SetInt("CycleCount", cycleCount);
//    }

//    public void LoadSettings()
//    {
//        workTime = PlayerPrefs.GetFloat("WorkTime", 25 * 60); // Varsay�lan 25 dakika
//        breakTime = PlayerPrefs.GetFloat("BreakTime", 5 * 60); // Varsay�lan 5 dakika
//        cycleCount = PlayerPrefs.GetInt("CycleCount", 4); // Varsay�lan 4 d�ng�
//    }

//    public void SaveState()
//    {
//        PlayerPrefs.SetFloat("CurrentTime", currentTime);
//        PlayerPrefs.SetInt("CurrentCycle", currentCycle);
//        PlayerPrefs.SetInt("IsWorking", isWorking ? 1 : 0);
//        PlayerPrefs.SetInt("TimerRunning", timerRunning ? 1 : 0);
//    }

//    public void LoadState()
//    {
//        currentTime = PlayerPrefs.GetFloat("CurrentTime", workTime);
//        currentCycle = PlayerPrefs.GetInt("CurrentCycle", 0);
//        isWorking = PlayerPrefs.GetInt("IsWorking", 1) == 1;
//        timerRunning = PlayerPrefs.GetInt("TimerRunning", 0) == 1;
//        totalWorkTime = PlayerPrefs.GetFloat("TotalWorkTime", 0); // Toplam �al��ma s�resini y�kle

//    }
//}


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

    public bool timerPaused; // Zamanlay�c� duraklat�ld� m�?

    private PomodoroManager manager;

    public float totalWorkTime; // Toplam �al��ma s�resi

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
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        workTime = PlayerPrefs.GetFloat("WorkTime", 25 * 60); // Varsay�lan 25 dakika
        breakTime = PlayerPrefs.GetFloat("BreakTime", 5 * 60); // Varsay�lan 5 dakika
        cycleCount = PlayerPrefs.GetInt("CycleCount", 4); // Varsay�lan 4 d�ng�
    }

    public void SaveState()
    {
        PlayerPrefs.SetFloat("CurrentTime", currentTime);
        PlayerPrefs.SetInt("CurrentCycle", currentCycle);
        PlayerPrefs.SetInt("IsWorking", isWorking ? 1 : 0);
        PlayerPrefs.SetInt("TimerRunning", timerRunning ? 1 : 0);
        PlayerPrefs.SetFloat("TotalWorkTime", totalWorkTime); // Toplam �al��ma s�resini kaydet
        PlayerPrefs.Save(); // PlayerPrefs verilerini hemen diske yazar
    }

    public void LoadState()
    {
        currentTime = PlayerPrefs.GetFloat("CurrentTime", workTime);
        currentCycle = PlayerPrefs.GetInt("CurrentCycle", 0);
        isWorking = PlayerPrefs.GetInt("IsWorking", 1) == 1;
        timerRunning = PlayerPrefs.GetInt("TimerRunning", 0) == 1;
        totalWorkTime = PlayerPrefs.GetFloat("TotalWorkTime", 0); // Toplam �al��ma s�resini y�kle

    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            // Uygulama geri geldi�inde ve zamanlay�c� duraklat�lmam��sa, zamanlay�c�y� duraklat�lm�� halde tut
            if (!timerPaused && timerRunning)
            {
                timerRunning = false; // Zamanlay�c�y� durdur
                timerPaused = true; // Zamanlay�c� duraklat�ld� olarak i�aretle
            }
            LoadState(); // Kaydedilmi� zamanlay�c� durumunu y�kle
        }
    }
    private void OnApplicationQuit()
    {

        SaveState();

    }
}
