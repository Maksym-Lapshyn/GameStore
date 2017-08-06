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
                new { controller = "Comments" }
            );

            routes.MapRoute(
                "DisplayPublisher1",
                "publishers/{companyName}",
                new { controller = "Publishers", action = "Show" }
            );

            routes.MapRoute(
                "DisplayPublisher",
                "publishers/{companyName}/{action}",
                new { controller = "Publishers" }
            );


            routes.MapRoute(
                "DefaultWithGameKey",
                "{controller}/{gameKey}/{action}",
                new { action = "Show", gameKey = UrlParameter.Optional }
            );


            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "Games", action = "ListAll" }
            );
        }
    }
}