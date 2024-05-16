using UnityEngine;

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
}