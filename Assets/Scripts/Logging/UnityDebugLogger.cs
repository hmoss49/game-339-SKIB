using UnityEngine;
using ClueGame.Core.Logging;

public class UnityDebugLogger : IGameLogger
{
    public void LogDebug(string message)
    {
        Debug.Log($"[DEBUG] {message}");
    }

    public void LogInfo(string message)
    {
        Debug.Log($"[INFO] {message}");
    }

    public void LogWarning(string message)
    {
        Debug.LogWarning(message);
    }

    public void LogError(string message)
    {
        Debug.LogError(message);
    }

    public void LogGameEvent(string eventType, string eventData)
    {
        Debug.Log($"<color=cyan>[GAME_EVENT] {eventType} | {eventData}</color>");
    }
}