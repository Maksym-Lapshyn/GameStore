using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Web.Infrastructure.Abstract;

namespace GameStore.Web.Infrastructure.Attributes
{
    public class LogExceptionAttribute : HandleErrorAttribute
    {
		//TODO: Consider: make fields readonly
		private ILogger _logger;

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