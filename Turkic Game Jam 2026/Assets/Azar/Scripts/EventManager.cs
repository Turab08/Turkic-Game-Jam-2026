using System;
using UnityEngine;

public static class EventManager
{
    public static event Action OnGameFinished;
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;
    public static event Action<bool, GameObject> OnDraggingProcess;
    public static event Action OnFoodCooked;
    public static event Action<int> OnScoreChanged;
    public static event Action<int, bool> OnIngredientMatched;
    public static event Action<bool, string> OnInteractableHovered;

    public static void GameFinished()
    {
        OnGameFinished?.Invoke();
    }

    public static void GamePaused()
    {
        OnGamePaused?.Invoke();
    }
    public static void GameResume()
    {
        OnGameResumed?.Invoke();
    }

    public static void DraggingProcess(bool dragState, GameObject draggedObject)
    {
        OnDraggingProcess?.Invoke(dragState, draggedObject);
    }

    public static void FoodCooked()
    {
        OnFoodCooked?.Invoke();
    }

    public static void ScoreChanged(int score)
    {
        OnScoreChanged?.Invoke(score);
    }

    public static void IngredientMatched(int index, bool reset)
    {
        OnIngredientMatched?.Invoke(index, reset);
    }

    public static void InteractableHovered(bool hoverState, string iconIndex)
    {
        OnInteractableHovered?.Invoke(hoverState, iconIndex);
    }

}
