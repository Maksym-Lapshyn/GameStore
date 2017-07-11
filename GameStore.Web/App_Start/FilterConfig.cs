using GameStore.Web.Filters;
using System.Web.Mvc;

namespace GameStore.Web
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new LogEventsAndPerformanceAttribute());
			filters.Add(new LogExceptionAttribute());
			filters.Add(new LogIpAttribute());
		}
	}
}