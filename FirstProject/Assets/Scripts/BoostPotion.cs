using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoostPotion : MonoBehaviour
{

    [SerializeField] float boost;
    PlayerBoost playerBoostScript;
    GameManager gameManager;
    int index;
    void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
        playerBoostScript = FindObjectOfType<PlayerBoost>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            if (GetComponent<CapsuleCollider2D>().IsTouching(collision.GetComponent<CapsuleCollider2D>()))
            {

                for (int i = 0; i < gameManager.boostPotions.Length; i++)
                {

                    if (gameObject == gameManager.boostPotions[i])
                    {

                        PlayerPrefs.SetInt("Boost"+i, 1);
                        gameManager.PlayPickUpBoostSound();
                        playerBoostScript.SetBoost(boost);
                        Destroy(gameObject);

                    }

                }

            }  

        }    
    
    }


    public void DeleteBoostPlayerPrefs()
    {

       for(int i = 0 ;i<gameManager.boostPotions.Length; i++) 
       {

            PlayerPrefs.DeleteKey("Boost" + i);    
        
        
       }


    }



}
