
//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;

//public class PomodoroTimer : MonoBehaviour
//{
//    public TMP_InputField workTimeInput;
//    public TMP_InputField breakTimeInput;
//    public TMP_InputField cycleCountInput;

//    public TextMeshProUGUI timerText;
//    public TextMeshProUGUI setStatusText;
//    //public TextMeshProUGUI totalWorkTimeText; // Toplam çalýþma süresi için TextMeshPro alaný
//    public Button startButton;
//    public Button stopButton;
//    public Button resetButton;
//    public Button saveButton;

//    public Image progressBar;

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
//        UpdateSetStatusText();
//        //UpdateTotalWorkTimeText(); // Baþlangýçta toplam çalýþma süresini güncelle
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

//            if (manager.isWorking)
//            {
//                manager.totalWorkTime += Time.deltaTime; // Toplam çalýþma süresini güncelle
//                Debug.Log("toplam calýsma süresi: " + manager.totalWorkTime);
//                // UpdateTotalWorkTimeText(); // Ekranda toplam çalýþma süresini güncelle
//            }

//            if (manager.currentTime <= 0)
//            {
//                if (manager.isWorking)
//                {
//                    manager.currentCycle++;
//                    if (manager.currentCycle >= manager.cycleCount)
//                    {
//                        manager.timerRunning = false;
//                        timerText.text = "Done!";
//                        progressBar.fillAmount = 1;
//                        setStatusText.text = "Completed";
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
//            UpdateSetStatusText();
//        }
//    }

//    private void StartTimer()
//    {
//        if (manager != null && !manager.timerRunning)
//        {
//            if (manager.currentTime <= 0)
//            {
//                manager.currentTime = manager.isWorking ? manager.workTime : manager.breakTime;
//            }
//            manager.timerRunning = true;
//            UpdateProgressBar();
//            UpdateSetStatusText();
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
//            UpdateProgressBar();
//            UpdateSetStatusText();
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
//            string status = manager.isWorking ? "DERS" : "MOLA";
//            string setText = (manager.currentCycle == 0 && !manager.timerRunning && !manager.isWorking)
//                ? "HAZIRSAN BASLA"
//                : $"{manager.currentCycle + (manager.isWorking ? 1 : 0)}. SET {status}";

//            setStatusText.text = setText;
//        }
//    }

//    // private void UpdateTotalWorkTimeText()
//    //{
//    //    if (manager != null)
//    //    {
//    //        int totalMinutes = Mathf.FloorToInt(manager.totalWorkTime / 60F);
//    //        int totalSeconds = Mathf.FloorToInt(manager.totalWorkTime % 60F);
//    //        totalWorkTimeText.text = string.Format("Toplam Çalýþma Süresi: {0:00}:{1:00}", totalMinutes, totalSeconds);
//    //    }
//    //}

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
//                // UpdateTotalWorkTimeText(); // Uygulama geri geldiðinde toplam çalýþma süresini güncelle
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
    public TextMeshProUGUI errorMessageText; // Hata mesajlarý için TextMeshPro alaný
    //public TextMeshProUGUI totalWorkTimeText; // Toplam çalýþma süresi için TextMeshPro alaný
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

        workTimeInput.onValueChanged.AddListener(delegate { ValidateInputFields(); });
        breakTimeInput.onValueChanged.AddListener(delegate { ValidateInputFields(); });
        cycleCountInput.onValueChanged.AddListener(delegate { ValidateInputFields(); });

        manager.LoadSettings();
        LoadInputs();

        UpdateTimerText();
        UpdateProgressBar();
        UpdateSetStatusText();
        //UpdateTotalWorkTimeText(); // Baþlangýçta toplam çalýþma süresini güncelle
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
                manager.totalWorkTime += Time.deltaTime; // Toplam çalýþma süresini güncelle
                Debug.Log("toplam calýsma süresi: " + manager.totalWorkTime);
                // UpdateTotalWorkTimeText(); // Ekranda toplam çalýþma süresini güncelle
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
            ClearErrorMessage(); // Reset iþleminde hata mesajýný temizle
        }
    }

    private void SaveSettings()
    {
        if (manager != null)
        {
            if (ValidateInputs())
            {
                float workTime = float.Parse(workTimeInput.text) * 60;
                float breakTime = float.Parse(breakTimeInput.text) * 60;
                int cycles = int.Parse(cycleCountInput.text);

                manager.SaveSettings(workTime, breakTime, cycles);
                manager.LoadSettings();
                ResetTimer();
                EnableButtons(true); // Giriþler geçerli olduðunda butonlarý etkinleþtir
            }
            else
            {
                EnableButtons(false); // Giriþler geçersiz olduðunda butonlarý devre dýþý býrak
            }
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

    private void ValidateInputFields()
    {
        if (ValidateInputs())
        {
            ClearErrorMessage();
            EnableButtons(true); // Giriþler geçerli olduðunda butonlarý etkinleþtir
        }
        else
        {
            EnableButtons(false); // Giriþler geçersiz olduðunda butonlarý devre dýþý býrak
        }
    }

    private bool ValidateInputs()
    {
        ClearErrorMessage(); // Hata mesajýný temizle

        float workTime;
        float breakTime;
        int cycles;

        if (!float.TryParse(workTimeInput.text, out workTime) || !float.TryParse(breakTimeInput.text, out breakTime) || !int.TryParse(cycleCountInput.text, out cycles))
        {
            ShowErrorMessage("Geçersiz deðer girildi. Yalnýzca sayýsal deðerler kabul edilir.");
            return false;
        }

        if (workTime <= 0 || breakTime <= 0 || cycles <= 0)
        {
            ShowErrorMessage("Deðerler sýfýrdan büyük olmalýdýr.");
            return false;
        }

        return true;
    }

    private void ShowErrorMessage(string message)
    {
        errorMessageText.text = message;
        errorMessageText.gameObject.SetActive(true);
    }

    private void ClearErrorMessage()
    {
        errorMessageText.text = "";
        errorMessageText.gameObject.SetActive(false);
    }

    private void EnableButtons(bool enable)
    {
        startButton.interactable = enable;
        stopButton.interactable = enable;
        resetButton.interactable = enable;
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
    //        totalWorkTimeText.text = string.Format("Toplam Çalýþma Süresi: {0:00}:{1:00}", totalMinutes, totalSeconds);
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
                // UpdateTotalWorkTimeText(); // Uygulama geri geldiðinde toplam çalýþma süresini güncelle
            }
        }
    }
}
