using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SuggestionManager : MonoBehaviour
{
    public TextMeshProUGUI stressLevelText;
    public TextMeshProUGUI suggestionText;
    public Button refreshButton;

    private string[][] suggestions = new string[][]
    {
        new string[]
        {
            "G�nl�k y�r�y�� yaparak stres seviyeni d���k tut.",
            "Hobilerinle ilgilen, �rne�in kitap okumak veya m�zik dinlemek.",
            "G�nl�k meditasyon yap.",
            "D�zenli nefes egzersizleri yap.",
            "Gev�eme teknikleri ��ren ve uygula.",
            "Sosyal etkinliklere kat�l ve arkada�lar�nla zaman ge�ir."
        },
        new string[]
        {
            "15-20 dakikal�k hafif egzersiz yap.",
            "Derin nefes egzersizleri yaparak rahatla.",
            "Zaman y�netimi tekniklerini kullan.",
            "Dengeli ve sa�l�kl� beslenmeye �zen g�ster.",
            "Yeterli uyku almaya dikkat et.",
            "K�sa molalar ver ve gev�eme aktiviteleri yap.",
            "Do�ada vakit ge�ir, y�r�y��e ��k."
        },
        new string[]
        {
            "Yoga veya pilates gibi rahatlama egzersizleri yap.",
            "D��ar� ��k�p do�ada vakit ge�ir.",
            "Kafein ve �eker t�ketimini azalt.",
            "Profesyonel yard�m almay� d���n.",
            "Aile veya arkada�lar�nla konu�arak duygular�n� payla�.",
            "M�zik dinleyerek veya resim yaparak stresini azalt.",
            "Rahatlama tekniklerini d�zenli olarak uygula."
        }
    };

    private void Start()
    {
        refreshButton.onClick.AddListener(RefreshSuggestions);
        DisplaySuggestions(SurveyData.totalScore);
    }

    void DisplaySuggestions(int stressLevel)
    {
        if (stressLevel < 13.3)
        {
            stressLevelText.text = "D�s�k Stres Seviyesi";
            suggestionText.text = GetRandomSuggestion(0);
        }
        else if (stressLevel < 26.5)
        {
            stressLevelText.text = "Orta Stres Seviyesi";
            suggestionText.text = GetRandomSuggestion(1);
        }
        else
        {
            stressLevelText.text = "Y�ksek Stres Seviyesi";
            suggestionText.text = GetRandomSuggestion(2);
        }
    }

    string GetRandomSuggestion(int index)
    {
        string[] selectedSuggestions = suggestions[index];
        int randomIndex = Random.Range(0, selectedSuggestions.Length);
        return selectedSuggestions[randomIndex];
    }

    void RefreshSuggestions()
    {
        DisplaySuggestions(SurveyData.totalScore);
    }
}

