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

        if(languageButtonText.text == "ENGL�SH")
        {

            settingsHeaderText.text = "AYARLAR";
            winHeaderText.text = "KAZANDIN!";
            gameOverHeaderText.text = "KAYBETT�N!";
            pauseText.text = "OYUN DURDU";
            languageButtonText.text = "T�RK�E";


        }

        else
        {

            settingsHeaderText.text = "SETT�NGS";
            winHeaderText.text = "YOU W�N!";
            gameOverHeaderText.text = "GAME OVER";
            pauseText.text = "PAUSE";
            languageButtonText.text = "ENGL�SH";


        }
    }
}
