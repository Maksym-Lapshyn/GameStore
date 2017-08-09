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
				"games/{key}/comments",
				new { controller = "Comments", action = "NewComment" }
			);

			routes.MapRoute(
				"DisplayPublisher1",
				"{controller}/{action}",
				new { controller = "Games", action = "ListAll" },
				new { action = "New|History|Update|Remove|ShowCount" }
			);

			routes.MapRoute(
				"DefaultWithGameKey",
				"{controller}/{key}/{action}",
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