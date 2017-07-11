using GameStore.Web.Infrastructure.Abstract;
using System.Web.Mvc;

namespace GameStore.Web.Filters
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