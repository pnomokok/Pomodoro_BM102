using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveLessonData : MonoBehaviour
{
    public TMP_InputField TurkceCorrectInputField;
    public TMP_InputField TurkceWrongInputField;
    public TMP_InputField TurkceEmptyInputField;

    public TMP_InputField MatematikCorrectInputField;
    public TMP_InputField MatematikWrongInputField;
    public TMP_InputField MatematikEmptyInputField;

    public TMP_InputField FenCorrectInputField;
    public TMP_InputField FenWrongInputField;
    public TMP_InputField FenEmptyInputField;

    public TMP_InputField SosyalCorrectInputField;
    public TMP_InputField SosyalWrongInputField;
    public TMP_InputField SosyalEmptyInputField;

    public LessonData lessonData;
    public Graph graph;

    public void SaveData()
    {
        lessonData.TurkceCorrectAnswers = int.Parse(TurkceCorrectInputField.text);
        lessonData.TurkceWrongAnswers = int.Parse(TurkceWrongInputField.text);
        lessonData.TurkceEmptyAnswers = int.Parse(TurkceEmptyInputField.text);

        lessonData.MatematikCorrectAnswers = int.Parse(MatematikCorrectInputField.text);
        lessonData.MatematikWrongAnswers = int.Parse(MatematikWrongInputField.text);
        lessonData.MatematikEmptyAnswers = int.Parse(MatematikEmptyInputField.text);

        lessonData.FenCorrectAnswers = int.Parse(FenCorrectInputField.text);
        lessonData.FenWrongAnswers = int.Parse(FenWrongInputField.text);
        lessonData.FenEmptyAnswers = int.Parse(FenEmptyInputField.text);

        lessonData.SosyalCorrectAnswers = int.Parse(SosyalCorrectInputField.text);
        lessonData.SosyalWrongAnswers = int.Parse(SosyalWrongInputField.text);
        lessonData.SosyalEmptyAnswers = int.Parse(SosyalEmptyInputField.text);

        float toplamNet = (lessonData.TurkceCorrectAnswers + lessonData.MatematikCorrectAnswers + lessonData.FenCorrectAnswers + lessonData.SosyalCorrectAnswers) - (lessonData.TurkceWrongAnswers + lessonData.MatematikWrongAnswers + lessonData.FenWrongAnswers + lessonData.SosyalWrongAnswers) / 4.0f;

        // Deneme netlerini güncelle
        if (lessonData.lastFiveNets.Count >= 5)
        {
            lessonData.lastFiveNets.RemoveAt(0); // En eski deneme netini kaldýr
        }
        lessonData.lastFiveNets.Add(toplamNet);

        // Grafiði güncelle
        graph.UpdateGraph(lessonData.lastFiveNets);
    }
}


//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class SaveLessonData : MonoBehaviour
//{
//    public TMP_InputField TurkceCorrectInputField;
//    public TMP_InputField TurkceWrongInputField;
//    public TMP_InputField TurkceEmptyInputField;

//    public TMP_InputField MatematikCorrectInputField;
//    public TMP_InputField MatematikWrongInputField;
//    public TMP_InputField MatematikEmptyInputField;

//    public TMP_InputField FenCorrectInputField;
//    public TMP_InputField FenWrongInputField;
//    public TMP_InputField FenEmptyInputField;

//    public TMP_InputField SosyalCorrectInputField;
//    public TMP_InputField SosyalWrongInputField;
//    public TMP_InputField SosyalEmptyInputField;

//    public float toplamNet;

//    public LessonData lessonData;

//    public Button confirmButton;

//    void Start()
//    {
//        confirmButton.onClick.AddListener(ConfirmButtonClicked);
//    }

//    void ConfirmButtonClicked()
//    {
//        SaveData();
//        CalculateTotalNet();
//    }

