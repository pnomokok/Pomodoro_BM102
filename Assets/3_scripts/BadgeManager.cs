//using UnityEngine;
//using UnityEngine.UI;

//public class BadgeManager : MonoBehaviour
//{
//    public Image[] badgeImages; // Rozet görüntüleri
//    public float[] badgeThresholds; // Rozetlerin kilidini açmak için gereken süreler
//    public Sprite[] colorfulBadges; // Renkli rozetlerin sprite'larý

//    private PomodoroManager manager;

//    private void Start()
//    {
//        manager = PomodoroManager.Instance;

//        if (manager == null)
//        {
//            Debug.LogError("PomodoroManager instance is not found.");
//            return;
//        }

//        UpdateBadges();
//    }

//    private void Update()
//    {
//        UpdateBadges();
//    }

//    private void UpdateBadges()
//    {
//        for (int i = 0; i < badgeImages.Length; i++)
//        {
//            if (manager.totalWorkTime >= badgeThresholds[i])
//            {
//                // Rozet sprite'ýný renkli hale getir
//                badgeImages[i].sprite = colorfulBadges[i];
//            }
//        }
//    }
//}

using UnityEngine;
using UnityEngine.UI;

public class BadgeManager : MonoBehaviour
{
    public Image[] badgeImages; // Rozet görüntüleri
    public float[] badgeThresholds; // Rozetlerin kilidini açmak için gereken süreler
    public Sprite[] colorfulBadges; // Renkli rozetlerin sprite'larý

    private PomodoroManager manager;

    private void Start()
    {
        manager = PomodoroManager.Instance;

        if (manager == null)
        {
            Debug.LogError("PomodoroManager instance is not found.");
            return;
        }
        LoadBadges();
        UpdateBadges();
    }

    private void Update()
    {
        UpdateBadges();
    }

    private void UpdateBadges()
    {
        for (int i = 0; i < badgeImages.Length; i++)
        {
            if (manager.totalWorkTime >= badgeThresholds[i])
            {
                // Rozet sprite'ýný renkli hale getir
                badgeImages[i].sprite = colorfulBadges[i];
            }
        }
    }
    public void SaveBadges()
    {
        for (int i = 0; i < badgeImages.Length; i++)
        {
            PlayerPrefs.SetInt("Badge_" + i, manager.totalWorkTime >= badgeThresholds[i] ? 1 : 0);
        }
        PlayerPrefs.Save();
    }
    public void LoadBadges()
    {
        for (int i = 0; i < badgeImages.Length; i++)
        {
            if (PlayerPrefs.GetInt("Badge_" + i, 0) == 1)
            {
                badgeImages[i].sprite = colorfulBadges[i];
            }
        }
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (manager != null)
        {
            if (pauseStatus)
            {
                SaveBadges();
            }
            else
            {
                LoadBadges();
            }
        }
    }
    private void OnDisable()
    {
        SaveBadges();
    }
}