using GameStore.Common.Entities;
using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class GameInputLocalizer : IInputLocalizer<Game>
	{
		private readonly ILanguageRepository _languageRepository;

		public GameInputLocalizer(ILanguageRepository languageRepository)
		{
			_languageRepository = languageRepository;
		}

		public Game Localize(string language, Game entity)
		{
			var gameLocale = entity.GameLocales.FirstOrDefault(l => l.Language.Name == language);

			if (gameLocale != null)
			{
				gameLocale.Description = entity.Description;
			}
			else
			{
				entity.GameLocales.Add(new GameLocale
				{
					Description = entity.Description,
					Language = _languageRepository.GetSingleBy(language)
				});
			}

			return entity;
		}
	}
}