using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{

  
     GameManager gameManager;
     int index;
   
    private void Start()
    {
 
        index = SceneManager.GetActiveScene().buildIndex;
       
        if(index != 0)
        {

            gameManager = FindObjectOfType<GameManager>();

        }
        
       
        
    }

    public void AddCoins()
    {

   
        PlayerPrefs.SetInt("Level" + index,PlayerPrefs.GetInt("Level"+index)+1);
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins")+1);
        PlayerPrefs.Save();


    }

    public void AddCoinRow(int coinRow)
    {

        PlayerPrefs.SetInt("Coin" +index+coinRow,1);
        PlayerPrefs.Save();

    }


   public void UpdateCoinUI()
    {

        gameManager.UpdateCoinText();
        
    }


}
