using GameStore.Common.Entities.Localization;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Localization
{
	public interface IPublisherLocaleRepository
	{
		IEnumerable<PublisherLocale> GetAllBy(int publisherId);
	}
}