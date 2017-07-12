using AutoMapper;
using GameStore.Services.Infrastructure;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			Mapper.Initialize(cfg => cfg.AddProfile(new ServiceProfile()));
			Mapper.Initialize(cfg => cfg.AddProfile(new WebProfile()));
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
		}
	}
}