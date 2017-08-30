using GameStore.Common.Entities.Localization;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Localization
{
	public interface IRoleLocaleRepository
	{
		IEnumerable<RoleLocale> GetAllBy(int roleId);
	}
}