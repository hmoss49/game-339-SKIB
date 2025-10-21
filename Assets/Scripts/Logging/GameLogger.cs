using UnityEngine;
using ClueGame.Core.Logging;
using System.IO;

public static class GameLogger
{
    private static IGameLogger _logger;

    public static IGameLogger Instance
    {
        get
        {
            if (_logger == null)
            {
                Initialize();
            }
            return _logger;
        }
    }

    public static void Initialize()
    {
        string logPath = Path.Combine(Application.persistentDataPath, "game.log");
        
        var fileLogger = new FileLogger(logPath);
        var unityLogger = new UnityDebugLogger();
        
        _logger = new CompositeLogger(fileLogger, unityLogger);
        
        _logger.LogInfo($"Logger initialized. Log file: {logPath}");
    }
}