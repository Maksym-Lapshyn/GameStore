using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class PlatformTypeOutputLocalizer : IOutputLocalizer<PlatformType>
	{
		private const string DefaultLanguage = "en";

		public PlatformType Localize(string language, PlatformType entity)
		{
            if (entity.PlatformTypeLocales.Count != 0)
            {
                var platformTypeLocale = entity.PlatformTypeLocales.FirstOrDefault(l => l.Language.Name == language);
                entity.Type = platformTypeLocale != null ? platformTypeLocale.Type : entity.PlatformTypeLocales.First(l => l.Language.Name == DefaultLanguage).Type;
            }

			return entity;
		}
	}
}