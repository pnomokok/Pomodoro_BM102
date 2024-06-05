using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class SurveyManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public Button nextButton;
    public Button previousButton;
    public TextMeshProUGUI warnText;

    public Image circleProgressBar;

    public Sprite nextArrowSprite;
    public Sprite checkmarkSprite;

    private int currentQuestionIndex = 0;
    private int totalScore = 0;
    private int[] selectedAnswers;
    private HashSet<int>[] multipleSelectedAnswers;

    public string[] questions = {
        "Son bir hafta içinde kendinizi ne sýklýkla stresli hissettiniz?",
        "Son zamanlarda baþ aðrýsý, mide problemleri, uyku sorunlarý, kas gerginliði,  kalp çarpýntýsý, yorgunluk gibi fiziksel belirtilerden kaç tanesini yaþadýnýz?",
        "Konsantrasyon sorunlarý yaþýyor musunuz?",
        "Son bir hafta içinde kendinizi ne sýklýkla huzursuz veya gergin hissettiniz?",
        "Stresli hissettiðinizde konuþabileceðiniz birine sahip misiniz?",
        "Kendinize ayýrdýðýnýz boþ zamanlarda rahatlamayý baþarabiliyor musunuz?",
        "Okul/sýnavlar, aile, sosyal iliþkiler, kiþisel saðlýk, gelecek endiþesi, maddi durum gibi faktörlerden kaç tanesi sizi strese sokar?"
    };

    public string[][] answers = {
        new string[] { "Hiç", "Nadiren", "Bazen", "Sýk sýk", "Her zaman" },
        new string[] { "Hiçbiri", "1-2", "3-4", "5-6" },
        new string[] { "Hiç", "Nadiren", "Bazen", "Sýk sýk", "Her zaman" },
        new string[] { "Hiç", "Nadiren", "Bazen", "Sýk sýk", "Her zaman" },
        new string[] { "Evet, her zaman", "Evet, bazen", "Hayýr, nadiren", "Hayýr, hiç" },
        new string[] { "Evet, her zaman", "Evet, bazen", "Hayýr, nadiren", "Hayýr, hiç" },
        new string[] {  "Hiçbiri", "1-2", "3-4", "5-6"  }
    };

    private bool[] isMultipleChoice = {
        false, // Son bir hafta içinde kendinizi ne sýklýkla stresli hissettiniz?
        false,  //Son zamanlarda baþ aðrýsý, mide problemleri, uyku sorunlarý, kas gerginliði,  kalp çarpýntýsý, yorgunluk gibi fiziksel belirtilerden kaç tanesini yaþadýnýz?
        false, // Konsantrasyon sorunlarý yaþýyor musunuz?
        false, // Son bir hafta içinde kendinizi ne sýklýkla huzursuz veya gergin hissettiniz?
        false, // Stresli hissettiðinizde konuþabileceðiniz birine sahip misiniz?
        false, // Kendinize ayýrdýðýnýz boþ zamanlarda rahatlamayý baþarabiliyor musunuz?
        false   // Okul/sýnavlar, aile, sosyal iliþkiler, kiþisel saðlýk, gelecek endiþesi, maddi durum gibi faktörlerden kaç tanesi sizi strese sokar?
    };

    void Start()
    {
        selectedAnswers = new int[questions.Length];
        multipleSelectedAnswers = new HashSet<int>[questions.Length];
        for (int i = 0; i < multipleSelectedAnswers.Length; i++)
        {
            multipleSelectedAnswers[i] = new HashSet<int>();
        }

        nextButton.onClick.AddListener(NextQuestion);
        previousButton.onClick.AddListener(PreviousQuestion);

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i; // Capture the index for the lambda
            optionButtons[i].onClick.AddListener(() => SelectAnswer(index + 1));
        }

        UpdateQuestion();
        circleProgressBar.fillAmount = currentQuestionIndex / 7f;
    }

    void UpdateQuestion()
    {
        warnText.text = " ";
        questionText.text = questions[currentQuestionIndex];
        circleProgressBar.fillAmount = currentQuestionIndex / 7f;
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < answers[currentQuestionIndex].Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[currentQuestionIndex][i];

                // Reset button visuals
                optionButtons[i].image.color = Color.white;
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }

        // Highlight the selected answer if exists
        if (isMultipleChoice[currentQuestionIndex])
        {
            foreach (var answerIndex in multipleSelectedAnswers[currentQuestionIndex])
            {
                optionButtons[answerIndex - 1].image.color = Color.green;
            }
        }
        else if (selectedAnswers[currentQuestionIndex] != 0)
        {
            optionButtons[selectedAnswers[currentQuestionIndex] - 1].image.color = Color.green;
        }

        previousButton.gameObject.SetActive(currentQuestionIndex > 0);

        // Update the next button's image
        if (currentQuestionIndex == questions.Length - 1)
        {
            nextButton.GetComponent<Image>().sprite = checkmarkSprite;
        }
        else
        {
            nextButton.GetComponent<Image>().sprite = nextArrowSprite;
        }
    }

    void SelectAnswer(int answerIndex)
    {
        Color selectedColor = new Color(120 / 255f, 114 / 255f, 222 / 255f); // #7872DE

        if (isMultipleChoice[currentQuestionIndex])
        {
            if (multipleSelectedAnswers[currentQuestionIndex].Contains(answerIndex))
            {
                multipleSelectedAnswers[currentQuestionIndex].Remove(answerIndex);
                totalScore -= answerIndex - 1;
            }
            else
            {
                multipleSelectedAnswers[currentQuestionIndex].Add(answerIndex);
                totalScore += answerIndex - 1;
            }
        }
        else
        {
            if (selectedAnswers[currentQuestionIndex] != 0)
            {
                totalScore -= selectedAnswers[currentQuestionIndex];
            }
            selectedAnswers[currentQuestionIndex] = answerIndex;
            totalScore += answerIndex - 1;
        }

        // Reset button visuals and highlight the selected ones
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].image.color = Color.white;
        }

        if (isMultipleChoice[currentQuestionIndex])
        {
            foreach (var idx in multipleSelectedAnswers[currentQuestionIndex])
            {
                optionButtons[idx - 1].image.color = selectedColor;
            }
        }
        else
        {
            optionButtons[answerIndex - 1].image.color = selectedColor;
        }
    }

    void NextQuestion()
    {
        if (!isMultipleChoice[currentQuestionIndex] && selectedAnswers[currentQuestionIndex] == 0)
        {
            warnText.text = "Lütfen bir cevap seçin.";
            Debug.LogWarning("Lütfen bir cevap seçin.");
            return;
        }

        if (isMultipleChoice[currentQuestionIndex] && multipleSelectedAnswers[currentQuestionIndex].Count == 0)
        {
            warnText.text = "Lütfen en az bir cevap seçin.";
            Debug.LogWarning("Lütfen en az bir cevap seçin.");
            return;
        }

        if (currentQuestionIndex < questions.Length - 1)
        {
            currentQuestionIndex++;
            UpdateQuestion();
            circleProgressBar.fillAmount = currentQuestionIndex / 7f;
        }
        else
        {
            SurveyData.totalScore = totalScore;
            SceneManager.LoadScene("RENKRZ5.3_ssonuc"); // Sonuç sahnesine geçiþ
        }
    }

    void PreviousQuestion()
    {
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            UpdateQuestion();
            circleProgressBar.fillAmount = currentQuestionIndex / 7f;
        }
    }
}




