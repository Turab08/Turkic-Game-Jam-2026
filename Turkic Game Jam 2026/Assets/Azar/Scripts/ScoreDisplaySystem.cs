using TMPro;
using UnityEngine;

public class ScoreDisplaySystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    void OnEnable()
    {
        EventManager.OnScoreChanged += Handle_OnScoreChanged;
    }

    private void Handle_OnScoreChanged(int scoreChange)
    {
        int newScore = GameManager.instance.CalculateNewScore(scoreChange);

        scoreText.text = "Score: " + newScore.ToString();
    }
}
