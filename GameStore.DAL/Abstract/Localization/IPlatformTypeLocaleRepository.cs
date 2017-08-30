using GameStore.Common.Entities.Localization;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Localization
{
	public interface IPlatformTypeLocaleRepository
	{
		IEnumerable<PlatformTypeLocale> GetAllBy(int platformTypeId);
	}
}