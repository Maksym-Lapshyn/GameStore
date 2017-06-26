using System.Web.Mvc;
using GameStore.Web.Infrastructure.Attributes;

namespace GameStore.Web.App_Start
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