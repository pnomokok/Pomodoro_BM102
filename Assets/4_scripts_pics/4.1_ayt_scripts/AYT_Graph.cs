using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AYT_Graph : MonoBehaviour
{
    // Editor üzerinden ayarlanabilir deðiþkenler
    [SerializeField] private Sprite circleSprite; // Nokta gösterimi için kullanýlacak sprite
    [SerializeField] private GameObject linePrefab; // Çizgi prefab'ý referansý
    [SerializeField] private GameObject textPrefab; // Metin prefab'ý referansý

    // Özel bileþenler ve veri listeleri
    private RectTransform graphContainer; // Grafiðin yerleþtirileceði container
    private List<GameObject> circleList = new List<GameObject>(); // Noktalarýn listesi
    private List<GameObject> lineList = new List<GameObject>(); // Çizgilerin listesi
    private List<GameObject> textList = new List<GameObject>(); // Metinlerin listesi
    private CustomLineDrawer lineDrawer; // Çizgi çizim bileþeni

    private void Start()
    {
        // GraphContainer nesnesini bulup referans alýr
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();

        // GraphContainer nesnesine CustomLineDrawer bileþeni ekler
        lineDrawer = graphContainer.gameObject.AddComponent<CustomLineDrawer>();

        // Çizgi rengini ayarlar
        lineDrawer.color = new Color32(0x78, 0x72, 0xDE, 0xFF); // #7872DE rengini ayarlar

        // Grafiði yeniler
        RefreshGraph();
    }

    // Yeni bir nokta oluþturur
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

    // Grafiði günceller
    public void UpdateGraph(List<float> values)
    {
        // Eski öðeleri temizler
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

        // Noktalar arasýndaki yatay mesafeyi ayarlar
        float xSpacing = 150f;
        List<Vector2> positions = new List<Vector2>();

        // Yeni noktalar ve çizgiler oluþturur
        for (int i = 0; i < values.Count; i++)
        {
            float xPosition = xSpacing * (i + 1);
            float yPosition = values[i] * 10.6f + 18; // Y pozisyonunu hesaplar
            GameObject circle = CreateCircle(new Vector2(xPosition, yPosition));
            circleList.Add(circle);

            // Anchored konum local konuma dönüþtürülür
            Vector2 anchoredPos = new Vector2(xPosition, yPosition);
            positions.Add(anchoredPos);

            // Çizgi oluþturma
            GameObject line = Instantiate(linePrefab, graphContainer);
            RectTransform lineRectTransform = line.GetComponent<RectTransform>();
            lineRectTransform.anchoredPosition = new Vector2(-472, yPosition - 485);
            lineList.Add(line);

            // Metin oluþturma
            GameObject text = Instantiate(textPrefab, graphContainer);
            RectTransform textRectTransform = text.GetComponent<RectTransform>();
            textRectTransform.anchoredPosition = new Vector2(xPosition - 488, yPosition - 445);
            TextMeshProUGUI textComponent = text.GetComponent<TextMeshProUGUI>();
            textComponent.text = values[i].ToString("F2"); // Net deðeri yazma
            textList.Add(text);
        }

        // Çizgi noktalarýný ayarlar ve çizimi tetikler
        lineDrawer.points = positions;
        lineDrawer.SetVerticesDirty(); // Çizgiyi yeniden çizmeyi tetikler
    }

    // Grafiði yeniler
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
