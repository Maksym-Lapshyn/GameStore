using System.Web.Http;

namespace GameStore.Web
{
	public class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				"",
				"api/login",
				new { controller = "BaseApi", action = "Login" },
				new { lang = "en|ru" }
			);

			config.Routes.MapHttpRoute(
				"",
				"api/getlogin/{contentType}",
				new { controller = "BaseApi", action = "GetLogin", contentType = "json" },
				new { lang = "en|ru", contentType = "json|xml" }
			);

			config.Routes.MapHttpRoute(
				"",
				"api/{lang}/{controller}/{contentType}",
				new { contentType = "json"},
				new { lang = "en|ru", contentType = "json|xml" }
			);

			config.Routes.MapHttpRoute(
				"",
				"api/{lang}/{controller}/{key}/{contentType}",
				new { contentType = "json" },
				new { lang = "en|ru", contentType = "json|xml" }
			);

			config.Routes.MapHttpRoute(
				"",
				"api/{lang}/games/{key}/{controller}/{contentType}",
				new { contentType = "json" },
				new { lang = "en|ru", contentType = "json|xml" }
			);

			config.Routes.MapHttpRoute(
				"",
				"api/{lang}/games/{key}/{controller}/{id}/{contentType}",
				new { contentType = "json" },
				new { lang = "en|ru", contentType = "json|xml" }
			);
		}
	}
}