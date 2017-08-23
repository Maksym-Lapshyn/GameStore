using GameStore.Services.Abstract;
using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace GameStore.Web.Filters
{
	public class CacheGamesCountAttribute : ActionFilterAttribute
	{
		private const string CacheKey = "GamesCount";

		private readonly IGameService _gameService;

		public CacheGamesCountAttribute()
		{
			_gameService = DependencyResolver.Current.GetService<IGameService>();
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
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