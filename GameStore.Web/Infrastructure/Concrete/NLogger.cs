using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Web.Infrastructure.Abstract;
using GameStore.Web.Infrastructure.Util;
using NLog;
using ILogger = GameStore.Web.Infrastructure.Abstract.ILogger;

namespace GameStore.Web.Infrastructure.Concrete
{
    public class NLogger : ILogger
    {
        private static Logger _logger;

        public NLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception ex)
        {
            string exceptionMessage = LogUtility.BuildException(ex);
            _logger.Error(exceptionMessage);
        }
    }
}