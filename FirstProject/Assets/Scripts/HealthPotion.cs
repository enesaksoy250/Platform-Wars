using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{

    [SerializeField] float boostHealth;
    PlayerHealth playerHealth;
    GameManager gameManager;

    void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
        playerHealth = FindObjectOfType<PlayerHealth>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            if (GetComponent<CapsuleCollider2D>().IsTouching(collision.GetComponent<CapsuleCollider2D>()))
            {

                for (int i = 0; i < gameManager.healthPotions.Length; i++)
                {

                    if (gameObject == gameManager.healthPotions[i])
                    {

                        PlayerPrefs.SetInt("Health" + i, 1);
                        gameManager.PlayPickUpHealthSound();
                        playerHealth.ChangeHealthForHealthPotion(boostHealth);
                        Destroy(gameObject);

                    }

                }

            }

        }



    }

    public void DeleteHealthPlayerPrefs()
    {

      
            for (int i = 0; i < gameManager.healthPotions.Length; i++)
            {

                PlayerPrefs.DeleteKey("Health" + i);


            }
       

    }

}
