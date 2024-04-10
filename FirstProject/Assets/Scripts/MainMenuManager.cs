
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] GameObject[] mainMenuPanel;
    [SerializeField] TextMeshProUGUI languageButtonText;
    [SerializeField] TextMeshProUGUI settingsHeaderText;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] GameObject[] tilemaps;
    [SerializeField] new GameObject particleSystem;
    [SerializeField] AudioSource startSound;
    [SerializeField] TextMeshProUGUI coinScore;
    [SerializeField] GameObject[] coinGameobjects;
    CheckPointManager checkPointManager;
    LevelLockSystem levelLockSystem;
    int queue;
    int coin;
    int index;


    private void Awake()
    {

        BackgroundControl();
        CoinGameobjectControl();

       

    }

    private void Start()
    {

        levelLockSystem=FindObjectOfType<LevelLockSystem>();
        queue = 1;
        checkPointManager = FindObjectOfType<CheckPointManager>();
        coinScore.text = PlayerPrefs.GetInt("TotalCoins").ToString();
        

    }

    public void LoadPanel(string panelName)
    {

        foreach (GameObject panel in mainMenuPanel)
        {

            if (panel.name == panelName)
            {

                panel.SetActive(true);

            }

        }


    }

    public void ClosePanel(string panelName)
    {

        foreach (GameObject panel in mainMenuPanel)
        {

            if (panel.name == panelName)
            {

                panel.SetActive(false);

            }

        }



    }

    public void QuitGame()
    {

        Application.Quit();

    }

    public void PlayGame()
    {

        for(int i = 2; i <= 9;i++)
        {

            if (PlayerPrefs.HasKey("Lock" + i))
            {

                queue = i;

                if (queue == 9)
                {

                    queue = 8;
                }

            }



        }

        PlayStartSound();
        StartCoroutine(PlayChapterWithDelay(queue));
    }

    public void ChangeLanguage()
    {

        if (languageButtonText.text == "ENGLÝSH")
        {

            settingsHeaderText.text = "AYARLAR";
            languageButtonText.text = "TÜRKÇE";


        }

        else
        {

            settingsHeaderText.text = "SETTÝNGS";
            languageButtonText.text = "ENGLÝSH";

        }




    }

    public void StartChapter(int chapter)
    {


        PlayStartSound();
        StartCoroutine(PlayChapterWithDelay(chapter));
        checkPointManager.DeleteCheckPointPos();


        for (int i = 0; i < chapter; i++)
        {

            PlayerPrefs.DeleteKey("Boost" + i);

        }

        for (int i = 0; i < chapter; i++)
        {

            PlayerPrefs.DeleteKey("Health" + i);

        }

        PlayerPrefs.DeleteKey("Score");

    }

    public void Price()
    {


        for(int i = 2; i < 9;i++)
        {

            if (!PlayerPrefs.HasKey("Lock" + i))
            {

                switch (i)
                {

                    case 2:

                         coin = 1;
                         break;

                    case 3:

                        coin = 5;
                        break;

                    case 4:

                        coin = 10;
                        break;

                    case 5:

                        coin = 15;
                        break;

                    case 6:

                        coin = 20;
                        break;

                    case 7:

                        coin = 25;
                        break;

                    case 8:

                        coin = 30;
                        break;
                }

                break;

            }

           
        }


    }

    public void LevelControl()

    {

        for (int i = 2; i < 9; i++)
        {

            if (!PlayerPrefs.HasKey("Lock" + i))
            {

                index = i;               
                break;

            }


        }


    }

    public void YesButton()
    {

        if (PlayerPrefs.GetInt("TotalCoins") >= coin)
        {

            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - coin);
            PlayerPrefs.SetInt("Lock" + index, 1);
            PlayerPrefs.SetInt("NumberOfStartsLevel" + (index - 1), 0);
            PlayerPrefs.Save();
            coinScore.text = PlayerPrefs.GetInt("TotalCoins").ToString();
            ClosePanel("OpenLevelWithCoinPanel");
            CoinGameobjectControl();


            for (int i = 2; i <= levelLockSystem.buttons.Length + 1; i++)
            {

                if (PlayerPrefs.HasKey("Lock" + i))
                {

                    levelLockSystem.levelStars = levelLockSystem.buttons[i - 2].GetComponentsInChildren<Image>();

                    if (i != 9)
                    {

                        levelLockSystem.buttons[i - 1].GetComponent<Image>().sprite = levelLockSystem.sprite;
                        levelLockSystem.buttons[i - 1].interactable = true;
                        levelLockSystem.textMeshProUGUI[i-1].enabled = true;


                    }

                }

            }
        }

        else
        {

            LoadPanel("InsufficientCoinsPanel");
            Invoke(nameof(CloseInsufficientCoinsPanel),2);

        }
    }

  
    void CloseInsufficientCoinsPanel()
    {

        ClosePanel("InsufficientCoinsPanel");

    }
   

    public void PlayStartSound()
    {

        startSound.Play();

    }

    IEnumerator PlayChapterWithDelay(int chapter1)
    {

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(chapter1);

 }


  void BackgroundControl()
    {

        if (!PlayerPrefs.HasKey("Snow"))
        {

            backgrounds[2].SetActive(false);
            tilemaps[2].SetActive(false);
            backgrounds[0].SetActive(true);
            tilemaps[0].SetActive(true);
            PlayerPrefs.SetInt("Snow", 1);
            PlayerPrefs.DeleteKey("Forest");
            particleSystem.SetActive(true);

        }

        else if (!PlayerPrefs.HasKey("Cave"))
        {

            backgrounds[1].SetActive(true);
            tilemaps[1].SetActive(true);
            backgrounds[0].SetActive(false);
            tilemaps[0].SetActive(false);
            PlayerPrefs.SetInt("Cave", 1);

        }

        else
        {

            backgrounds[1].SetActive(false);
            tilemaps[1].SetActive(false);
            backgrounds[0].SetActive(false);
            tilemaps[0].SetActive(false);
            backgrounds[2].SetActive(true);
            tilemaps[2].SetActive(true);
            PlayerPrefs.SetInt("Forest", 1);
            PlayerPrefs.DeleteKey("Snow");
            PlayerPrefs.DeleteKey("Cave");

        }


    }


  void CoinGameobjectControl()
    {

        for (int i = 0; i < coinGameobjects.Length; i++)
        {

            if (!PlayerPrefs.HasKey("Lock" + (i + 2)))
            {

                if(i != 7)
                {

                    coinGameobjects[i + 1].SetActive(true);


                }

                

                if (i > 0)
                {

                    coinGameobjects[i].SetActive(false);

                }

                break;


            }


        }


    }

}
