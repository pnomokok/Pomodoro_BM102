
using UnityEngine;
using UnityEngine.UI;


public class StressBarManager : MonoBehaviour
{
    public Image stressBar;
    public Image indicator;


    private int maxScore = 21; // Maksimum skor
    private int minScore = 0;  // Minimum skor

    // Bu pozisyonlarý stres barýnýn en alt ve en üst pozisyonlarýna göre ayarlayýn
    public float minYPosition = -830f;
    public float maxYPosition = 520f;

    void Start()
    {
        // Kullanýcýnýn anketten aldýðý toplam skor
        int totalScore = SurveyData.totalScore;

        // Stres barý üzerinde skoru göster
        UpdateStressBar(totalScore);
    }

    void UpdateStressBar(int score)
    {
        // Skoru min-max aralýðýna ölçekle
        float normalizedScore = Mathf.InverseLerp(minScore, maxScore, score);

        // Göstergeyi stres barý üzerinde uygun pozisyona yerleþtir
        RectTransform indicatorRect = indicator.GetComponent<RectTransform>();

        // Manuel konumlandýrma için hesaplama
        float indicatorPositionY = Mathf.Lerp(minYPosition, maxYPosition, normalizedScore);

        // Indicator'ü doðru pozisyona yerleþtir
        indicatorRect.anchoredPosition = new Vector2(indicatorRect.anchoredPosition.x, indicatorPositionY);


    }
}