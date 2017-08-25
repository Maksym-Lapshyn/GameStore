using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class GenreLocalizer : ILocalizer<Genre>
	{
		private const string DefaultLanguage = "en";

		public Genre Localize(Genre entity, string language)
		{
			var genreLocale = entity.GenreLocales.FirstOrDefault(l => l.Language.Name == language);

			if (genreLocale != null)
			{
				entity.Name = genreLocale.Name;
				entity.ParentGenre.Name = genreLocale.ParentGenreName;
			}
			else
			{
				entity.Name = entity.GenreLocales.First(l => l.Language.Name == DefaultLanguage).Name;
				entity.ParentGenre.Name = entity.GenreLocales.First(l => l.Language.Name == DefaultLanguage).ParentGenreName;
			}
			
			return entity;
		}
	}
}