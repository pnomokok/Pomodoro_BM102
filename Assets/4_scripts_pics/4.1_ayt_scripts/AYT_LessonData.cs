using UnityEngine;
using System.Collections.Generic;

// CreateAssetMenu attribute, bu sýnýfýn Unity'nin asset creation menüsünde görünmesini saðlar
[CreateAssetMenu(fileName = "NewAytLessonData", menuName = "AYT Lesson Data")]
public class AYT_LessonData : ScriptableObject
{
    // Sosyal Bilimler-1 dersi için doðru, yanlýþ ve boþ cevaplarýn tutulacaðý deðiþkenler
    public int aytSos1CorrectAnswers;
    public int aytSos1WrongAnswers;
    public int aytSos1EmptyAnswers;

    // Sosyal Bilimler-2 dersi için doðru, yanlýþ ve boþ cevaplarýn tutulacaðý deðiþkenler
    public int aytSos2CorrectAnswers;
    public int aytSos2WrongAnswers;
    public int aytSos2EmptyAnswers;

    // Matematik dersi için doðru, yanlýþ ve boþ cevaplarýn tutulacaðý deðiþkenler
    public int aytMatematikCorrectAnswers;
    public int aytMatematikWrongAnswers;
    public int aytMatematikEmptyAnswers;

    // Fen Bilimleri dersi için doðru, yanlýþ ve boþ cevaplarýn tutulacaðý deðiþkenler
    public int aytFenCorrectAnswers;
    public int aytFenWrongAnswers;
    public int aytFenEmptyAnswers;

    // Son beþ net sayýsýný tutan liste
    public List<float> ayt_lastFiveNets = new List<float>();
}
