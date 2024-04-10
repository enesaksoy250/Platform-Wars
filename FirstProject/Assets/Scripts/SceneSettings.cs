using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSettings : MonoBehaviour
{
   
    GameManager gameManager;  
    PlayerScore playerScore;
    PlayerHealth playerHealth;
    CheckPointManager checkPointManager;
    BoostPotion boostPotion;
    HealthPotion healthPotion;
    int index;
    private void Awake()
    {

        checkPointManager=FindObjectOfType<CheckPointManager>();
        playerHealth=FindObjectOfType<PlayerHealth>();
        playerScore = FindObjectOfType<PlayerScore>();
        gameManager = FindObjectOfType<GameManager>();
        boostPotion = FindObjectOfType<BoostPotion>();
        healthPotion = FindObjectOfType<HealthPotion>();
        index = SceneManager.GetActiveScene().buildIndex;

    }

 

    public void QuitGame()
    {

        Application.Quit();

    }

    public void PauseToLoadMainMenu()
    {
        
        PlayerPrefs.SetInt("TotalStars", PlayerPrefs.GetInt("TotalStars") - PlayerPrefs.GetInt("Score"));        
        PlayerPrefs.DeleteKey("Star1");
        PlayerPrefs.DeleteKey("Star2");
        PlayerPrefs.DeleteKey("Star3");
        PlayerPrefs.DeleteKey("Score");
        
        for(int i = 1; i < 4; i++)
        {

            PlayerPrefs.DeleteKey("StarLevel" + index + i);


        }

        PlayerPrefs.Save();
        checkPointManager.checkpointPositions.Clear();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;

    }

    public void WinGameToLoadMainMenu()
    {

        PlayerPrefs.DeleteKey("Star1");
        PlayerPrefs.DeleteKey("Star2");
        PlayerPrefs.DeleteKey("Star3");
        PlayerPrefs.DeleteKey("Score");
        checkPointManager.checkpointPositions.Clear();
        SceneManager.LoadScene(0);
        PlayerPrefs.Save();
        Time.timeScale = 1;


    }


    public void RestartCurrentChapter()
    {


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("Current", 1);
        PlayerPrefs.Save();         
        gameManager.CloseAllPanel();


    }

    public void RestartCurrentChapterBeginning()
    {
        
        PlayerPrefs.SetInt("TotalStars", PlayerPrefs.GetInt("TotalStars") - PlayerPrefs.GetInt("Score"));
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        checkPointManager.checkpointPositions.Clear();
        playerScore.SetPlayerScore();        
        gameManager.CloseAllPanel();
        gameManager.starText.text = playerScore.GetPlayerScore().ToString();
        playerHealth.SetPlayerHealth(100);
        boostPotion.DeleteBoostPlayerPrefs();
        healthPotion.DeleteHealthPlayerPrefs();

    }

    public void NextChapter()
    {

        checkPointManager.DeleteCheckPointPos();
        playerScore.SetPlayerScore();
        PlayerPrefs.DeleteKey("Star1");
        PlayerPrefs.DeleteKey("Star2");
        PlayerPrefs.DeleteKey("Star3");
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;       

    }
}
