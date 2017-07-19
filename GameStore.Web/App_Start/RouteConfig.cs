using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.Web
{
	public class RouteConfig //TODO Consider: Add Default route, //TODO Consider: reduce routes amount
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.LowercaseUrls = true;

			routes.MapRoute(
				"CreateGame",
				"games/new",
				new {controller = "Game", action = "New"}
			);

			routes.MapRoute(
				"EditGame",
				"games/update",
				new {controller = "Game", action = "Update"}
			);

			routes.MapRoute(
				"GetGameDetailsByKey",
				"game/{gameKey}",
				new {controller = "Game", action = "Show"}
			);

			routes.MapRoute(
				"GetAllGames",
				"games",
				new {controller = "Game", action = "ListAll"}
			);

			routes.MapRoute(
				"DeleteGame",
				"games/remove",
				new {controller = "Game", action = "Delete"}
			);

			routes.MapRoute(
				"LeaveComment",
				"game/{gameKey}/newcomment",
				new {controller = "Comment", action = "New"}
			);

			routes.MapRoute(
				"GetAllComments",
				"game/{gameKey}/comments",
				new {controller = "Comment", action = "ListAll"}
			);

			routes.MapRoute(
				"DownloadGame",
				"game/{gameKey}/download",
				new {controller = "Game", action = "Download"}
			);

			routes.MapRoute(
				"CreatePublisher",
				"publisher/new",
				new {controller = "Publisher", action = "New"}
			);

			routes.MapRoute(
				"DisplayPublisher",
				"publisher/{companyName}",
				new {controller = "Publisher", action = "Show"}
			);

			routes.MapRoute(
				"DisplayBasket",
				"basket",
				new { controller = "Order", action = "Show" }
			);

			routes.MapRoute(
				"Empty",
				"{controller}/{action}",
				new {controller = "Game", action = "ListAll"}
			);
		}
	}
}