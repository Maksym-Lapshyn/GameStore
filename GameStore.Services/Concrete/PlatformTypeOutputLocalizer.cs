using GameStore.Common.Entities;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class PlatformTypeOutputLocalizer : IOutputLocalizer<PlatformType>
	{
		public void Localize(string language, PlatformType entity)
		{
			if (entity.PlatformTypeLocales.Count == 0)
			{
				return;
			}

			var platformTypeLocale = entity.PlatformTypeLocales.FirstOrDefault(l => l.Language.Name == language) ?? entity.PlatformTypeLocales.First();
			entity.Type = platformTypeLocale.Type;
		}
	}
}