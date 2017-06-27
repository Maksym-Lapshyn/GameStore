using System;
using System.Web.Mvc;
using GameStore.Web.Infrastructure.Abstract;

namespace GameStore.Web.Infrastructure.Attributes
{
    public class LogEventsAndPerformanceAttribute : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        private int _startTime;

        public LogEventsAndPerformanceAttribute()
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
            _logger.LogEventsAndPerformance(message);
        }
    }
}