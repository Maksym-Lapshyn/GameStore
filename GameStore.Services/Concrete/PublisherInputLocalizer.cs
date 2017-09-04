using GameStore.Common.Entities;
using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.Localization;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class PublisherInputLocalizer : IInputLocalizer<Publisher>
	{
		private readonly ILanguageRepository _languageRepository;

		public PublisherInputLocalizer(ILanguageRepository languageRepository)
		{
			_languageRepository = languageRepository;
		}

		public void Localize(string language, Publisher entity)
		{
			var publisherLocale = entity.PublisherLocales.FirstOrDefault(l => l.Language.Name == language);

			if (publisherLocale != null)
			{
				publisherLocale.Description = entity.Description;
			}
			else
			{
				entity.PublisherLocales.Add(new PublisherLocale
				{
					Description = entity.Description,
					Language = _languageRepository.GetSingleBy(language)
				});
			}
		}
	}
}