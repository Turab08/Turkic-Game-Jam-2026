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


    [SerializeField] private TMP_Text foodNameText;
    [SerializeField] private GameObject gameOverPanel;


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
    }

    void OnDisable()
    {
        EventManager.OnScoreChanged += Handle_OnScoreChanged;
        EventManager.OnGameFinished -= Handle_OnGameFinished;
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
        int newScore = gameScore + scoreChange;
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

    IEnumerator GameOver()
    {
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
}
