using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AYT_SaveLessonData : MonoBehaviour
{
    public TMP_InputField aytSos1CorrectInputField;
    public TMP_InputField aytSos1WrongInputField;
    public TMP_InputField aytSos1EmptyInputField;

    public TMP_InputField aytSos2CorrectInputField;
    public TMP_InputField aytSos2WrongInputField;
    public TMP_InputField aytSos2EmptyInputField;

    public TMP_InputField aytMatematikCorrectInputField;
    public TMP_InputField aytMatematikWrongInputField;
    public TMP_InputField aytMatematikEmptyInputField;

    public TMP_InputField aytFenCorrectInputField;
    public TMP_InputField aytFenWrongInputField;
    public TMP_InputField aytFenEmptyInputField;

    public TextMeshProUGUI warningText; // Uyarý metni referansý

    public AYT_LessonData aytLessonData;

    public void AYT_SaveData()
    {
        int sos1Correct = ParseInputField(aytSos1CorrectInputField);
        int sos1Wrong = ParseInputField(aytSos1WrongInputField);
        int sos1Empty = ParseInputField(aytSos1EmptyInputField);

        int sos2Correct = ParseInputField(aytSos2CorrectInputField);
        int sos2Wrong = ParseInputField(aytSos2WrongInputField);
        int sos2Empty = ParseInputField(aytSos2EmptyInputField);

        int matematikCorrect = ParseInputField(aytMatematikCorrectInputField);
        int matematikWrong = ParseInputField(aytMatematikWrongInputField);
        int matematikEmpty = ParseInputField(aytMatematikEmptyInputField);

        int fenCorrect = ParseInputField(aytFenCorrectInputField);
        int fenWrong = ParseInputField(aytFenWrongInputField);
        int fenEmpty = ParseInputField(aytFenEmptyInputField);

        if (!IsValidInput(sos1Correct, sos1Wrong, sos1Empty, 40) ||
            !IsValidInput(sos2Correct, sos2Wrong, sos2Empty, 40) ||
            !IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) ||
            !IsValidInput(fenCorrect, fenWrong, fenEmpty, 40))
        {
            warningText.text = "Lütfen her ders için geçerli deðerler girin. Toplam soru sayýsý ve negatif deðerler kontrol edilmeli.";
            return;
        }

        aytLessonData.aytSos1CorrectAnswers = sos1Correct;
        aytLessonData.aytSos1WrongAnswers = sos1Wrong;
        aytLessonData.aytSos1EmptyAnswers = sos1Empty;

        aytLessonData.aytSos2CorrectAnswers = sos2Correct;
        aytLessonData.aytSos2WrongAnswers = sos2Wrong;
        aytLessonData.aytSos2EmptyAnswers = sos2Empty;

        aytLessonData.aytMatematikCorrectAnswers = matematikCorrect;
        aytLessonData.aytMatematikWrongAnswers = matematikWrong;
        aytLessonData.aytMatematikEmptyAnswers = matematikEmpty;

        aytLessonData.aytFenCorrectAnswers = fenCorrect;
        aytLessonData.aytFenWrongAnswers = fenWrong;
        aytLessonData.aytFenEmptyAnswers = fenEmpty;

        float ayt_ToplamNet = (aytLessonData.aytSos1CorrectAnswers + aytLessonData.aytSos2CorrectAnswers + aytLessonData.aytMatematikCorrectAnswers + aytLessonData.aytFenCorrectAnswers)
            - (aytLessonData.aytSos1WrongAnswers + aytLessonData.aytSos2WrongAnswers + aytLessonData.aytMatematikWrongAnswers + aytLessonData.aytFenWrongAnswers) / 4.0f;

        AYT_DataManager.aytInstance.AddNet(ayt_ToplamNet);
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
