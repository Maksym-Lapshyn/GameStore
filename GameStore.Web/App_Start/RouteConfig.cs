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
                name: "CreateGame",
                url: "games/new",
                defaults: new { controller = "Game", action = "NewGame" }
            );

            routes.MapRoute(
                name: "EditGame",
                url: "games/update",
                defaults: new { controller = "Game", action = "UpdateGame" }
            );

            routes.MapRoute(
                name: "GetGameDetailsByKey",
                url: "game/{gameKey}",
                defaults: new { controller = "Game", action = "ShowGame" }
            );

            routes.MapRoute(
                name: "GetAllGames",
                url: "games",
                defaults: new { controller = "Game", action = "ListAllGames" }
            );

            routes.MapRoute(
                name: "DeleteGame",
                url: "games/remove",
                defaults: new { controller = "Game", action = "DeleteGame" }
            );

            routes.MapRoute(
                name: "LeaveComment",
                url: "game/{gameKey}/newcomment",
                defaults: new { controller = "Comment", action = "NewComment" }
            );

            routes.MapRoute(
                name: "GetAllComments",
                url: "game/{gameKey}/comments",
                defaults: new { controller = "Comment", action = "ListAllComments" }
            );

            routes.MapRoute(
                name: "DownloadGame",
                url: "game/{gameKey}/download",
                defaults: new { controller = "Game", action = "DownloadGame" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
