using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    CoinManager coinManager;
    GameManager gameManager;
    public int coinValue = 1;


    private void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
        coinManager = FindObjectOfType<CoinManager>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            if (GetComponent<CircleCollider2D>().IsTouching(collision.GetComponent<CapsuleCollider2D>()))
            {

                for (int i = 0; i < gameManager.coinGameObjects.Length; i++)
                {

                    if (gameObject == gameManager.coinGameObjects[i])
                    {

                        gameManager.PlayCoinAudio();
                        coinManager.AddCoins();
                        coinManager.AddCoinRow(i);
                        gameManager.UpdateCoinText();
                        Destroy(gameObject);

                    }

                }

            }

        }
    }
}
