using Xunit;
using ClueGame.Core.Logging;
using System.IO;

namespace ClueGame.Core.Tests
{
    public class LoggerTests
    {
        [Fact]
        public void FileLogger_WritesToFile()
        {
            // Arrange
            string testLogPath = "test_game.log";
            if (File.Exists(testLogPath))
                File.Delete(testLogPath);

            var logger = new FileLogger(testLogPath);

            // Act
            logger.LogInfo("Test log message");
            logger.LogGameEvent("TestEvent", "TestData");

            // Assert
            Assert.True(File.Exists(testLogPath));
            string content = File.ReadAllText(testLogPath);
            Assert.Contains("Test log message", content);
            Assert.Contains("TestEvent", content);

            // Cleanup
            File.Delete(testLogPath);
        }

        [Fact]
        public void ConsoleLogger_DoesNotThrow()
        {
            // Arrange
            var logger = new ConsoleLogger();

            // Act & Assert - should not throw
            logger.LogDebug("Debug message");
            logger.LogInfo("Info message");
            logger.LogWarning("Warning message");
            logger.LogError("Error message");
            logger.LogGameEvent("EventType", "EventData");
        }
    }
}