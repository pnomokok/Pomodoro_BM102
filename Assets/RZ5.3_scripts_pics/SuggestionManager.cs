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
            "Günlük yürüyüþ yaparak stres seviyeni düþük tut.",
            "Hobilerinle ilgilen, örneðin kitap okumak veya müzik dinlemek.",
            "Günlük meditasyon yap.",
            "Düzenli nefes egzersizleri yap.",
            "Gevþeme teknikleri öðren ve uygula.",
            "Sosyal etkinliklere katýl ve arkadaþlarýnla zaman geçir."
        },
        new string[]
        {
            "15-20 dakikalýk hafif egzersiz yap.",
            "Derin nefes egzersizleri yaparak rahatla.",
            "Zaman yönetimi tekniklerini kullan.",
            "Dengeli ve saðlýklý beslenmeye özen göster.",
            "Yeterli uyku almaya dikkat et.",
            "Kýsa molalar ver ve gevþeme aktiviteleri yap.",
            "Doðada vakit geçir, yürüyüþe çýk."
        },
        new string[]
        {
            "Yoga veya pilates gibi rahatlama egzersizleri yap.",
            "Dýþarý çýkýp doðada vakit geçir.",
            "Kafein ve þeker tüketimini azalt.",
            "Profesyonel yardým almayý düþün.",
            "Aile veya arkadaþlarýnla konuþarak duygularýný paylaþ.",
            "Müzik dinleyerek veya resim yaparak stresini azalt.",
            "Rahatlama tekniklerini düzenli olarak uygula."
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
            stressLevelText.text = "Düsük Stres Seviyesi";
            suggestionText.text = GetRandomSuggestion(0);
        }
        else if (stressLevel < 26.5)
        {
            stressLevelText.text = "Orta Stres Seviyesi";
            suggestionText.text = GetRandomSuggestion(1);
        }
        else
        {
            stressLevelText.text = "Yüksek Stres Seviyesi";
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

