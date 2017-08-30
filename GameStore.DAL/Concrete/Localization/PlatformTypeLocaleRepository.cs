using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.Localization;
using GameStore.DAL.Context;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Localization
{
	public class PlatformTypeLocaleRepository : IPlatformTypeLocaleRepository
	{
		private readonly GameStoreContext _context;

		public PlatformTypeLocaleRepository(GameStoreContext context)
		{
			_context = context;
		}

		public IEnumerable<PlatformTypeLocale> GetAllBy(int platformTypeId)
		{
			return _context.PlatformTypeLocales.Where(l => l.PlatformType.Id == platformTypeId);
		}
	}
}