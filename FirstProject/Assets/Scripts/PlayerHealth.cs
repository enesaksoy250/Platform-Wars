using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI gameOverCoinText;
    Animator animator;
    GameManager gameManager;
    SoundControl soundControl;
    PlayerMove playerMove;
    InterstitialAd interstitialAd;
    public bool isAlive;
    float playerHealth;

    void Start()
    {
        
        interstitialAd=FindObjectOfType<InterstitialAd>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        soundControl = FindObjectOfType<SoundControl>();
        playerMove = FindObjectOfType<PlayerMove>();
        isAlive = true;

    }



    public void SetPlayerHealth(float health)
    {

        playerHealth = health;
        healthText.text = playerHealth.ToString();
        healthSlider.value = playerHealth / 100;

    }
    public float GetPlayerHealth()
    {

        return playerHealth;

    }

    public void ChangeHealth(float enemyPower)
    {

        playerHealth -= enemyPower;
        healthText.text = playerHealth.ToString();
        healthSlider.value -= enemyPower / 100;


        if (playerHealth <= 0)
        {

            playerHealth = 0;
            healthText.text = playerHealth.ToString();
            Die();


        }

    }

    public void Die()
    {

        playerMove.isJump = false;
        animator.SetTrigger("Die");
        isAlive = false;
        gameOverCoinText.text = PlayerPrefs.GetInt("TotalCoins").ToString();
        gameManager.LoadPanel("GameOverPanel", true);
        soundControl.StopGameMusic();
        gameManager.PlayLoseAudioSource();

        PlayerPrefs.SetInt("Die", PlayerPrefs.GetInt("Die") + 1);


        if (PlayerPrefs.GetInt("Die") == 1)
        {

            interstitialAd.LoadInterstitialAd();

        }


        if (PlayerPrefs.GetInt("Die") == 5)
        {           
            PlayerPrefs.SetInt("Die", 0);
            interstitialAd.ShowInterstitialAd();
            PlayerPrefs.Save();
        }
           
        

    }

    public void ChangeHealthForHealthPotion(float health)
    {

        playerHealth += health;
        healthText.text = playerHealth.ToString();
        healthSlider.value += health / 100;

        if (playerHealth > 100)
        {

            playerHealth = 100;
            healthText.text = playerHealth.ToString();

        }


    }

   
}
