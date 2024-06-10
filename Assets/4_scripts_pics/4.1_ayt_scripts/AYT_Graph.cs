using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AYT_Graph : MonoBehaviour
{
    // Editor �zerinden ayarlanabilir de�i�kenler
    [SerializeField] private Sprite circleSprite; // Nokta g�sterimi i�in kullan�lacak sprite
    [SerializeField] private GameObject linePrefab; // �izgi prefab'� referans�
    [SerializeField] private GameObject textPrefab; // Metin prefab'� referans�

    // �zel bile�enler ve veri listeleri
    private RectTransform graphContainer; // Grafi�in yerle�tirilece�i container
    private List<GameObject> circleList = new List<GameObject>(); // Noktalar�n listesi
    private List<GameObject> lineList = new List<GameObject>(); // �izgilerin listesi
    private List<GameObject> textList = new List<GameObject>(); // Metinlerin listesi
    private CustomLineDrawer lineDrawer; // �izgi �izim bile�eni

    private void Start()
    {
        // GraphContainer nesnesini bulup referans al�r
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();

        // GraphContainer nesnesine CustomLineDrawer bile�eni ekler
        lineDrawer = graphContainer.gameObject.AddComponent<CustomLineDrawer>();

        // �izgi rengini ayarlar
        lineDrawer.color = new Color32(0x78, 0x72, 0xDE, 0xFF); // #7872DE rengini ayarlar

        // Grafi�i yeniler
        RefreshGraph();
    }

    // Yeni bir nokta olu�turur
    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(50, 50);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    // Grafi�i g�nceller
    public void UpdateGraph(List<float> values)
    {
        // Eski ��eleri temizler
        foreach (GameObject circle in circleList)
        {
            Destroy(circle);
        }
        foreach (GameObject line in lineList)
        {
            Destroy(line);
        }
        foreach (GameObject text in textList)
        {
            Destroy(text);
        }

        circleList.Clear();
        lineList.Clear();
        textList.Clear();

        // Noktalar aras�ndaki yatay mesafeyi ayarlar
        float xSpacing = 150f;
        List<Vector2> positions = new List<Vector2>();

        // Yeni noktalar ve �izgiler olu�turur
        for (int i = 0; i < values.Count; i++)
        {
            float xPosition = xSpacing * (i + 1);
            float yPosition = values[i] * 10.6f + 18; // Y pozisyonunu hesaplar
            GameObject circle = CreateCircle(new Vector2(xPosition, yPosition));
            circleList.Add(circle);

            // Anchored konum local konuma d�n��t�r�l�r
            Vector2 anchoredPos = new Vector2(xPosition, yPosition);
            positions.Add(anchoredPos);

            // �izgi olu�turma
            GameObject line = Instantiate(linePrefab, graphContainer);
            RectTransform lineRectTransform = line.GetComponent<RectTransform>();
            lineRectTransform.anchoredPosition = new Vector2(-472, yPosition - 485);
            lineList.Add(line);

            // Metin olu�turma
            GameObject text = Instantiate(textPrefab, graphContainer);
            RectTransform textRectTransform = text.GetComponent<RectTransform>();
            textRectTransform.anchoredPosition = new Vector2(xPosition - 488, yPosition - 445);
            TextMeshProUGUI textComponent = text.GetComponent<TextMeshProUGUI>();
            textComponent.text = values[i].ToString("F2"); // Net de�eri yazma
            textList.Add(text);
        }

        // �izgi noktalar�n� ayarlar ve �izimi tetikler
        lineDrawer.points = positions;
        lineDrawer.SetVerticesDirty(); // �izgiyi yeniden �izmeyi tetikler
    }

    // Grafi�i yeniler
    public void RefreshGraph()
    {
        if (AYT_DataManager.aytInstance != null && AYT_DataManager.aytInstance.aytLastFiveNets != null)
        {
            Debug.Log("Graph update called with data: " + string.Join(", ", AYT_DataManager.aytInstance.aytLastFiveNets));
            UpdateGraph(AYT_DataManager.aytInstance.aytLastFiveNets);
        }
        else
        {
            Debug.LogError("AYT_DataManager.aytInstance or aytLastFiveNets is null");
        }
    }
}
