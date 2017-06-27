using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Web.Infrastructure.Abstract
{
	//TODO: Required: Blank line after each method/property
	public interface ILogger
    {
        void LogEvent();
        void LogIp();
        void LogException(Exception exception);
        void LogPerformance(string message);
    }
}