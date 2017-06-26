using System.Web.Mvc;
using GameStore.Web.Infrastructure.Abstract;

namespace GameStore.Web.Infrastructure.Attributes
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