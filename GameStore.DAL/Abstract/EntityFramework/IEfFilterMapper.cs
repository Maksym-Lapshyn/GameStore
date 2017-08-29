using GameStore.Common.Entities;
using GameStore.DAL.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfFilterMapper
	{
		List<IFilter<IQueryable<Game>>> Map(GameFilter filter);
	}
}