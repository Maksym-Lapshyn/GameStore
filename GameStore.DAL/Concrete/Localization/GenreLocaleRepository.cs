using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.Localization;
using GameStore.DAL.Context;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Localization
{
	public class GenreLocaleRepository : IGenreLocaleRepository
	{
		private readonly GameStoreContext _context;

		public GenreLocaleRepository(GameStoreContext context)
		{
			_context = context;
		}

		public IEnumerable<GenreLocale> GetAllBy(int genreId)
		{
			return _context.GenreLocales.Where(l => l.Genre.Id == genreId);
		}
	}
}