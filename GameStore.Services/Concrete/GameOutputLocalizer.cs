using GameStore.Common.Entities;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class GameOutputLocalizer : IOutputLocalizer<Game>
	{
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

			var gameLocale = entity.GameLocales.FirstOrDefault(l => l.Language.Name == language) ?? entity.GameLocales.First();
			entity.Description = gameLocale.Description;

			if (entity.Genres.Count != 0)
			{
				foreach (var genre in entity.Genres)
				{
					_genreLocalizer.Localize(language, genre);
				}
				/*for (var i = 0; i < entity.Genres.Count; i++)
				{
					entity.Genres.ToList()[i] = _genreLocalizer.Localize(language, entity.Genres.ToList()[i]);
				}*/
			}

			if (entity.PlatformTypes.Count != 0)
			{
				foreach (var platformType in entity.PlatformTypes)
				{
					_platformTypeLocalizer.Localize(language, platformType);
				}
				/*for (var i = 0; i < entity.PlatformTypes.Count; i++)
				{
					entity.PlatformTypes.ToList()[i] = _platformTypeLocalizer.Localize(language, entity.PlatformTypes.ToList()[i]);
				}*/
			}

			if (entity.Publisher != null)
			{
				entity.Publisher = _publisherLocalizer.Localize(language, entity.Publisher);
			}

			return entity;
		}
	}
}