using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Web.Infrastructure.Abstract;
using NLog;
using ILogger = GameStore.Web.Infrastructure.Abstract.ILogger;

namespace GameStore.Web.Infrastructure.Concrete
{
    public class NLogger : ILogger
    {
		//TODO: Consider: join all to one logger
		private Logger _ipLogger;
        private Logger _eventLogger;
        private Logger _exceptionLogger;
        private Logger _performanceLogger;

        public NLogger()
        {
            _ipLogger = LogManager.GetLogger("IpLogger");
            _eventLogger = LogManager.GetLogger("EventLogger");
            _exceptionLogger = LogManager.GetLogger("ExceptionLogger");
            _performanceLogger = LogManager.GetLogger("PerformanceLogger");
        }

        public void LogEvent()
        {
            _eventLogger.Debug(string.Empty);
        }

        public void LogIp()
        {
            _ipLogger.Info(string.Empty);
        }

        public void LogException(Exception exception)
        {
            _exceptionLogger.Error(exception);
        }

        public void LogPerformance(string message)
        {
            _performanceLogger.Debug(message);
        }
    }
}