//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;

//public class PomodoroTimer : MonoBehaviour
//{
//    public TMP_InputField workTimeInput;
//    public TMP_InputField breakTimeInput;
//    public TMP_InputField cycleCountInput;

//    public TextMeshProUGUI timerText;
//    public TextMeshProUGUI setStatusText; // Yeni TextMeshPro alan�
//    public Button startButton;
//    public Button stopButton;
//    public Button resetButton;
//    public Button saveButton;

//    public Image progressBar; // Progress bar i�in Image referans�

//    private PomodoroManager manager;

//    private void Start()
//    {
//        manager = PomodoroManager.Instance;

//        if (manager == null)
//        {
//            Debug.LogError("PomodoroManager instance is not found.");
//            return;
//        }

//        startButton.onClick.AddListener(StartTimer);
//        stopButton.onClick.AddListener(StopTimer);
//        resetButton.onClick.AddListener(ResetTimer);
//        saveButton.onClick.AddListener(SaveSettings);

//        manager.LoadSettings();
//        LoadInputs();

//        UpdateTimerText();
//        UpdateProgressBar();
//        UpdateSetStatusText(); // Ba�lang��ta set durumu g�ncelle
//    }

//    private void Update()
//    {
//        if (manager == null)
//        {
//            return;
//        }

//        if (manager.timerRunning)
//        {
//            manager.currentTime -= Time.deltaTime;

//            if (manager.currentTime <= 0)
//            {
//                if (manager.isWorking)
//                {
//                    manager.currentCycle++;
//                    if (manager.currentCycle >= manager.cycleCount)
//                    {
//                        manager.timerRunning = false;

//                        timerText.text = "Done!";

//                        //timerText.text = string.Format("{0:00}:00", Mathf.FloorToInt(manager.workTime / 60F)); //�al��ma bitti�inde text=  son girilen work time oluyor

//                        progressBar.fillAmount = 1; // Zamanlay�c� bitti�inde doluluk oran� 1 olsun
//                        setStatusText.text = "Completed"; // Set durumu bitti�inde g�ncelle
//                        return;
//                    }
//                    manager.isWorking = false;
//                    manager.currentTime = manager.breakTime;
//                }
//                else
//                {
//                    manager.isWorking = true;
//                    manager.currentTime = manager.workTime;
//                }
//            }

//            UpdateTimerText();
//            UpdateProgressBar();
//            UpdateSetStatusText(); // Set durumunu g�ncelle
//        }
//    }

//    private void StartTimer()    //�al�l��yor, text denemek i�in yoruma al�nd�
//    {
//        if (manager != null && !manager.timerRunning)
//        {
//            // Zamanlay�c� durdurulduysa mevcut s�reyi koruyun
//            if (manager.currentTime <= 0)
//            {
//                manager.currentTime = manager.isWorking ? manager.workTime : manager.breakTime;
//            }
//            manager.timerRunning = true;
//            UpdateProgressBar(); // Ba�lang��ta progress bar'� g�ncelle
//            UpdateSetStatusText(); // Set durumunu ba�lat�rken g�ncelle
//        }
//    }



//    private void StopTimer()
//    {
//        if (manager != null)
//        {
//            manager.timerRunning = false;
//        }
//    }

//    private void ResetTimer()
//    {
//        if (manager != null)
//        {
//            manager.timerRunning = false;
//            manager.currentTime = manager.workTime;
//            manager.currentCycle = 0;
//            manager.isWorking = true;
//            UpdateTimerText();
//            UpdateProgressBar(); // Reset'te progress bar'� s�f�rla
//            UpdateSetStatusText(); // Reset'te set durumunu s�f�rla
//        }
//    }

//    private void SaveSettings()
//    {
//        if (manager != null)
//        {
//            float workTime = float.Parse(workTimeInput.text) * 60;
//            float breakTime = float.Parse(breakTimeInput.text) * 60;
//            int cycles = int.Parse(cycleCountInput.text);

//            manager.SaveSettings(workTime, breakTime, cycles);
//            manager.LoadSettings();
//            ResetTimer();
//        }
//    }

//    private void LoadInputs()
//    {
//        if (manager != null)
//        {
//            workTimeInput.text = (manager.workTime / 60).ToString();
//            breakTimeInput.text = (manager.breakTime / 60).ToString();
//            cycleCountInput.text = manager.cycleCount.ToString();
//        }
//    }

//    private void UpdateTimerText()
//    {
//        if (manager != null)
//        {
//            int minutes = Mathf.FloorToInt(manager.currentTime / 60F);
//            int seconds = Mathf.FloorToInt(manager.currentTime % 60F);
//            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
//        }
//    }

//    private void UpdateProgressBar()
//    {
//        if (manager != null)
//        {
//            float totalTime = manager.isWorking ? manager.workTime : manager.breakTime;
//            progressBar.fillAmount = manager.currentTime / totalTime;
//        }
//    }



//    private void UpdateSetStatusText()
//    {
//        if (manager != null)
//        {
//            string status;
//            if (manager.isWorking)
//            {
//                status = "DERS";
//            }
//            else
//            {
//                status = "MOLA";
//            }

//            string setText;
//            if (manager.currentCycle == 0 && !manager.timerRunning && !manager.isWorking)
//            {
//                setText = "HAZIRSAN BASLA";
//            }
//            else
//            {
//                setText = $"{manager.currentCycle + (manager.isWorking ? 1 : 0)}. SET {status}";
//            }

//            setStatusText.text = setText;
//        }
//    }





//    private void OnApplicationPause(bool pauseStatus)
//    {
//        if (manager != null)
//        {
//            if (pauseStatus)
//            {
//                manager.SaveState();
//            }
//            else
//            {
//                manager.LoadState();
//                UpdateTimerText();
//                UpdateProgressBar();
//                UpdateSetStatusText();
//            }
//        }
//    }
//}



