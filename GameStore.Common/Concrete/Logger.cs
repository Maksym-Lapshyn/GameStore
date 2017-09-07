using NLog;
using System;

namespace GameStore.Common.Concrete
{
	public class Logger : Abstract.ILogger
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