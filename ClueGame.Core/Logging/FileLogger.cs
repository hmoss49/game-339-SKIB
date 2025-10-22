using System;
using System.IO;

namespace ClueGame.Core.Logging
{
    public class FileLogger : IGameLogger
    {
        private readonly string _logFilePath;

        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
            
            string directory = Path.GetDirectoryName(_logFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void LogDebug(string message)
        {
            WriteLog("DEBUG", message);
        }

        public void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public void LogWarning(string message)
        {
            WriteLog("WARNING", message);
        }

        public void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        public void LogGameEvent(string eventType, string eventData)
        {
            WriteLog("GAME_EVENT", $"{eventType} | {eventData}");
        }

        private void WriteLog(string level, string message)
        {
            try
            {
                string logEntry = $"[{level}] {message}";
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write log: {ex.Message}");
            }
        }
    }
}