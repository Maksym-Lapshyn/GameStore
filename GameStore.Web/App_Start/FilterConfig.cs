using System.Web.Mvc;
using GameStore.Web.Infrastructure.Filters;

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