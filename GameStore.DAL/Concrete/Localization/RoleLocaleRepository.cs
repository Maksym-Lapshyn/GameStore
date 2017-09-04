using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.Localization;
using GameStore.DAL.Context;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Localization
{
	public class RoleLocaleRepository : IRoleLocaleRepository
	{
		private readonly GameStoreContext _context;

		public RoleLocaleRepository(GameStoreContext context)
		{
			_context = context;
		}

		public IEnumerable<RoleLocale> GetAllBy(int roleId)
		{
			return _context.RoleLocales.Where(l => l.Role.Id == roleId);
		}
	}
}