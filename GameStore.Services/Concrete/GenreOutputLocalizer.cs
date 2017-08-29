using GameStore.Common.Entities;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class GenreOutputLocalizer : IOutputLocalizer<Genre>
	{
		private const string DefaultLanguage = "en";

		public Genre Localize(string language, Genre entity)
		{
			if (entity.GenreLocales.Count == 0)
			{
				return entity;
			}

			var genreLocale = entity.GenreLocales.FirstOrDefault(l => l.Language.Name == language);

			if (genreLocale != null)
			{
				entity.Name = genreLocale.Name;
			}
			else
			{
				genreLocale = entity.GenreLocales.First(l => l.Language.Name == DefaultLanguage);
				entity.Name = genreLocale.Name;
			}

			if (entity.ParentGenre == null)
			{
				return entity;
			}

			var parentGenreLocale = entity.GenreLocales.FirstOrDefault(l => l.Language.Name == language) ?? entity.GenreLocales.First(l => l.Language.Name == DefaultLanguage);
			entity.ParentGenre.Name = parentGenreLocale.Name;

			return entity;
		}
	}
}