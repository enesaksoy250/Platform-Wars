using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    [SerializeField] Image[] starImage;
    [SerializeField] Sprite starSprite;
    [SerializeField] GameObject[] gamePanel;
    [SerializeField] AudioSource coinAudioSource;
    [SerializeField] AudioSource starAudioSource;
    [SerializeField] AudioSource SwordEnemyAudioSource;
    [SerializeField] AudioSource SwordSingleAudioSource;
    [SerializeField] AudioSource PickUpHealthAudioSource;
    [SerializeField] AudioSource PickUpBoostAudioSource;
    [SerializeField] AudioSource WinAudioSource;
    [SerializeField] AudioSource FinalWinAudioSource;
    [SerializeField] AudioSource LoseAudioSource;
    PlayerScore playerScore;
    public GameObject[] starGameObjects;
    public GameObject[] coinGameObjects;
    public GameObject[] healthPotions;
    public GameObject[] boostPotions;
    public TextMeshProUGUI starText;
    public TextMeshProUGUI coinText;
    int index;
    

    private void Awake()
    {
       
        if (!PlayerPrefs.HasKey("Current"))
        {

            PlayerPrefs.DeleteKey("Star1");
            PlayerPrefs.DeleteKey("Star2");
            PlayerPrefs.DeleteKey("Star3");
            PlayerPrefs.Save();

        }


        StarAndCoinControl();
        BoostControl();
        HealthControl();
        


    }




    private void Start()
    {

        index = SceneManager.GetActiveScene().buildIndex;
        playerScore = FindObjectOfType<PlayerScore>();
        starText.text = playerScore.GetPlayerScore().ToString();
        coinText.text = PlayerPrefs.GetInt("TotalCoins").ToString();
       

    }

    
    void StarAndCoinControl()
    {

        for (int i = 1; i < 4; i++)
        {

            if (PlayerPrefs.HasKey("Star" + i))
            {

                starGameObjects[i - 1].SetActive(false);

            }

        }

        index = SceneManager.GetActiveScene().buildIndex;


        if (PlayerPrefs.HasKey("Level" + index))
        {

            for (int j = 0; j <= coinGameObjects.Length; j++)
            {


                if (PlayerPrefs.HasKey("Coin" + index + j))
                {

                    coinGameObjects[j].SetActive(false);

                }


            }

        }



    }

    void HealthControl()
    {

        for (int i = 0; i < healthPotions.Length; i++)
        {

            if (PlayerPrefs.HasKey("Health" + i))
            {

                healthPotions[i].SetActive(false);

            }


        }



    }
    void BoostControl()
    {

        for (int i = 0; i < boostPotions.Length; i++)
        {

            if (PlayerPrefs.HasKey("Boost" + i))
            {

                boostPotions[i].SetActive(false);

            }


        }


    }
   

    public void UpdateStartText()
    {

        starText.text = playerScore.GetPlayerScore().ToString();


    }


    public void UpdateCoinText()
    {

        coinText.text = PlayerPrefs.GetInt("TotalCoins").ToString();
    }



    public void PlayCoinAudio()
    {

        coinAudioSource.Play();

    }

    public void PlayStarAudio()
    {

        starAudioSource.Play();

    }

    public void PlaySwordEnemySound()
    {

        SwordEnemyAudioSource.Play();

    }

    public void PlaySwordSingleSound()
    {

        SwordSingleAudioSource.Play();

    }

    public void PlayPickUpHealthSound()
    {

        PickUpHealthAudioSource.Play();

    }

    public void PlayPickUpBoostSound()
    {

        PickUpBoostAudioSource.Play();

    }

    public void PlayWinAudioSource()
    {

        WinAudioSource.Play();

    }

    public void PlayFinalWinAudioSource()
    {

        FinalWinAudioSource.Play();

    }

    public void PlayLoseAudioSource()
    {

        LoseAudioSource.Play();

    }


    public void NumberOfStar(int starNum)
    {

        for (int i = 0; i < starNum; i++)
        {

            starImage[i].sprite = starSprite;

        }


    }

    public void LoadPanel(string panelName, bool setActive)
    {

        foreach (GameObject gameObject in gamePanel)
        {

            if (gameObject.name == panelName)
            {

                gameObject.SetActive(setActive);

                if (gameObject.name == "PausePanel" && setActive == true || gameObject.name == "SettingsPanel" && setActive == true)
                {

                    Time.timeScale = 0;

                }

            }

        }

    }

    public void CloseAllPanel()
    {

        foreach (GameObject gameObject in gamePanel)
        {

            gameObject.SetActive(false);
            Time.timeScale = 1;

        }

    }

    public void LoadPanel(string panelName)
    {

        foreach (GameObject gameObject in gamePanel)
        {

            if (gameObject.name == panelName)
            {

                CloseAllPanel();
                gameObject.SetActive(true);

                if (gameObject.name == "PausePanel" || gameObject.name == "SettingsPanel")
                {

                    Time.timeScale = 0;

                }

            }

        }


    }

    public void ClosePanel(string panelName)
    {


        foreach (GameObject gameObject in gamePanel)
        {

            if (gameObject.name == panelName)
            {


                gameObject.SetActive(false);
                Time.timeScale = 1;


            }

        }


    }


}
