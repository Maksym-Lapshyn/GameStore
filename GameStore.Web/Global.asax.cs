using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Web.App_Start;
using GameStore.Services.Infrastructure;
using GameStore.Web.Infrastructure.Attributes;

namespace GameStore.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.RegisterMappings();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}