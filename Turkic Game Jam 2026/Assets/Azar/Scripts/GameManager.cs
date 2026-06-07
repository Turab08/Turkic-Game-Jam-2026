using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<GameObject> activeItems = new();
    private MenuItem currentFoodToBeCooked = null;
    private List<KeyValuePair<FoodData, bool>> ingredients = new();
    private int gameScore = 0;
    private float scoreMultiplier = 1;


    [SerializeField] private TMP_Text foodNameText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Animator gameOverAnimator;
    [SerializeField] private Animator pauseAnimator;

    [SerializeField] private PotCap potCap;



    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void OnEnable()
    {
        EventManager.OnScoreChanged += Handle_OnScoreChanged;
        EventManager.OnGameFinished += Handle_OnGameFinished;
        EventManager.OnGamePaused += Handle_OnGamePaused;
        EventManager.OnGameResumed += Handle_OnGameResumed;
    }

    void OnDisable()
    {
        EventManager.OnScoreChanged -= Handle_OnScoreChanged;
        EventManager.OnGameFinished -= Handle_OnGameFinished;
         EventManager.OnGamePaused -= Handle_OnGamePaused;
        EventManager.OnGameResumed -= Handle_OnGameResumed;
    }

    public void AddItem(GameObject item)
    {
        activeItems.Add(item);
    }

    public void RemoveItem(GameObject item)
    {
        if(activeItems.Count > 0)
        {
            activeItems.Remove(item);
        }
    }

    public List<GameObject> GetActiveItems()
    {
        return activeItems;
    }

    public MenuItem GetCurrentFoodToBeCooked()
    {
        return currentFoodToBeCooked;
    }

    public int GetGameScore()
    {
        return gameScore;
    }

    public int CalculateNewScore(int scoreChange)
    {
        int newScore = gameScore + (int)Math.Floor(scoreChange * scoreMultiplier);

        if (scoreChange > 0)
        {
            scoreMultiplier *= 1.2f;
        }
        else
        {
            scoreMultiplier = 1;
        }

        return (newScore < 0 ? 0 : newScore);
    }

    public void SetCurrentFoodToBeCooked(MenuItem data) 
    {
        currentFoodToBeCooked = data;
        foodNameText.text = data.menuItemName;
        ingredients.Clear();
        ingredients.Add(new KeyValuePair<FoodData, bool>(data.food1, false));
        ingredients.Add(new KeyValuePair<FoodData, bool>(data.food2, false));
        ingredients.Add(new KeyValuePair<FoodData, bool>(data.food3, false));
    }

    private void Handle_OnScoreChanged(int scoreIncrease)
    {
        gameScore = CalculateNewScore(scoreIncrease);
    }

    public List<KeyValuePair<FoodData, bool>> GetIngredients()
    {
        return ingredients;
    }

    private void Handle_OnGameFinished()
    {
        StartCoroutine(GameOver());
    }

    private void Handle_OnGamePaused()
    {
        StartCoroutine(Pause());
    }
    private void Handle_OnGameResumed()
    {
        StartCoroutine(Resume());
    }

    IEnumerator GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverAnimator.SetBool("IsPaused", true);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }

    IEnumerator Pause()
    {
        pausePanel.SetActive(true);
        pauseAnimator.SetBool("IsPaused", true);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }

    IEnumerator Resume()
    {
        pausePanel.SetActive(false);
        pauseAnimator.SetBool("IsPaused", false);
        potCap.ReturnBack();
        Time.timeScale = 1;
        yield return new WaitForSeconds(1f);
    }
}
