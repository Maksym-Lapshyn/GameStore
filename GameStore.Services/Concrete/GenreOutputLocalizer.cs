using GameStore.Common.Entities;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class GenreOutputLocalizer : IOutputLocalizer<Genre>
	{
		public void Localize(string language, Genre entity)
		{
			if (entity.ParentGenre != null && entity.ParentGenre.GenreLocales.Count != 0)
			{
				var parentGenreLocale = entity.ParentGenre.GenreLocales.FirstOrDefault(l => l.Language.Name == language) ?? entity.ParentGenre.GenreLocales.First();
				entity.ParentGenre.Name = parentGenreLocale.Name;
			}

			if (entity.GenreLocales.Count == 0)
			{
				return;

			}

			var genreLocale = entity.GenreLocales.FirstOrDefault(l => l.Language.Name == language) ?? entity.GenreLocales.First();
			entity.Name = genreLocale.Name;
		}
	}
}