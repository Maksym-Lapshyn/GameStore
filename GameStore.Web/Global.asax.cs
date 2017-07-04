﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Web.App_Start;
using GameStore.Services.Infrastructure;
using GameStore.Web.Infrastructure.Filters;

namespace GameStore.Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			ServicesAutoMapperConfig.RegisterMappings();
			WebAutoMapperConfig.RegisterMappings();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
		}
	}
}