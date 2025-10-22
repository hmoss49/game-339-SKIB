using System;

namespace ClueGame.Core.Logging
{
    public class ConsoleLogger : IGameLogger
    {
        public void LogDebug(string message)
        {
            Console.WriteLine($"[DEBUG] {message}");
        }

        public void LogInfo(string message)
        {
            Console.WriteLine($"[INFO] {message}");
        }

        public void LogWarning(string message)
        {
            Console.WriteLine($"[WARNING] {message}");
        }

        public void LogError(string message)
        {
            Console.WriteLine($"[ERROR] {message}");
        }

        public void LogGameEvent(string eventType, string eventData)
        {
            Console.WriteLine($"[GAME_EVENT] {eventType} | {eventData}");
        }
    }
}