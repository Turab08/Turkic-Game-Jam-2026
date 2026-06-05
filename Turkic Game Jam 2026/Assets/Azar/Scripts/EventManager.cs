using System;

public static class EventManager
{
    public static event Action OnGameFinished;

    public static void GameFinished()
    {
        OnGameFinished?.Invoke();
    }
}
