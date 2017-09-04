using GameStore.Common.Entities;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class PublisherOutputLocalizer : IOutputLocalizer<Publisher>
	{
		public void Localize(string language, Publisher entity)
		{
			if (entity.PublisherLocales.Count == 0)
			{
				return;
			}

			var publisherLocale = entity.PublisherLocales.FirstOrDefault(l => l.Language.Name == language) ?? entity.PublisherLocales.First();
			entity.Description = publisherLocale.Description;
		}
	}
}