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
				"CreateGame",
				"games/new",
				new { controller = "Games", action = "New" }
			);

			routes.MapRoute(
				"EditGame",
				"games/update",
				new { controller = "Games", action = "Update" }
			);

			routes.MapRoute(
				"GetGameDetailsByKey",
				"game/{gameKey}",
				new { controller = "Games", action = "Show" }
			);

			routes.MapRoute(
				"GetAllGames",
				"games",
				new { controller = "Games", action = "ListAll" }
			);

			routes.MapRoute(
				"DeleteGame",
				"games/remove",
				new { controller = "Games", action = "Delete" }
			);

			routes.MapRoute(
				"LeaveComment",
				"game/{gameKey}/newcomment",
				new { controller = "Comments", action = "New" }
			);

			routes.MapRoute(
				"GetAllComments",
				"game/{gameKey}/comments",
				new { controller = "Comments", action = "ListAll" }
			);

			routes.MapRoute(
				"DownloadGame",
				"game/{gameKey}/download",
				new { controller = "Games", action = "Download" }
			);

			routes.MapRoute(
				"CreatePublisher",
				"publisher/new",
				new { controller = "Publishers", action = "New" }
			);

			routes.MapRoute(
				"DisplayPublisher",
				"publisher/{companyName}",
				new { controller = "Publishers", action = "Show" }
			);

			routes.MapRoute(
				"DisplayBasket",
				"busket",
				new { controller = "Orders", action = "Show" }
			);

			routes.MapRoute(
				"DisplayOrdersHistory",
				"orders/history",
				new { controller = "Orders", action = "ListAll" }
			);

			routes.MapRoute(
				"BuyProduct",
				"game/{gamekey}/buy",
				new { controller = "Orders", action = "Update" }
			);

			routes.MapRoute(
				"Default",
				"{controller}/{action}",
				new { controller = "Games", action = "ListAll" }
			);
		}
	}
}