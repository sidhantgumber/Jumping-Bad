using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score

{
    public static void Start()
    {
        ResetHighScore();
    }
    
    
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }
    
    public static bool SetNewHighScore(int score)
    {
        int currentHighScore = GetHighScore();
        if (currentHighScore<score) {
            PlayerPrefs.SetInt("HighScore", score);
             PlayerPrefs.Save();
            return true; // we set a new highscore

        }

        else
        {
            return false;
        }
    }

    public static void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();
    }
}
