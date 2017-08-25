using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class GameLocalizer : ILocalizer<Game>
	{
		private const string DefaultLanguage = "en";

		private readonly ILocalizer<Genre> _genreLocalizer;
		private readonly ILocalizer<PlatformType> _platformTypeLocalizer;

		public GameLocalizer(ILocalizer<Genre> genreLocalizer, ILocalizer<PlatformType> platformTypeLocalizer)
		{
			_genreLocalizer = genreLocalizer;
			_platformTypeLocalizer = platformTypeLocalizer;
		}

		public Game Localize(Game entity, string language)
		{
			var gameLocale = entity.GameLocales.FirstOrDefault(l => l.Language.Name == language);

			if (gameLocale != null)
			{
				entity.Description = gameLocale.Description;

				for (var i = 0; i < entity.Genres.Count; i++)
				{
					entity.Genres.ToList()[i] = _genreLocalizer.Localize(entity.Genres.ToList()[i], language);
				}

				for (var i = 0; i < entity.PlatformTypes.Count; i++)
				{
					entity.PlatformTypes.ToList()[i] = _platformTypeLocalizer.Localize(entity.PlatformTypes.ToList()[i], language);
				}
			}
			else
			{
				entity.Description = entity.GameLocales.First(l => l.Language.Name == DefaultLanguage).Description;

				for (var i = 0; i < entity.Genres.Count; i++)
				{
					entity.Genres.ToList()[i] = _genreLocalizer.Localize(entity.Genres.ToList()[i], DefaultLanguage);
				}

				for (var i = 0; i < entity.PlatformTypes.Count; i++)
				{
					entity.PlatformTypes.ToList()[i] = _platformTypeLocalizer.Localize(entity.PlatformTypes.ToList()[i], DefaultLanguage);
				}
			}

			return entity;
		}
	}
}