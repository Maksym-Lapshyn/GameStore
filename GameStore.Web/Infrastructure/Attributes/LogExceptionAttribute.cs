using System.Web.Mvc;
using GameStore.Web.Infrastructure.Abstract;

namespace GameStore.Web.Infrastructure.Attributes
{
    public class LogExceptionAttribute : HandleErrorAttribute
    {
        private readonly ILogger _logger;

        public LogExceptionAttribute()
        {
            _logger = (ILogger)DependencyResolver.Current.GetService(typeof(ILogger));
        }

        public override void OnException(ExceptionContext filterContext)
        {
            _logger.LogException(filterContext.Exception);
        }
    }
}