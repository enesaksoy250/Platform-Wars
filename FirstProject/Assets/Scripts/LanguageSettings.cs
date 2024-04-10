using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSettings : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI settingsHeaderText;
    [SerializeField] TextMeshProUGUI winHeaderText;
    [SerializeField] TextMeshProUGUI gameOverHeaderText;
    [SerializeField] TextMeshProUGUI pauseText;
    [SerializeField] TextMeshProUGUI languageButtonText;
   


    public void ChangeLanguage()
    {

        if(languageButtonText.text == "ENGLÝSH")
        {

            settingsHeaderText.text = "AYARLAR";
            winHeaderText.text = "KAZANDIN!";
            gameOverHeaderText.text = "KAYBETTÝN!";
            pauseText.text = "OYUN DURDU";
            languageButtonText.text = "TÜRKÇE";


        }

        else
        {

            settingsHeaderText.text = "SETTÝNGS";
            winHeaderText.text = "YOU WÝN!";
            gameOverHeaderText.text = "GAME OVER";
            pauseText.text = "PAUSE";
            languageButtonText.text = "ENGLÝSH";


        }
    }
}
