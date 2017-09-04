using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.Localization;
using GameStore.DAL.Context;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Localization
{
	public class GameLocaleRepository : IGameLocaleRepository
	{
		private readonly GameStoreContext _context;

		public GameLocaleRepository(GameStoreContext context)
		{
			_context = context;
		}

		public IEnumerable<GameLocale> GetAllBy(int gameId)
		{
			return _context.GameLocales.Where(l => l.Game.Id == gameId);
		}
	}
}