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
				"GetAllGames",
				"games",
				new { controller = "Games", action = "ListAll" }
			);

			routes.MapRoute(
				"DisplayBasket",
				"busket",
				new { controller = "Orders", action = "Show" }
			);

			routes.MapRoute(
				"LeaveComment",
				"games/{gameKey}/{action}",
				new { controller = "Comments" },
				new { action = "NewComment"}
			);

			routes.MapRoute(
				"DisplayPublisher1",
				"{controller}/{action}",
				new { controller = "Publishers", action = "Show" },
				new { action = "New|History|Update|Remove"}
			);

			routes.MapRoute(
				"DisplayPublisher",
				"{controller}/key",
				new { action = "Show" }
			);

			routes.MapRoute(
				"DefaultWithGameKey",
				"{controller}/{gameKey}/{action}",
				new { action = "Show" }
			);

			routes.MapRoute(
				"Default",
				"{controller}/{action}",
				new { controller = "Games", action = "ListAll" }
			);
		}
	}
}