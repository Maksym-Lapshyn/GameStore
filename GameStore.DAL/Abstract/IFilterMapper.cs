using GameStore.Common.Entities;
using GameStore.DAL.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Abstract
{
	public interface IFilterMapper
	{
		List<IFilter<IQueryable<Game>>> Map(GameFilter filter);
	}
}
