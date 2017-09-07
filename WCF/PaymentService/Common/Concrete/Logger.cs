using NLog;
using ILogger = Common.Abstract.ILogger;

namespace Common.Concrete
{
	public class Logger : ILogger
	{
		private readonly NLog.Logger _logger;

		public Logger()
		{
			_logger = LogManager.GetCurrentClassLogger();
		}

		public void LogPayment(string message)
		{
			_logger.Info(message);
		}
	}
}