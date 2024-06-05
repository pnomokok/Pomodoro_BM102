
using UnityEngine;
using UnityEngine.UI;


public class StressBarController : MonoBehaviour
{
    public Image stressBar;
    public Image indicator;


    private int maxScore = 120; // Maksimum skor
    private int minScore = 50;  // Minimum skor

    // Bu pozisyonlar� stres bar�n�n en alt ve en �st pozisyonlar�na g�re ayarlay�n
    public float minYPosition = -830f;
    public float maxYPosition = 520f;

    void Start()
    {
        // Kullan�c�n�n anketten ald��� toplam skor
        int heartRate = PlayerPrefs.GetInt("HeartRate");

        if (heartRate > 120)
        {
            heartRate = 120;
        }

        // Stres bar� �zerinde skoru g�ster
        UpdateStressBar(heartRate);
    }

    void UpdateStressBar(int score)
    {
        // Skoru min-max aral���na �l�ekle
        float normalizedScore = Mathf.InverseLerp(minScore, maxScore, score);

        // G�stergeyi stres bar� �zerinde uygun pozisyona yerle�tir
        RectTransform indicatorRect = indicator.GetComponent<RectTransform>();

        // Manuel konumland�rma i�in hesaplama
        float indicatorPositionY = Mathf.Lerp(minYPosition, maxYPosition, normalizedScore);

        // Indicator'� do�ru pozisyona yerle�tir
        indicatorRect.anchoredPosition = new Vector2(indicatorRect.anchoredPosition.x, indicatorPositionY);


    }
}