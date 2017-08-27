using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class GenreOutputLocalizer : IOutputLocalizer<Genre>
	{
		private const string DefaultLanguage = "en";

		public Genre Localize(string language, Genre entity)
		{
            if (entity.GenreLocales.Count != 0)
            {
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

                if (entity.ParentGenre != null)
                {
                    var parentGenreLocale = entity.GenreLocales.FirstOrDefault(l => l.Language.Name == language);

                    if (parentGenreLocale != null)
                    {
                        entity.ParentGenre.Name = parentGenreLocale.Name;
                    }
                    else
                    {
                        parentGenreLocale = entity.GenreLocales.First(l => l.Language.Name == DefaultLanguage);
                        entity.Name = genreLocale.Name;
                    }
                }
            }

            return entity;
        }
	}
}