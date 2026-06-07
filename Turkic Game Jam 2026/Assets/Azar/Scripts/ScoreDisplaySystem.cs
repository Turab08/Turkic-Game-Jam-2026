using System;
using TMPro;
using UnityEngine;

public class ScoreDisplaySystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private int highScore;


    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void OnEnable()
    {
        EventManager.OnScoreChanged += Handle_OnScoreChanged;
    }

    private void Handle_OnScoreChanged(int scoreChange)
    {
        int newScore = GameManager.instance.CalculateNewScore(scoreChange);
        
        if (scoreChange > 0 && highScore < newScore)
        {
            highScore = newScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        scoreText.text = "Score: " + newScore.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}