//    public void SaveData()
//    {
//        lessonData.TurkceCorrectAnswers = int.Parse(TurkceCorrectInputField.text);
//        lessonData.TurkceWrongAnswers = int.Parse(TurkceWrongInputField.text);
//        lessonData.TurkceEmptyAnswers = int.Parse(TurkceEmptyInputField.text);

//        lessonData.MatematikCorrectAnswers = int.Parse(MatematikCorrectInputField.text);
//        lessonData.MatematikWrongAnswers = int.Parse(MatematikWrongInputField.text);
//        lessonData.MatematikEmptyAnswers = int.Parse(MatematikEmptyInputField.text);

//        lessonData.FenCorrectAnswers = int.Parse(FenCorrectInputField.text);
//        lessonData.FenWrongAnswers = int.Parse(FenWrongInputField.text);
//        lessonData.FenEmptyAnswers = int.Parse(FenEmptyInputField.text);

//        lessonData.SosyalCorrectAnswers = int.Parse(SosyalCorrectInputField.text);
//        lessonData.SosyalWrongAnswers = int.Parse(SosyalWrongInputField.text);
//        lessonData.SosyalEmptyAnswers = int.Parse(SosyalEmptyInputField.text);
//    }
//    public void CalculateTotalNet()
//    {
//        toplamNet = (lessonData.TurkceCorrectAnswers + lessonData.MatematikCorrectAnswers + lessonData.FenCorrectAnswers + lessonData.SosyalCorrectAnswers) - (lessonData.TurkceWrongAnswers + lessonData.MatematikWrongAnswers + lessonData.FenWrongAnswers + lessonData.SosyalWrongAnswers) / 4.0f;
//    }
//}
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class SaveLessonData : MonoBehaviour
//{
//    public TMP_InputField TurkceCorrectInputField;
//    public TMP_InputField TurkceWrongInputField;
//    public TMP_InputField TurkceEmptyInputField;

//    public TMP_InputField MatematikCorrectInputField;
//    public TMP_InputField MatematikWrongInputField;
//    public TMP_InputField MatematikEmptyInputField;

//    public TMP_InputField FenCorrectInputField;
//    public TMP_InputField FenWrongInputField;
//    public TMP_InputField FenEmptyInputField;

//    public TMP_InputField SosyalCorrectInputField;
//    public TMP_InputField SosyalWrongInputField;
//    public TMP_InputField SosyalEmptyInputField;

//    public float toplamNet;

//    public LessonData lessonData;

//    public void SaveData()
//    {
//        lessonData.TurkceCorrectAnswers = int.Parse(TurkceCorrectInputField.text);
//        lessonData.TurkceWrongAnswers = int.Parse(TurkceWrongInputField.text);
//        lessonData.TurkceEmptyAnswers = int.Parse(TurkceEmptyInputField.text);

//        lessonData.MatematikCorrectAnswers = int.Parse(MatematikCorrectInputField.text);
//        lessonData.MatematikWrongAnswers = int.Parse(MatematikWrongInputField.text);
//        lessonData.MatematikEmptyAnswers = int.Parse(MatematikEmptyInputField.text);

//        lessonData.FenCorrectAnswers = int.Parse(FenCorrectInputField.text);
//        lessonData.FenWrongAnswers = int.Parse(FenWrongInputField.text);
//        lessonData.FenEmptyAnswers = int.Parse(FenEmptyInputField.text);

//        lessonData.SosyalCorrectAnswers = int.Parse(SosyalCorrectInputField.text);
//        lessonData.SosyalWrongAnswers = int.Parse(SosyalWrongInputField.text);
//        lessonData.SosyalEmptyAnswers = int.Parse(SosyalEmptyInputField.text);

//        toplamNet = (lessonData.TurkceCorrectAnswers + lessonData.MatematikCorrectAnswers + lessonData.FenCorrectAnswers + lessonData.SosyalCorrectAnswers) - (lessonData.TurkceWrongAnswers + lessonData.MatematikWrongAnswers + lessonData.FenWrongAnswers + lessonData.SosyalWrongAnswers) / 4.0f;
//    }
//}