using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PomodoroTimer : MonoBehaviour
{
    public TMP_InputField workTimeInput;
    public TMP_InputField breakTimeInput;
    public TMP_InputField cycleCountInput;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI setStatusText;
    //public TextMeshProUGUI totalWorkTimeText; // Toplam �al��ma s�resi i�in TextMeshPro alan�
    public Button startButton;
    public Button stopButton;
    public Button resetButton;
    public Button saveButton;

    public Image progressBar;

    private PomodoroManager manager;

    private void Start()
    {
        manager = PomodoroManager.Instance;

        if (manager == null)
        {
            Debug.LogError("PomodoroManager instance is not found.");
            return;
        }

        startButton.onClick.AddListener(StartTimer);
        stopButton.onClick.AddListener(StopTimer);
        resetButton.onClick.AddListener(ResetTimer);
        saveButton.onClick.AddListener(SaveSettings);

        manager.LoadSettings();
        LoadInputs();

        UpdateTimerText();
        UpdateProgressBar();
        UpdateSetStatusText();
        //UpdateTotalWorkTimeText(); // Ba�lang��ta toplam �al��ma s�resini g�ncelle
    }

    private void Update()
    {
        if (manager == null)
        {
            return;
        }

        if (manager.timerRunning)
        {
            manager.currentTime -= Time.deltaTime;

            if (manager.isWorking)
            {
                manager.totalWorkTime += Time.deltaTime; // Toplam �al��ma s�resini g�ncelle
                Debug.Log("toplam cal�sma s�resi: " + manager.totalWorkTime);
                // UpdateTotalWorkTimeText(); // Ekranda toplam �al��ma s�resini g�ncelle
            }

            if (manager.currentTime <= 0)
            {
                if (manager.isWorking)
                {
                    manager.currentCycle++;
                    if (manager.currentCycle >= manager.cycleCount)
                    {
                        manager.timerRunning = false;
                        timerText.text = "Done!";
                        progressBar.fillAmount = 1;
                        setStatusText.text = "Completed";
                        return;
                    }
                    manager.isWorking = false;
                    manager.currentTime = manager.breakTime;
                }
                else
                {
                    manager.isWorking = true;
                    manager.currentTime = manager.workTime;
                }
            }

            UpdateTimerText();
            UpdateProgressBar();
            UpdateSetStatusText();
        }
    }

    private void StartTimer()
    {
        if (manager != null && !manager.timerRunning)
        {
            if (manager.currentTime <= 0)
            {
                manager.currentTime = manager.isWorking ? manager.workTime : manager.breakTime;
            }
            manager.timerRunning = true;
            UpdateProgressBar();
            UpdateSetStatusText();
        }
    }

    private void StopTimer()
    {
        if (manager != null)
        {
            manager.timerRunning = false;
        }
    }

    private void ResetTimer()
    {
        if (manager != null)
        {
            manager.timerRunning = false;
            manager.currentTime = manager.workTime;
            manager.currentCycle = 0;
            manager.isWorking = true;
            UpdateTimerText();
            UpdateProgressBar();
            UpdateSetStatusText();
        }
    }

    private void SaveSettings()
    {
        if (manager != null)
        {
            float workTime = float.Parse(workTimeInput.text) * 60;
            float breakTime = float.Parse(breakTimeInput.text) * 60;
            int cycles = int.Parse(cycleCountInput.text);

            manager.SaveSettings(workTime, breakTime, cycles);
            manager.LoadSettings();
            ResetTimer();
        }
    }

    private void LoadInputs()
    {
        if (manager != null)
        {
            workTimeInput.text = (manager.workTime / 60).ToString();
            breakTimeInput.text = (manager.breakTime / 60).ToString();
            cycleCountInput.text = manager.cycleCount.ToString();
        }
    }

    private void UpdateTimerText()
    {
        if (manager != null)
        {
            int minutes = Mathf.FloorToInt(manager.currentTime / 60F);
            int seconds = Mathf.FloorToInt(manager.currentTime % 60F);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void UpdateProgressBar()
    {
        if (manager != null)
        {
            float totalTime = manager.isWorking ? manager.workTime : manager.breakTime;
            progressBar.fillAmount = manager.currentTime / totalTime;
        }
    }

    private void UpdateSetStatusText()
    {
        if (manager != null)
        {
            string status = manager.isWorking ? "DERS" : "MOLA";
            string setText = (manager.currentCycle == 0 && !manager.timerRunning && !manager.isWorking)
                ? "HAZIRSAN BASLA"
                : $"{manager.currentCycle + (manager.isWorking ? 1 : 0)}. SET {status}";

            setStatusText.text = setText;
        }
    }

    // private void UpdateTotalWorkTimeText()
    //{
    //    if (manager != null)
    //    {
    //        int totalMinutes = Mathf.FloorToInt(manager.totalWorkTime / 60F);
    //        int totalSeconds = Mathf.FloorToInt(manager.totalWorkTime % 60F);
    //        totalWorkTimeText.text = string.Format("Toplam �al��ma S�resi: {0:00}:{1:00}", totalMinutes, totalSeconds);
    //    }
    //}

    private void OnApplicationPause(bool pauseStatus)
    {
        if (manager != null)
        {
            if (pauseStatus)
            {
                manager.SaveState();
            }
            else
            {
                manager.LoadState();
                UpdateTimerText();
                UpdateProgressBar();
                UpdateSetStatusText();
                // UpdateTotalWorkTimeText(); // Uygulama geri geldi�inde toplam �al��ma s�resini g�ncelle
            }
        }
    }
}