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
                new {controller = "Game", action = "NewGame"}
            );

            routes.MapRoute(
                "EditGame",
                "games/update",
                new {controller = "Game", action = "UpdateGame"}
            );

            routes.MapRoute(
                "GetGameDetailsByKey",
                "game/{gameKey}",
                new {controller = "Game", action = "ShowGame"}
            );

            routes.MapRoute(
                "GetAllGames",
                "games",
                new {controller = "Game", action = "ListAllGames"}
            );

            routes.MapRoute(
                "DeleteGame",
                "games/remove",
                new {controller = "Game", action = "DeleteGame"}
            );

            routes.MapRoute(
                "LeaveComment",
                "game/{gameKey}/newcomment",
                new {controller = "Comment", action = "NewComment"}
            );

            routes.MapRoute(
                "GetAllComments",
                "game/{gameKey}/comments",
                new {controller = "Comment", action = "ListAllComments"}
            );

            routes.MapRoute(
                "DownloadGame",
                "game/{gameKey}/download",
                new {controller = "Game", action = "DownloadGame"}
            );

            routes.MapRoute(
                "CreatePublisher",
                "publisher/new",
                new {controller = "Publisher", action = "NewPublisher"}
            );

            routes.MapRoute(
                "DisplayPublisher",
                "publisher/{companyName}",
                new {controller = "Publisher", action = "ShowPublisher"}
            );

            routes.MapRoute(
                "DisplayBasket",
                "basket",
                new { controller = "Order", action = "ShowOrder" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}