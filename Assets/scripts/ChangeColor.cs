using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ChangeColor : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI textMeshPro;

    public void ChangeToRed()
    {
        // RGB deðerlerini 0-255 aralýðýnda belirleyin
        int r = 255;
        int g = 255;
        int b = 255;

        // RGB deðerlerini 0-1 aralýðýna dönüþtürün
        Color customColor = new Color(r / 255f, g / 255f, b / 255f);
        img.color = customColor;
        textMeshPro.text = "Mola Zamani";
    }
    public void ChangeToGreen()
    {
        //img.color = Color.green;
        textMeshPro.text = "Ders zamani:( Uzgunum...";

    }
}
