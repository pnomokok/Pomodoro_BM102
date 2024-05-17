using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform GraphContainer;
    public SaveLessonData saveLessonData;

    void Start()
    {
        if (saveLessonData != null)
        {
            saveLessonData.confirmButton.onClick.AddListener(CreateGraph);
        }
        else
        {
            Debug.Log("SaveLessonData scripti atanmamýþ veya eksik!");
        }
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(GraphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
    }

    void CreateGraph()
    {
        if (saveLessonData != null)
        {
            CreateCircle(new Vector2(200, saveLessonData.toplamNet));
        }
        else
        {
            Debug.LogWarning("SaveLessonData scripti atanmamýþ veya eksik!");
        }
    }
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Graph : MonoBehaviour
//{
//    [SerializeField] private Sprite circleSprite;
//    private RectTransform GraphContainer;
//    public SaveLessonData saveLessonData;

//    private void Awake()
//    {
//        GraphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
//        CreateCircle(new Vector2(200, saveLessonData.toplamNet));
//    }

//    private void CreateCircle(Vector2 anchoredPosition)
//    {
//        GameObject gameObject = new GameObject("circle", typeof(Image));
//        gameObject.transform.SetParent(GraphContainer, false);
//        gameObject.GetComponent<Image>().sprite = circleSprite;
//        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
//        rectTransform.anchoredPosition = anchoredPosition;
//        rectTransform.sizeDelta = new Vector2(11, 11);
//        rectTransform.anchorMin = new Vector2(0, 0);
//        rectTransform.anchorMax = new Vector2(0, 0);
//    }
//}