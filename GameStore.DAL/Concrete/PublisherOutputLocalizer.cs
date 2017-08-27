using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Linq;

namespace GameStore.DAL.Concrete
{
    public class PublisherOutputLocalizer : IOutputLocalizer<Publisher>
    {
        private const string DefaultLanguage = "en";

        public Publisher Localize(string language, Publisher entity)
        {
            if (entity.PublisherLocales.Count != 0)
            {
                var publisherLocale = entity.PublisherLocales.FirstOrDefault(l => l.Language.Name == language);
                entity.Description = publisherLocale != null ? publisherLocale.Description : entity.PublisherLocales.First(l => l.Language.Name == DefaultLanguage).Description;
            }

            return entity;
        }
    }
}
