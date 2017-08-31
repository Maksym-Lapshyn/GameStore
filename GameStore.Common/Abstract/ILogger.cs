using System;

namespace GameStore.Common.Abstract
{
	public interface ILogger
	{
		void LogIp();

		void LogException(Exception exception);

		void LogEventsAndPerformance(string message);
	}
}