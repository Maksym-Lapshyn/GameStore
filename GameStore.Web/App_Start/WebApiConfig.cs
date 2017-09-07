using System.Web.Http;

namespace GameStore.Web
{
	public class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				"Login",
				"api/login",
				new { controller = "BaseApi", action = "Login" }
			);

			config.Routes.MapHttpRoute(
				"Get",
				"api/{lang}/{controller}/{contentType}",
				new { contentType = "json" },
				new { lang = "en|ru", contentType = "json|xml" }
			);

			config.Routes.MapHttpRoute(
				"Get Post Put Delete",
				"api/{lang}/{controller}/{key}/{contentType}",
				new { contentType = "json" },
				new { lang = "en|ru", contentType = "json|xml" }
			);

			config.Routes.MapHttpRoute(
				"Comments, genres and publishers of game",
				"api/{lang}/games/{key}/{controller}/{contentType}",
				new { contentType = "json", action = "GetAllByGameKey" },
				new { lang = "en|ru", contentType = "json|xml", controller = "comments|genres|publishers" }
			);

			config.Routes.MapHttpRoute(
				"Comments",
				"api/{lang}/games/{key}/comments/{id}/{contentType}",
				new { contentType = "json", controller = "comments" },
				new { lang = "en|ru", contentType = "json|xml" }
			);

			config.Routes.MapHttpRoute(
				"Games of publisher",
				"api/{lang}/publishers/{key}/games/{contentType}",
				new { contentType = "json", action = "GetAllByCompanyName", controller = "games" },
				new { lang = "en|ru", contentType = "json|xml" }
			);

			config.Routes.MapHttpRoute(
				"Games of genre",
				"api/{lang}/genres/{key}/games/{contentType}",
				new { contentType = "json", action = "GetAllByGenreName", controller = "games" },
				new { lang = "en|ru", contentType = "json|xml" }
			);
		}
	}
}