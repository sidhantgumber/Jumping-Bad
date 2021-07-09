using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverWindow : MonoBehaviour
{
    private Text scoreText;
    private Text highScoreText;
    public static GameOverWindow instance;
   

    public static GameOverWindow GetInstance()
    {
        return instance;
    }

   // private Bird bird;
    private void Awake()
    {
        instance = this;
      
        scoreText = transform.Find("ScoreText").GetComponent<Text>();
        highScoreText = transform.Find("HighScore").GetComponent<Text>();
       
        Hide();
    }

    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");

    }
   

    public void ShowPopUpOnBirdDeath()
    {
        
        scoreText.text = LevelManager.GetInstance().GetCurrentScore().ToString();
        if (LevelManager.GetInstance().GetCurrentScore() >= Score.GetHighScore())
        {
            highScoreText.text = "NEW HIGHSCORE";
        }
        else
        {
            highScoreText.text = "HIGHSCORE: " + Score.GetHighScore();
        }
        
        Show();
        Debug.Log("mar gayi");


    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

}
