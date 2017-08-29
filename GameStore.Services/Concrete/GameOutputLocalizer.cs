using GameStore.Common.Entities;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class GameOutputLocalizer : IOutputLocalizer<Game>
	{
		private const string DefaultLanguage = "en";

		private readonly IOutputLocalizer<Genre> _genreLocalizer;
		private readonly IOutputLocalizer<PlatformType> _platformTypeLocalizer;
		private readonly IOutputLocalizer<Publisher> _publisherLocalizer;

		public GameOutputLocalizer(IOutputLocalizer<Genre> genreLocalizer,
			IOutputLocalizer<PlatformType> platformTypeLocalizer,
			IOutputLocalizer<Publisher> publisherLocalizer)
		{
			_genreLocalizer = genreLocalizer;
			_platformTypeLocalizer = platformTypeLocalizer;
			_publisherLocalizer = publisherLocalizer;
		}

		public Game Localize(string language, Game entity)
		{
			if (entity.GameLocales.Count == 0)
			{
				return entity;
			}

			var gameLocale = entity.GameLocales.FirstOrDefault(l => l.Language.Name == language);

			if (gameLocale != null)
			{
				entity.Description = gameLocale.Description;

				for (var i = 0; i < entity.Genres.Count; i++)
				{
					entity.Genres.ToList()[i] = _genreLocalizer.Localize(language, entity.Genres.ToList()[i]);
				}

				for (var i = 0; i < entity.PlatformTypes.Count; i++)
				{
					entity.PlatformTypes.ToList()[i] = _platformTypeLocalizer.Localize(language, entity.PlatformTypes.ToList()[i]);
				}

				entity.Publisher = _publisherLocalizer.Localize(language, entity.Publisher);
			}
			else
			{
				entity.Description = entity.GameLocales.First(l => l.Language.Name == DefaultLanguage).Description;

				for (var i = 0; i < entity.Genres.Count; i++)
				{
					entity.Genres.ToList()[i] = _genreLocalizer.Localize(DefaultLanguage, entity.Genres.ToList()[i]);
				}

				for (var i = 0; i < entity.PlatformTypes.Count; i++)
				{
					entity.PlatformTypes.ToList()[i] = _platformTypeLocalizer.Localize(DefaultLanguage, entity.PlatformTypes.ToList()[i]);
				}
			}

			return entity;
		}
	}
}