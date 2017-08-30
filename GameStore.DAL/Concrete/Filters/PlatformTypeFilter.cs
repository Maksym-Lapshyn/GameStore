using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Filters
{
	public class PlatformTypeFilter : IFilter<IQueryable<Game>>
	{
		private readonly IEnumerable<string> _platformTypeTypes;

		public PlatformTypeFilter(IEnumerable<string> platformTypeTypes)
		{
			_platformTypeTypes = platformTypeTypes;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			return input.Where(game => game.PlatformTypes.Any(platformType => platformType.PlatformTypeLocales.Any(l => _platformTypeTypes.Contains(l.Type)) 
				|| _platformTypeTypes.Contains(platformType.Type)));
		}
	}
}