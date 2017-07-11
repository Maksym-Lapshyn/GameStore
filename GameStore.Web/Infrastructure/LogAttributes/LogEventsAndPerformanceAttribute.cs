using GameStore.Web.Infrastructure.Abstract;
using System;
using System.Web.Mvc;

namespace GameStore.Web.Infrastructure.Filters
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
            var endTime = DateTime.UtcNow.Millisecond;
            var message = (endTime - _startTime).ToString();
            _logger.LogEventsAndPerformance(message);
        }
    }
}