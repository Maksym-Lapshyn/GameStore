using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"",
				"{lang}/{controller}",
				new { action = "ShowAll" },
				new
				{
					controller = "Games|Comments|Publishers|Genres|Orders|Roles|Users|",
					lang = "en|ru"
				}
			);

			routes.MapRoute(
				"",
				"{lang}/busket",
				new { controller = "Orders", action = "Busket" },
				new { lang = "en|ru" }
			);

			routes.MapRoute(
				"",
				"{lang}/games/{key}/comments",
				new { controller = "Comments", action = "New" },
				new { lang = "en|ru" }
			);

			routes.MapRoute(
				"",
				"{lang}/{controller}/{action}",
				null,
				new
				{
					action = "New|History|Update|Login|Logout|Register|ShowAll|Delete|Confirm|Ship|AddDetails|DeleteDetails|Buy",
					lang = "en|ru"
				}
			);

			routes.MapRoute(
				"",
				"{lang}/{controller}/{key}/{action}",
				new { lang = "en|ru" }
			);

			routes.MapRoute(
				"",
				"{lang}/{controller}/{*key}",
				new { action = "Show" },
				new { lang = "en|ru" }
			);

			routes.MapRoute(
				"Default",
				"{controller}/{action}",
				new { controller = "Games", action = "ShowAll", lang = "en" }
			);
		}
	}
}