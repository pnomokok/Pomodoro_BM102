using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        LoadInputFields();
    }

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

        float toplamNet = (lessonData.TurkceCorrectAnswers + lessonData.MatematikCorrectAnswers + lessonData.FenCorrectAnswers + lessonData.SosyalCorrectAnswers) -
                          (lessonData.TurkceWrongAnswers + lessonData.MatematikWrongAnswers + lessonData.FenWrongAnswers + lessonData.SosyalWrongAnswers) / 4.0f;

        DataManager.Instance.AddNet(toplamNet);
    }

    public void SaveInputFields()
    {
        PlayerPrefs.SetString("TurkceCorrect", TurkceCorrectInputField.text);
        PlayerPrefs.SetString("TurkceWrong", TurkceWrongInputField.text);
        PlayerPrefs.SetString("TurkceEmpty", TurkceEmptyInputField.text);

        PlayerPrefs.SetString("MatematikCorrect", MatematikCorrectInputField.text);
        PlayerPrefs.SetString("MatematikWrong", MatematikWrongInputField.text);
        PlayerPrefs.SetString("MatematikEmpty", MatematikEmptyInputField.text);

        PlayerPrefs.SetString("FenCorrect", FenCorrectInputField.text);
        PlayerPrefs.SetString("FenWrong", FenWrongInputField.text);
        PlayerPrefs.SetString("FenEmpty", FenEmptyInputField.text);

        PlayerPrefs.SetString("SosyalCorrect", SosyalCorrectInputField.text);
        PlayerPrefs.SetString("SosyalWrong", SosyalWrongInputField.text);
        PlayerPrefs.SetString("SosyalEmpty", SosyalEmptyInputField.text);

        PlayerPrefs.Save();
    }

    public void LoadInputFields()
    {
        TurkceCorrectInputField.text = PlayerPrefs.GetString("TurkceCorrect", "");
        TurkceWrongInputField.text = PlayerPrefs.GetString("TurkceWrong", "");
        TurkceEmptyInputField.text = PlayerPrefs.GetString("TurkceEmpty", "");

        MatematikCorrectInputField.text = PlayerPrefs.GetString("MatematikCorrect", "");
        MatematikWrongInputField.text = PlayerPrefs.GetString("MatematikWrong", "");
        MatematikEmptyInputField.text = PlayerPrefs.GetString("MatematikEmpty", "");

        FenCorrectInputField.text = PlayerPrefs.GetString("FenCorrect", "");
        FenWrongInputField.text = PlayerPrefs.GetString("FenWrong", "");
        FenEmptyInputField.text = PlayerPrefs.GetString("FenEmpty", "");

        SosyalCorrectInputField.text = PlayerPrefs.GetString("SosyalCorrect", "");
        SosyalWrongInputField.text = PlayerPrefs.GetString("SosyalWrong", "");
        SosyalEmptyInputField.text = PlayerPrefs.GetString("SosyalEmpty", "");
    }
}
