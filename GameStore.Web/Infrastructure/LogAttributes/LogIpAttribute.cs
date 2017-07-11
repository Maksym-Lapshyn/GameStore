using GameStore.Web.Infrastructure.Abstract;
using System.Web.Mvc;

namespace GameStore.Web.Infrastructure.Filters
{
    public class LogIpAttribute : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public LogIpAttribute()
        {
            _logger = (ILogger)DependencyResolver.Current.GetService(typeof(ILogger));
        }
        
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        { 
            _logger.LogIp();
        }
    }
}