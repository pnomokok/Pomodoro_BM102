using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private RectTransform graphContainer; // SerializeField kullanarak doðrudan Unity Editor'de atama yapabiliriz.
    private List<GameObject> circleList = new List<GameObject>();

    private void Start()
    {
        if (graphContainer == null)
        {
            Debug.LogError("GraphContainer is not assigned in the Inspector.");
            return;
        }

        UpdateGraph(DataManager.Instance.lastFiveNets);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public void UpdateGraph(List<float> values)
    {
        foreach (GameObject circle in circleList)
        {
            Destroy(circle);
        }
        circleList.Clear();

        float xSpacing = 150f; // Grafik üzerindeki noktalar arasýndaki mesafe
        for (int i = 0; i < values.Count; i++)
        {
            float xPosition = xSpacing * (i + 1);
            float yPosition = values[i] * 8f; // Net deðeri bir ölçekle çarparak y pozisyonuna dönüþtür
            GameObject circle = CreateCircle(new Vector2(xPosition, yPosition));
            circleList.Add(circle);
        }
    }
}
