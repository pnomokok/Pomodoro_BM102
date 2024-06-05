//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Events;
//public class Result : MonoBehaviour
//{

//        public Questions questions;
//        public GameObject correctSprite;
//        public GameObject incorrectSprite;

//        public Scores scores;



//        public Button trueButton;
//        public Button falseButton;

//        public UnityEvent onNextQuestion;

//    void Start()
//    {
//        correctSprite.SetActive(false);
//        incorrectSprite.SetActive(false);
//    }
//    public void ShowResults(bool answer)
//    {
//        correctSprite.SetActive(questions.questionsList[questions.currentQuestion].isTrue==answer);
//        incorrectSprite.SetActive(questions.questionsList[questions.currentQuestion].isTrue!=answer);

//        if(questions.questionsList[questions.currentQuestion].isTrue==answer)
//           scores.AddScore();
//        else
//            scores.DeductScore();

//        trueButton.interactable=false;
//        falseButton.interactable=false;

//        StartCoroutine(ShowResult());
//    }
//    private IEnumerator ShowResult()
//    {
//        yield return new WaitForSeconds(1.0f);
//        correctSprite.SetActive(false);
//        incorrectSprite.SetActive(false);

//        trueButton.interactable=true;
//        falseButton.interactable=true;

//        onNextQuestion.Invoke();
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Result : MonoBehaviour
{
    public Questions questions;
    public GameObject correctSprite;
    public GameObject incorrectSprite;
    public Scores scores;
    public Button trueButton;
    public Button falseButton;
    public Text gameOverScoreText; // Ekranda score'u göstermek için Text UI öðesi
    public UnityEvent onNextQuestion;

    void Start()
    {
        correctSprite.SetActive(false);
        incorrectSprite.SetActive(false);
        gameOverScoreText.gameObject.SetActive(false); // Score Text baþlangýçta gizli
    }

    public void ShowResults(bool answer)
    {
        correctSprite.SetActive(questions.questionsList[questions.currentQuestion].isTrue == answer);
        incorrectSprite.SetActive(questions.questionsList[questions.currentQuestion].isTrue != answer);

        if (questions.questionsList[questions.currentQuestion].isTrue == answer)
            scores.AddScore();
        else
        {
            scores.DeductScore();
            GameOver(); // Kullanýcý yanýldýðýnda oyun biter
        }

        trueButton.interactable = false;
        falseButton.interactable = false;

        StartCoroutine(ShowResult());
    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(1.0f);
        correctSprite.SetActive(false);
        incorrectSprite.SetActive(false);

        trueButton.interactable = true;
        falseButton.interactable = true;

        onNextQuestion.Invoke();
    }

    private void GameOver()
    {
        gameOverScoreText.gameObject.SetActive(true); // Score Text'i göster
        gameOverScoreText.text = "Game Over! Your Score: " + scores.GetCurrentScore();
        trueButton.gameObject.SetActive(false);
        falseButton.gameObject.SetActive(false);
    }
}
