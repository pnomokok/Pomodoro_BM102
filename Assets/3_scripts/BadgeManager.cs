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
}