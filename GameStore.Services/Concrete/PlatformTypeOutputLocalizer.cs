using GameStore.Common.Entities;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class PlatformTypeOutputLocalizer : IOutputLocalizer<PlatformType>
	{
		public PlatformType Localize(string language, PlatformType entity)
		{
			if (entity.PlatformTypeLocales.Count == 0)
			{
				return entity;
			}

			var platformTypeLocale = entity.PlatformTypeLocales.FirstOrDefault(l => l.Language.Name == language) ?? entity.PlatformTypeLocales.First();
			entity.Type = platformTypeLocale.Type;

			return entity;
		}
	}
}