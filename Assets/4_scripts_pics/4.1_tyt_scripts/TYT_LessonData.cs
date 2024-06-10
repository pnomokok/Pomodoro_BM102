using UnityEngine;
using System.Collections.Generic;

// CreateAssetMenu attribute, bu s�n�f�n Unity'nin asset creation men�s�nde g�r�nmesini sa�lar
[CreateAssetMenu(fileName = "NewTytLessonData", menuName = "TYT Lesson Data")]
public class TYT_LessonData : ScriptableObject
{
    // T�rk�e dersi i�in do�ru, yanl�� ve bo� cevaplar�n tutulaca�� de�i�kenler
    public int tytTurkceCorrectAnswers;
    public int tytTurkceWrongAnswers;
    public int tytTurkceEmptyAnswers;

    // Matematik dersi i�in do�ru, yanl�� ve bo� cevaplar�n tutulaca�� de�i�kenler
    public int tytMatematikCorrectAnswers;
    public int tytMatematikWrongAnswers;
    public int tytMatematikEmptyAnswers;

    // Fen Bilimleri dersi i�in do�ru, yanl�� ve bo� cevaplar�n tutulaca�� de�i�kenler
    public int tytFenCorrectAnswers;
    public int tytFenWrongAnswers;
    public int tytFenEmptyAnswers;

    // Sosyal Bilimler dersi i�in do�ru, yanl�� ve bo� cevaplar�n tutulaca�� de�i�kenler
    public int tytSosyalCorrectAnswers;
    public int tytSosyalWrongAnswers;
    public int tytSosyalEmptyAnswers;

    // Son be� net say�s�n� tutan liste
    public List<float> tyt_lastFiveNets = new List<float>();
}
