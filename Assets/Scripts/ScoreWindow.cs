using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreWindow : MonoBehaviour
{
    private Text highScoreText;
    private Text scoreText;
   // private Text highScoreText;


    private void Awake()
    {
        scoreText = transform.Find("ScoreText").GetComponent<Text>();
        highScoreText = transform.Find("HighScoreText").GetComponent<Text>();

    }

    private void Start()
    {
        highScoreText.text = Score.GetHighScore().ToString();
    }
    private void Update()
    {
        scoreText.text = LevelManager.GetInstance().GetCurrentScore().ToString();
      //  highScoreText.text = LevelManager.GetInstance().GetHighScore().ToString();

    }
}
