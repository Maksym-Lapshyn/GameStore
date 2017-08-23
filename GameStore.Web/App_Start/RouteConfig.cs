using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.LowercaseUrls = true;

			routes.MapRoute(
				"",
				"{controller}",
				new { action = "ShowAll" },
				new { controller = "Games|Comments|Publishers|Genres|Orders|Roles|Users|" }
			);

			routes.MapRoute(
				"",
				"busket",
				new { controller = "Orders", action = "Busket" }
			);

			routes.MapRoute(
				"",
				"games/{key}/comments",
				new { controller = "Comments", action = "New" }
			);

			routes.MapRoute(
				"",
				"{controller}/{action}",
				null,
				new { action = "New|History|Update|ShowCount|Login|Logout|Register|ShowAll|Delete|Confirm|Ship|AddDetails|DeleteDetails|Buy" }
			);

			routes.MapRoute(
				"",
				"{controller}/{key}/{action}"
			);

			routes.MapRoute(
				"",
				"{controller}/{*key}",
				new { action = "Show" }
			);

			routes.MapRoute(
				"Default",
				"{controller}/{action}",
				new { controller = "Games", action = "ShowAll" }
			);
		}
	}
}