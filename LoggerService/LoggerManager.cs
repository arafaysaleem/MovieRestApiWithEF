using Contracts;
using NLog;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        // Singleton instance of logger class
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        // Used to log general diagnostic messages
        public void LogDebug(string message) => logger.Debug(message);

        // Used to log error messages
        public void LogError(string message) => logger.Error(message);

        // Used to log informative messages
        public void LogInfo(string message) => logger.Info(message);

        // Used to log warning messages
        public void LogWarn(string message) => logger.Warn(message);
    }
}