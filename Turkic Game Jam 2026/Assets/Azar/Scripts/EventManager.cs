using System;
using UnityEngine;

public static class EventManager
{
    public static event Action OnGameFinished;
    public static event Action<bool, GameObject> OnDraggingProcess;
    public static event Action OnFoodCooked;
    public static event Action<int> OnScoreChanged;

    public static void GameFinished()
    {
        OnGameFinished?.Invoke();
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
}
