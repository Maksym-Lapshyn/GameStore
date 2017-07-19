using GameStore.DAL.Entities;
using GameStore.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Abstract
{
	public interface IFilterMapper
	{
		List<IFilter<IQueryable<Game>>> Map(FilterDto filter);
	}
}
