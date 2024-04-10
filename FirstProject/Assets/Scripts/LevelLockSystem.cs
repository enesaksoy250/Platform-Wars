using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLockSystem : MonoBehaviour
{

    public Button[] buttons;
    public Sprite sprite;
    public Image[] levelStars;
    public TextMeshProUGUI [] textMeshProUGUI;
    public Sprite starSprite;

    private void Awake()
    {
        

        for(int i = 2; i <= textMeshProUGUI.Length; i++)
        {

            if (!PlayerPrefs.HasKey("Lock" + i))
            {

                textMeshProUGUI[i-1].enabled = false;


            }

        }

     
    }


    void Start()
    {


        for (int i = 2; i <= buttons.Length+1; i++)
        {

            if (PlayerPrefs.HasKey("Lock" + i))
            {
               
                levelStars = buttons[i - 2].GetComponentsInChildren<Image>();

                if (i !=9)
                {

                    buttons[i - 1].GetComponent<Image>().sprite = sprite;
                    buttons[i - 1].interactable = true;



                }


                if (PlayerPrefs.HasKey("NumberOfStarsLevel"+(i-1)))
                {
                    int number = PlayerPrefs.GetInt("NumberOfStarsLevel" + (i - 1));

                

                    for (int j = 0; j < number; j++)
                    {

                      
                          levelStars[j + 1].sprite = starSprite;
                          
                    }

                    for(int j = 0;j < 3; j++)
                    {

                        levelStars[j + 1].color = Color.white;

                    }

                }

               
            }

        }
    }


}
