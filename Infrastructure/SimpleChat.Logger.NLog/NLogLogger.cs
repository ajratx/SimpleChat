namespace SimpleChat.Logger.Nlog
{
    using System;

    public class NLogLogger : ILogger
    {
        private readonly NLog.Logger logger;

        public NLogLogger() => logger = NLog.LogManager.GetLogger(typeof(NLogLogger).FullName);

        public void Error(string message) => logger.Error(message);

        public void Error(string message, Exception e) => logger.Error(e, message);

        public void Info(string message) => logger.Info(message);

        public void Warn(string message) => logger.Warn(message);
    }
}
