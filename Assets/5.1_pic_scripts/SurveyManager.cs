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
        "Son bir hafta i�inde kendinizi ne s�kl�kla stresli hissettiniz?",
        "Son zamanlarda ba� a�r�s�, mide problemleri, uyku sorunlar�, kas gerginli�i,  kalp �arp�nt�s�, yorgunluk gibi fiziksel belirtilerden ka� tanesini ya�ad�n�z?",
        "Konsantrasyon sorunlar� ya��yor musunuz?",
        "Son bir hafta i�inde kendinizi ne s�kl�kla huzursuz veya gergin hissettiniz?",
        "Stresli hissetti�inizde konu�abilece�iniz birine sahip misiniz?",
        "Kendinize ay�rd���n�z bo� zamanlarda rahatlamay� ba�arabiliyor musunuz?",
        "Okul/s�navlar, aile, sosyal ili�kiler, ki�isel sa�l�k, gelecek endi�esi, maddi durum gibi fakt�rlerden ka� tanesi sizi strese sokar?"
    };

    public string[][] answers = {
        new string[] { "Hi�", "Nadiren", "Bazen", "S�k s�k", "Her zaman" },
        new string[] { "Hi�biri", "1-2", "3-4", "5-6" },
        new string[] { "Hi�", "Nadiren", "Bazen", "S�k s�k", "Her zaman" },
        new string[] { "Hi�", "Nadiren", "Bazen", "S�k s�k", "Her zaman" },
        new string[] { "Evet, her zaman", "Evet, bazen", "Hay�r, nadiren", "Hay�r, hi�" },
        new string[] { "Evet, her zaman", "Evet, bazen", "Hay�r, nadiren", "Hay�r, hi�" },
        new string[] {  "Hi�biri", "1-2", "3-4", "5-6"  }
    };

    private bool[] isMultipleChoice = {
        false, // Son bir hafta i�inde kendinizi ne s�kl�kla stresli hissettiniz?
        false,  //Son zamanlarda ba� a�r�s�, mide problemleri, uyku sorunlar�, kas gerginli�i,  kalp �arp�nt�s�, yorgunluk gibi fiziksel belirtilerden ka� tanesini ya�ad�n�z?
        false, // Konsantrasyon sorunlar� ya��yor musunuz?
        false, // Son bir hafta i�inde kendinizi ne s�kl�kla huzursuz veya gergin hissettiniz?
        false, // Stresli hissetti�inizde konu�abilece�iniz birine sahip misiniz?
        false, // Kendinize ay�rd���n�z bo� zamanlarda rahatlamay� ba�arabiliyor musunuz?
        false   // Okul/s�navlar, aile, sosyal ili�kiler, ki�isel sa�l�k, gelecek endi�esi, maddi durum gibi fakt�rlerden ka� tanesi sizi strese sokar?
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
            warnText.text = "L�tfen bir cevap se�in.";
            Debug.LogWarning("L�tfen bir cevap se�in.");
            return;
        }

        if (isMultipleChoice[currentQuestionIndex] && multipleSelectedAnswers[currentQuestionIndex].Count == 0)
        {
            warnText.text = "L�tfen en az bir cevap se�in.";
            Debug.LogWarning("L�tfen en az bir cevap se�in.");
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
            SceneManager.LoadScene("RENKRZ5.3_ssonuc"); // Sonu� sahnesine ge�i�
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




