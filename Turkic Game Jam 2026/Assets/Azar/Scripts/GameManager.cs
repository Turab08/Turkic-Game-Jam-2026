using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<GameObject> activeItems = new();
    private FoodData currentFoodToBeCooked = null;
    private int gameScore = 0;

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

    public FoodData GetCurrentFoodToBeCooked()
    {
        return currentFoodToBeCooked;
    }

    public void SetCurrentFoodToBeCooked(FoodData data) 
    {
        currentFoodToBeCooked = data;
    }

    private void Handle_OnScoreChanged(int scoreIncrease)
    {
        gameScore += scoreIncrease;
    }
}
