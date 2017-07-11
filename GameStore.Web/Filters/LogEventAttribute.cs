using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Web.Infrastructure.Abstract;

namespace GameStore.Web.Infrastructure.Attributes
{
    public class LogEventAttribute : ActionFilterAttribute
    {
		//TODO: Consider: make fields readonly
		private ILogger _logger;

        public LogEventAttribute()
        {
            _logger = (ILogger)DependencyResolver.Current.GetService(typeof(ILogger));
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _logger.LogEvent();
        }
    }
}