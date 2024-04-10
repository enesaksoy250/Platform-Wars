using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{

    private static PlayerScore Instance;
   
   

    private void Awake()
    {

      
        if (Instance == null)
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayerPrefs.DeleteKey("Star1");
            PlayerPrefs.DeleteKey("Star2");
            PlayerPrefs.DeleteKey("Star3");
            PlayerPrefs.DeleteKey("Score");
            PlayerPrefs.Save();

        }

        else
        {
           
            Destroy(gameObject);

        }

  
    }


    public void EnterStar()
    {

      
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);      
        PlayerPrefs.Save();

    }


    public void EnterTotalStar()
    {

        PlayerPrefs.SetInt("TotalStars", PlayerPrefs.GetInt("TotalStars") + 1);
        PlayerPrefs.Save();

    }

    public int GetPlayerScore()
    {

        
        return PlayerPrefs.GetInt("Score");

    }

    public void SetPlayerScore()
    {

        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.Save();
    
    }

}
