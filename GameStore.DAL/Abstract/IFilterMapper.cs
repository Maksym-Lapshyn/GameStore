using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;

namespace GameStore.DAL.Abstract
{
	public interface IFilterMapper
	{
		List<IFilter<IQueryable<Game>>> Map(GameFilter filter);
	}
}
