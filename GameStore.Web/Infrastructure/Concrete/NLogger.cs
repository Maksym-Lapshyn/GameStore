using System;
using NLog;
using ILogger = GameStore.Web.Infrastructure.Abstract.ILogger;

namespace GameStore.Web.Infrastructure.Concrete
{
    public class NLogger : ILogger
    {
        private readonly Logger _logger;

        public NLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void LogIp()
        {
            _logger.Info(string.Empty);
        }

        public void LogException(Exception exception)
        {
            _logger.Error(exception);
        }

        public void LogEventsAndPerformance(string message)
        {
            _logger.Debug(message);
        }
    }
}