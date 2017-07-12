using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Filters
{
	public class PlatformTypeFilter : IFilter<IQueryable<Game>>
	{
		private readonly IEnumerable<int> _platformTypeIds;

		public PlatformTypeFilter(IEnumerable<int> platformTypeIds)
		{
		    _platformTypeIds = platformTypeIds;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			input = input.Where(g => g.PlatformTypes.Select(p => p.Id).Intersect(_platformTypeIds).Any());

			return input;
		}
	}
}
