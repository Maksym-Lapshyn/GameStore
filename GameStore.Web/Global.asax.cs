using GameStore.Web.App_Start;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
		}
	}
}