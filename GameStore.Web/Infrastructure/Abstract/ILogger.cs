using System;

namespace GameStore.Web.Infrastructure.Abstract
{
	public interface ILogger
    {
        void LogIp();

        void LogException(Exception exception);

        void LogEventsAndPerformance(string message);
    }
}