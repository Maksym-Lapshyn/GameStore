using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.Localization;
using GameStore.DAL.Context;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Localization
{
	public class PublisherLocaleRepository : IPublisherLocaleRepository
	{
		private readonly GameStoreContext _context;

		public PublisherLocaleRepository(GameStoreContext context)
		{
			_context = context;
		}

		public IEnumerable<PublisherLocale> GetAllBy(int publisherId)
		{
			return _context.PublisherLocales.Where(l => l.Publisher.Id == publisherId);
		}
	}
}