

//using UnityEngine;
//using UnityEngine.UI;

//public class GameManager : MonoBehaviour
//{
//    public Player player;
//    public Text scoreText;
//    public GameObject playButton;
//    public GameObject gameOver;
//    public GameObject exitButton; // Exit butonunu ekleyin

//    private int score;

//    private void Awake()
//    {
//        Application.targetFrameRate = 60;

//        // Game Over ve Exit butonlarýný devre dýþý býrak
//        gameOver.SetActive(false);
//        exitButton.SetActive(true); 

//        Pause();
//    }

//    public void Play()
//    {
//        score = 0;
//        scoreText.text = score.ToString();

//        playButton.SetActive(false);
//        gameOver.SetActive(false);
//        exitButton.SetActive(false); // Exit butonunu devre dýþý býrak

//        Time.timeScale = 1f;
//        player.enabled = true;

//        Pipes[] pipes = FindObjectsOfType<Pipes>();

//        for (int i = 0; i < pipes.Length; i++)
//        {
//            Destroy(pipes[i].gameObject);
//        }
//    }

//    public void GameOver()
//    {
//        playButton.SetActive(true);
//        gameOver.SetActive(true);
//        exitButton.SetActive(true); // Exit butonunu göster

//        Pause();
//    }

//    public void Pause()
//    {
//        Time.timeScale = 0f;
//        player.enabled = false;
//    }

//    public void IncreaseScore()
//    {
//        score++;
//        scoreText.text = score.ToString();
//    }
//}


using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public Text bestScoreText; // En iyi skoru gösterecek Text
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject exitButton;

    private int score;
    private int bestScore;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        // Game Over ve Exit butonlarýný devre dýþý býrak
        gameOver.SetActive(false);
        exitButton.SetActive(true);

        // En iyi skoru yükle
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best: " + bestScore;

        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        exitButton.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);
        exitButton.SetActive(true);

        // En iyi skoru güncelle
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }

        bestScoreText.text = "Best: " + bestScore; // En iyi skoru güncelle
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
