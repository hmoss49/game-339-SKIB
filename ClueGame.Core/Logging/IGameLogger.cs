namespace ClueGame.Core.Logging
{
    public interface IGameLogger
    {
        void LogDebug(string message);
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogGameEvent(string eventType, string eventData);
    }
}