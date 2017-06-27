using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Web.Infrastructure.Abstract;
using System.Diagnostics;

namespace GameStore.Web.Infrastructure.Attributes
{
    public class LogPerformanceAttribute : ActionFilterAttribute
    {
		//TODO: Consider: make fields readonly
		private ILogger _logger;
        private int _startTime;

        public LogPerformanceAttribute()
        {
            _logger = (ILogger)DependencyResolver.Current.GetService(typeof(ILogger));
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _startTime = DateTime.UtcNow.Millisecond;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            int endTime = DateTime.UtcNow.Millisecond;
            string message = (endTime - _startTime).ToString();
            _logger.LogPerformance(message);
        }
    }
}