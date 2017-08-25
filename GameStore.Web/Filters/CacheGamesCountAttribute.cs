using GameStore.Services.Abstract;
using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace GameStore.Web.Filters
{
	public class CacheGamesCountAttribute : ActionFilterAttribute
	{
		private IGameService _gameService;

		public static string CacheKey = "GamesCount";

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			_gameService = DependencyResolver.Current.GetService<IGameService>();

			if (HttpContext.Current.Cache[CacheKey] == null)
			{
				HttpContext.Current.Cache.Add(CacheKey,
					_gameService.GetCount(),
					null,
					DateTime.UtcNow.AddSeconds(60),
					Cache.NoSlidingExpiration,
					CacheItemPriority.Default,
					null);
			}

			base.OnActionExecuted(filterContext);
		}
	}
}