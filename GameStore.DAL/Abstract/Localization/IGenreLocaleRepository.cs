using GameStore.Common.Entities.Localization;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Localization
{
	public interface IGenreLocaleRepository
	{
		IEnumerable<GenreLocale> GetAllBy(int genreId);
	}
}