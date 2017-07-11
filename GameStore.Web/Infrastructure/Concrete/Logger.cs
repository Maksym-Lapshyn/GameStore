using NLog;
using System;
using ILogger = GameStore.Web.Infrastructure.Abstract.ILogger;

namespace GameStore.Web.Infrastructure.Concrete
{
	public class Logger : ILogger
	{
		private readonly NLog.Logger _logger;

		public Logger()
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