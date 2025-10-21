namespace ClueGame.Core.Logging
{
    public class CompositeLogger : IGameLogger
    {
        private readonly IGameLogger[] _loggers;

        public CompositeLogger(params IGameLogger[] loggers)
        {
            _loggers = loggers;
        }

        public void LogDebug(string message)
        {
            foreach (var logger in _loggers)
                logger.LogDebug(message);
        }

        public void LogInfo(string message)
        {
            foreach (var logger in _loggers)
                logger.LogInfo(message);
        }

        public void LogWarning(string message)
        {
            foreach (var logger in _loggers)
                logger.LogWarning(message);
        }

        public void LogError(string message)
        {
            foreach (var logger in _loggers)
                logger.LogError(message);
        }

        public void LogGameEvent(string eventType, string eventData)
        {
            foreach (var logger in _loggers)
                logger.LogGameEvent(eventType, eventData);
        }
    }
}