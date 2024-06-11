using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TrueFalseGame : MonoBehaviour
{
    // UI elemanları
    public TMP_Text titleText;
    public Button playButton;
    public Button exitButton;
    public TMP_Text questionText;
    public Button trueButton;
    public Button falseButton;
    public TMP_Text scoreText;
    public TMP_Text gameOverText;
    public TMP_Text warnText;
    public TMP_Text questCountText;
    public Image correctImage; // Doğru cevap görseli
    public Image incorrectImage; // Yanlış cevap görseli
    public Image questFrame; // Soruların olduğu çerçeve
    public Button nextButton;

    // Oyun durumu değişkenleri
    private int score = 0;
    private string[] questions = { 
        "Enzimler biyokimyasal reaksiyonları başlatır.",
        "Osmanlı Devleti'nde \"Pençik Sistemi\"nin uygulanmasındaki temel amaç devlete sadık kişilerden oluşan bir ordu kurmaktır.",
        "Nötr halden anyona dönüşen bir taneciğin toplam tanecik sayısı artar.",
        "Karahanlıların Doğu-Batı Karahanlılar olarak ikiye ayrılması Haçlı Seferlerinin bir sonucudur.",
        "Etki-tepki kuvvetleri sadece temas gerektiren kuvvetler için geçerlidir.",
        "Türkiye, Ekvator üzerinde bir konuma getirilseydi yer şekilleri değişmezdi.",
        "Etçillerin bağırsaklarında selüloz sindiren bakteriler bulunur",
        "\"Sağlık raporu başvurularını kurul değerlendirecek\" cümlesinde topluluk adı kullanılmıştır",
        "Sentriollerin eşlenmesi bitki hücresinin hücre döngüsünde görülen bir olaydır.",
        "Sıcaklık azaldıkça gaz moleküllerinin kinetik enerjisi azalır.",
        "İslam dinine göre insanlara verilmiş olan kaza-kader sınırları çerçevesinde hareket imkanı tanıyan özgür irade \"Külli İrade\"dir.",
        "Moleküler kristallerin aynı koşullarda erime noktası iyonik kristallerinkinden düşüktür.",
        "Sürtünmeli bir yüzeyde atılan cismin yavaşlaması dengelenmiş kuvvet etkisinde olduğunu gösterir.",
        "Filogenetik sınıflandırmada aynı takımda olduğu bilinen canlıların çubeleri farklı olabilir.",
        "\"Araba hızını alamamış, ilerideki kanala uçmuş.\" bağımlı sıralı bir cümledir."
    };
    private bool[] answers = { 
        false, true, true, false, false, true, false, true, false, true, false, true, false, false, true 
    };
    private int currentQuestionIndex = 0;

    // Soruların rastgele sırayla sorulması için indeksler
    private List<int> questionIndices = new List<int>();
    private bool hasAnswered = false; // Kullanıcının cevap verip vermediğini izlemek için

    void Start()
    {
        // Başlangıç ekranını göster
        ShowStartScreen();
        // "Sonraki" butonuna tıklama dinleyicisi ekle
        nextButton.onClick.AddListener(LoadNextQuestionOnClick);
    }

    public void ShowStartScreen()
    {
        // Başlangıç ekranındaki elemanların görünürlüğünü ayarla
        titleText.text = "True          False";
        playButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        questionText.gameObject.SetActive(false);
        questCountText.gameObject.SetActive(false);
        trueButton.gameObject.SetActive(false);
        falseButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        correctImage.gameObject.SetActive(false);
        incorrectImage.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        questFrame.gameObject.SetActive(false);
        warnText.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        // Oyunu başlat ve ilgili elemanları görünür yap
        playButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        questionText.gameObject.SetActive(true);
        questCountText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        trueButton.gameObject.SetActive(true);
        falseButton.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        questFrame.gameObject.SetActive(true);

        // Oyun başlangıç değerlerini ayarla
        score = 0;
        currentQuestionIndex = 0;

        // Soru sayısını ve skoru güncelle
        questCountText.text = (currentQuestionIndex + 1) + "/15";
        scoreText.text = "Skor: " + score;

        // Soruları karıştır
        ShuffleQuestions();

        // İlk soruyu yükle
        LoadNextQuestion();
    }

    void ShuffleQuestions()
    {
        // Soruları karıştırma
        questionIndices.Clear();
        for (int i = 0; i < questions.Length; i++)
        {
            questionIndices.Add(i);
        }
        for (int i = 0; i < questionIndices.Count; i++)
        {
            int temp = questionIndices[i];
            int randomIndex = Random.Range(i, questionIndices.Count);
            questionIndices[i] = questionIndices[randomIndex];
            questionIndices[randomIndex] = temp;
        }
    }

    void LoadNextQuestion()
    {
        // Yeni soru yükle ve ilgili elemanları güncelle
        correctImage.gameObject.SetActive(false);
        incorrectImage.gameObject.SetActive(false);
        trueButton.interactable = true;
        falseButton.interactable = true;
        nextButton.interactable = true;
        hasAnswered = false; // Kullanıcı henüz cevap vermedi

        if (currentQuestionIndex < questionIndices.Count)
        {
            questCountText.text = (currentQuestionIndex + 1) + "/15";
            questionText.text = questions[questionIndices[currentQuestionIndex]];
            warnText.gameObject.SetActive(false);
        }
        else
        {
            // Oyun bittiğinde yapılacaklar
            gameOverText.text = "Oyun Bitti!\nSkor: " + score;

            trueButton.gameObject.SetActive(false);
            falseButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            questFrame.gameObject.SetActive(false);
            questionText.gameObject.SetActive(false);
            questCountText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(true);
            playButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
        }
    }

    public void Answer(bool isTrue)
    {
        if (hasAnswered)
            return;

        hasAnswered = true; // Kullanıcı cevap verdi

        if (answers[questionIndices[currentQuestionIndex]] == isTrue)
        {
            // Doğru cevap verildiğinde skor güncelle ve geri bildirim göster
            score++;
            scoreText.text = "Skor: " + score;
            ShowCorrectFeedback();
        }
        else
        {
            // Yanlış cevap verildiğinde geri bildirim göster
            ShowIncorrectFeedback();
        }

        // Cevap butonlarını devre dışı bırak
        trueButton.interactable = false;
        falseButton.interactable = false;
        nextButton.interactable = true;
        warnText.gameObject.SetActive(false);
    }

    void ShowCorrectFeedback()
    {
        // Doğru cevap görselini göster
        correctImage.gameObject.SetActive(true);
    }

    void ShowIncorrectFeedback()
    {
        // Yanlış cevap görselini göster
        incorrectImage.gameObject.SetActive(true);
    }

    void LoadNextQuestionOnClick()
    {
        if (!hasAnswered)
        {
            // Kullanıcı cevap vermediyse uyarı göster
            warnText.text = "Lütfen bir cevap seçin.";
            warnText.gameObject.SetActive(true);
            nextButton.interactable = false;
            return;
        }
        // Sonraki soruya geç
        currentQuestionIndex++;
        LoadNextQuestion();
    }
}
