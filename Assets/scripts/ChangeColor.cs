//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;


//public class ChangeColor : MonoBehaviour
//{

//    public TextMeshProUGUI textMeshPro;

//    public void ChangeToRed()
//    {

//        textMeshPro.text = "Mola Zamani";
//    }
//    public void ChangeToGreen()
//    {

//        textMeshPro.text = "Ders zamani:( Uzgunum...";

//    }
//}

//using System.Collections;                                //0.set mola olan kod
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class ChangeColor : MonoBehaviour
//{
//    public TextMeshProUGUI textMeshPro;

//    public void SetTextToBreak(int setNumber)
//    {
//        textMeshPro.text = $"{setNumber}. Set Mola";
//    }

//    public void SetTextToWork(int setNumber)
//    {
//        textMeshPro.text = $"{setNumber}. Set Ders";
//    }
//}

using UnityEngine;
using TMPro;

public class ChangeColor : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public void UpdateText(string newText)
    {
        textMeshPro.text = newText;
    }
}

