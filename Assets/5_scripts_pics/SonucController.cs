using UnityEngine;
using UnityEngine.UI;

public class SonucController : MonoBehaviour
{
    public Text nabizText;
    public Text stresText;

    void Start()
    {
        int nabiz = PlayerPrefs.GetInt("Nabiz");
        string stresSeviyesi = PlayerPrefs.GetString("StresSeviyesi");

        nabizText.text = "Nab�z De�eriniz: " + nabiz.ToString();
        stresText.text = "Stres Seviyeniz: " + stresSeviyesi;
    }
}
