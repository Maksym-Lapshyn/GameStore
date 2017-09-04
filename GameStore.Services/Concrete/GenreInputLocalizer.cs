using GameStore.Common.Entities;
using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.Localization;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class GenreInputLocalizer : IInputLocalizer<Genre>
	{
		private readonly ILanguageRepository _languageRepository;

		public GenreInputLocalizer(ILanguageRepository languageRepository)
		{
			_languageRepository = languageRepository;
		}

		public void Localize(string language, Genre entity)
		{
			var genreLocale = entity.GenreLocales.FirstOrDefault(l => l.Language.Name == language);

			if (genreLocale != null)
			{
				genreLocale.Name = entity.Name;
			}
			else
			{
				genreLocale = new GenreLocale
				{
					Name = entity.Name,
					Language = _languageRepository.GetSingleBy(language)
				};

				entity.GenreLocales.Add(genreLocale);
			}
		}
	}
}