using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewLessonData", menuName = "Lesson Data")]
public class LessonData : ScriptableObject
{
    public int TurkceCorrectAnswers;
    public int TurkceWrongAnswers;
    public int TurkceEmptyAnswers;

    public int MatematikCorrectAnswers;
    public int MatematikWrongAnswers;
    public int MatematikEmptyAnswers;

    public int FenCorrectAnswers;
    public int FenWrongAnswers;
    public int FenEmptyAnswers;

    public int SosyalCorrectAnswers;
    public int SosyalWrongAnswers;
    public int SosyalEmptyAnswers;

    public List<float> lastFiveNets = new List<float>();
}