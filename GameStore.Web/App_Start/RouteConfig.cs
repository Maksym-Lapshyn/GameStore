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
				new { controller = "Games/Genres/Publishers/Roles/Users"}
			);

			routes.MapRoute(
				"",
				"busket",
				new { controller = "Orders", action = "Show" }
			);

			routes.MapRoute(
				"",
				"games/{key}/comments",
				new { controller = "Comments", action = "NewComment" }
			);

			routes.MapRoute(
				"",
				"{controller}/{action}",
				new { controller = "Games", action = "ShowAll" },
				new { action = "New|History|Update|Delete|ShowCount" }
			);

			routes.MapRoute(
				"",
				"{controller}/{key}/{action}",
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