using UnityEngine;
using System.Collections.Generic;

// CreateAssetMenu attribute, bu sýnýfýn Unity'nin asset creation menüsünde görünmesini saðlar
[CreateAssetMenu(fileName = "NewTytLessonData", menuName = "TYT Lesson Data")]
public class TYT_LessonData : ScriptableObject
{
    // Türkçe dersi için doðru, yanlýþ ve boþ cevaplarýn tutulacaðý deðiþkenler
    public int tytTurkceCorrectAnswers;
    public int tytTurkceWrongAnswers;
    public int tytTurkceEmptyAnswers;

    // Matematik dersi için doðru, yanlýþ ve boþ cevaplarýn tutulacaðý deðiþkenler
    public int tytMatematikCorrectAnswers;
    public int tytMatematikWrongAnswers;
    public int tytMatematikEmptyAnswers;

    // Fen Bilimleri dersi için doðru, yanlýþ ve boþ cevaplarýn tutulacaðý deðiþkenler
    public int tytFenCorrectAnswers;
    public int tytFenWrongAnswers;
    public int tytFenEmptyAnswers;

    // Sosyal Bilimler dersi için doðru, yanlýþ ve boþ cevaplarýn tutulacaðý deðiþkenler
    public int tytSosyalCorrectAnswers;
    public int tytSosyalWrongAnswers;
    public int tytSosyalEmptyAnswers;

    // Son beþ net sayýsýný tutan liste
    public List<float> tyt_lastFiveNets = new List<float>();
}
