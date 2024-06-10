using UnityEngine;
using System.Collections.Generic;

// CreateAssetMenu attribute, bu s�n�f�n Unity'nin asset creation men�s�nde g�r�nmesini sa�lar
[CreateAssetMenu(fileName = "NewAytLessonData", menuName = "AYT Lesson Data")]
public class AYT_LessonData : ScriptableObject
{
    // Sosyal Bilimler-1 dersi i�in do�ru, yanl�� ve bo� cevaplar�n tutulaca�� de�i�kenler
    public int aytSos1CorrectAnswers;
    public int aytSos1WrongAnswers;
    public int aytSos1EmptyAnswers;

    // Sosyal Bilimler-2 dersi i�in do�ru, yanl�� ve bo� cevaplar�n tutulaca�� de�i�kenler
    public int aytSos2CorrectAnswers;
    public int aytSos2WrongAnswers;
    public int aytSos2EmptyAnswers;

    // Matematik dersi i�in do�ru, yanl�� ve bo� cevaplar�n tutulaca�� de�i�kenler
    public int aytMatematikCorrectAnswers;
    public int aytMatematikWrongAnswers;
    public int aytMatematikEmptyAnswers;

    // Fen Bilimleri dersi i�in do�ru, yanl�� ve bo� cevaplar�n tutulaca�� de�i�kenler
    public int aytFenCorrectAnswers;
    public int aytFenWrongAnswers;
    public int aytFenEmptyAnswers;

    // Son be� net say�s�n� tutan liste
    public List<float> ayt_lastFiveNets = new List<float>();
}
