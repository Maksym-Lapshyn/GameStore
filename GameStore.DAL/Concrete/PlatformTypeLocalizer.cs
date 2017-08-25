using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class PlatformTypeLocalizer : ILocalizer<PlatformType>
	{
		private const string DefaultLanguage = "en";

		public PlatformType Localize(PlatformType entity, string language)
		{
			var platformTypeLocale = entity.PlatformTypeLocales.FirstOrDefault(l => l.Language.Name == language);
			entity.Type = platformTypeLocale != null ? platformTypeLocale.Type : entity.PlatformTypeLocales.First(l => l.Language.Name == DefaultLanguage).Type;

			return entity;
		}
	}
}