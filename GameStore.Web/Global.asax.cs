using System;
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
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

		}

		public override void Init()
		{
			base.Init();
			AcquireRequestState += ShowRouteValues;
		}

		protected void ShowRouteValues(object sender, EventArgs e)
		{
			var context = HttpContext.Current;
			if (context == null)
				return;
			var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(context));
		}
	}
}
