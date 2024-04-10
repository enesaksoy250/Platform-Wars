
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndChapter : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI winPanelCoinText;
    PlayerScore playerScore;
    GameManager gameManager;
    PlayerMove playerMove;
    PlayerHealth playerHealth;
    SoundControl soundControl;
    InterstitialAd interstitialAd;
    int allCoins;
    int index;
    void Start()
    {

        index = SceneManager.GetActiveScene().buildIndex;
        FindObject();

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            if (playerScore.GetPlayerScore() > 0 && playerHealth.isAlive)
            {


                playerMove.GetComponent<Animator>().SetBool("Run", false);
                playerHealth.isAlive = false;
                AllCoins();
                gameManager.LoadPanel("WinPanel", true);
                winPanelCoinText.text = PlayerPrefs.GetInt("TotalCoins").ToString();
                gameManager.NumberOfStar(playerScore.GetPlayerScore());
                soundControl.StopGameMusic();
                SetChapterNumberToPlayerPrefs();
                int buildIndex = SceneManager.GetActiveScene().buildIndex;
                PlayerPrefs.SetInt("NumberOfStarsLevel" + buildIndex, playerScore.GetPlayerScore());
                PlayerPrefs.Save();
   
                if (buildIndex == 8)
                {

                    gameManager.PlayFinalWinAudioSource();

                }

                else
                {

                    gameManager.PlayWinAudioSource();

                }

                interstitialAd.ShowInterstitialAd();
                
            }

        }

    }

    void SetChapterNumberToPlayerPrefs()
    {

        int index = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Lock"+(index+1),1);
        PlayerPrefs.Save();
       

    }


    void FindObject()
    {

        interstitialAd=FindObjectOfType<InterstitialAd>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMove = FindObjectOfType<PlayerMove>();
        gameManager = FindObjectOfType<GameManager>();
        playerScore = FindObjectOfType<PlayerScore>();
        soundControl= FindObjectOfType<SoundControl>();

    }



    void AllCoins()
    {

        for (int i = 1; i < 9; i++)
        {

            if (PlayerPrefs.HasKey("Level" + i))
            {


                allCoins += PlayerPrefs.GetInt("Level" + i);


            }


        }


    }
}
