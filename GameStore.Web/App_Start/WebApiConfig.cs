using System.Web.Http;

namespace GameStore.Web
{
	public class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				"ApiLogin",
				"api/login",
				new { controller = "Base", action = "Login" },
				new { lang = "en|ru" }
			);

			config.Routes.MapHttpRoute(
				"ApiController",
				"api/{lang}/{controller}",
				null,
				new { lang = "en|ru" }
			);

			config.Routes.MapHttpRoute(
				"ApiKey",
				"api/{lang}/{controller}/{key}",
				null,
				new { lang = "en|ru" }
			);

			config.Routes.MapHttpRoute(
				"ApiAction",
				"api/{lang}/{controller}/{key}/{action}",
				null,
				new { lang = "en|ru" }
			);
		}
	}
}