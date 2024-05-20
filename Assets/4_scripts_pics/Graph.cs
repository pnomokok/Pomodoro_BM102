using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Material lineMaterial;
    private RectTransform graphContainer;
    private List<GameObject> circleList = new List<GameObject>();
    private CustomLineDrawer lineDrawer;

    private void Start()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        lineDrawer = gameObject.AddComponent<CustomLineDrawer>();
        lineDrawer.SetLineMaterial(lineMaterial);

        if (DataManager.Instance != null && DataManager.Instance.lastFiveNets != null)
        {
            UpdateGraph(DataManager.Instance.lastFiveNets);
        }
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

        float xSpacing = 150f;
        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < values.Count; i++)
        {
            float xPosition = xSpacing * (i + 1);
            float yPosition = values[i] * 7f;
            GameObject circle = CreateCircle(new Vector2(xPosition, yPosition));
            circleList.Add(circle);

            Vector2 anchoredPos = new Vector2(xPosition, yPosition);
            positions.Add(graphContainer.TransformPoint(anchoredPos));
        }

        lineDrawer.DrawLines(positions);
    }
}
