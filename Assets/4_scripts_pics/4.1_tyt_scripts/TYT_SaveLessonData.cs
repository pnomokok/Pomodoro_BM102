using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TYT_SaveLessonData : MonoBehaviour
{
    public TMP_InputField tytTurkceCorrectInputField;
    public TMP_InputField tytTurkceWrongInputField;
    public TMP_InputField tytTurkceEmptyInputField;

    public TMP_InputField tytSosyalCorrectInputField;
    public TMP_InputField tytSosyalWrongInputField;
    public TMP_InputField tytSosyalEmptyInputField;

    public TMP_InputField tytMatematikCorrectInputField;
    public TMP_InputField tytMatematikWrongInputField;
    public TMP_InputField tytMatematikEmptyInputField;

    public TMP_InputField tytFenCorrectInputField;
    public TMP_InputField tytFenWrongInputField;
    public TMP_InputField tytFenEmptyInputField;

    public TextMeshProUGUI warningText; // Uyarý metni referansý

    public TYT_LessonData tytLessonData;

    public void TYT_SaveData()
    {
        int turkceCorrect = ParseInputField(tytTurkceCorrectInputField);
        int turkceWrong = ParseInputField(tytTurkceWrongInputField);
        int turkceEmpty = ParseInputField(tytTurkceEmptyInputField);

        int sosyalCorrect = ParseInputField(tytSosyalCorrectInputField);
        int sosyalWrong = ParseInputField(tytSosyalWrongInputField);
        int sosyalEmpty = ParseInputField(tytSosyalEmptyInputField);

        int matematikCorrect = ParseInputField(tytMatematikCorrectInputField);
        int matematikWrong = ParseInputField(tytMatematikWrongInputField);
        int matematikEmpty = ParseInputField(tytMatematikEmptyInputField);

        int fenCorrect = ParseInputField(tytFenCorrectInputField);
        int fenWrong = ParseInputField(tytFenWrongInputField);
        int fenEmpty = ParseInputField(tytFenEmptyInputField);

        if (!IsValidInput(turkceCorrect, turkceWrong, turkceEmpty, 40) ||
            !IsValidInput(sosyalCorrect, sosyalWrong, sosyalEmpty, 20) ||
            !IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) ||
            !IsValidInput(fenCorrect, fenWrong, fenEmpty, 20))
        {
            warningText.text = "Lütfen her ders için geçerli deðerler girin. Toplam soru sayýsý ve negatif deðerler kontrol edilmeli.";
            return;
        }

        tytLessonData.tytTurkceCorrectAnswers = turkceCorrect;
        tytLessonData.tytTurkceWrongAnswers = turkceWrong;
        tytLessonData.tytTurkceEmptyAnswers = turkceEmpty;

        tytLessonData.tytSosyalCorrectAnswers = sosyalCorrect;
        tytLessonData.tytSosyalWrongAnswers = sosyalWrong;
        tytLessonData.tytSosyalEmptyAnswers = sosyalEmpty;

        tytLessonData.tytMatematikCorrectAnswers = matematikCorrect;
        tytLessonData.tytMatematikWrongAnswers = matematikWrong;
        tytLessonData.tytMatematikEmptyAnswers = matematikEmpty;

        tytLessonData.tytFenCorrectAnswers = fenCorrect;
        tytLessonData.tytFenWrongAnswers = fenWrong;
        tytLessonData.tytFenEmptyAnswers = fenEmpty;

        float tyt_ToplamNet = (tytLessonData.tytTurkceCorrectAnswers + tytLessonData.tytSosyalCorrectAnswers + tytLessonData.tytMatematikCorrectAnswers + tytLessonData.tytFenCorrectAnswers)
            - (tytLessonData.tytTurkceWrongAnswers + tytLessonData.tytSosyalWrongAnswers + tytLessonData.tytMatematikWrongAnswers + tytLessonData.tytFenWrongAnswers) / 4.0f;

        TYT_DataManager.tytInstance.AddNet(tyt_ToplamNet);

        Debug.Log("Net added: " + tyt_ToplamNet); // Debug log

        warningText.text = ""; // Uyarý mesajýný temizle
    }

    private int ParseInputField(TMP_InputField inputField)
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            return 0;
        }
        else
        {
            return int.Parse(inputField.text);
        }
    }

    private bool IsValidInput(int correct, int wrong, int empty, int totalQuestions)
    {
        return (correct + wrong + empty == totalQuestions) && (correct >= 0 && wrong >= 0 && empty >= 0);
    }
}
