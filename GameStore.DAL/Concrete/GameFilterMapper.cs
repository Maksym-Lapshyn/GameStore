using GameStore.Common.Entities;
using GameStore.Common.Enums;
using GameStore.DAL.Abstract;
using GameStore.DAL.Concrete.Filters;
using GameStore.DAL.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class GameFilterMapper : IFilterMapper
	{
		private readonly List<IFilter<IQueryable<Game>>> _filters = new List<IFilter<IQueryable<Game>>>();

		public List<IFilter<IQueryable<Game>>> Map(GameFilter filter)
		{
			if (filter.GenresInput.Count != 0)
			{
				_filters.Add(new GenreFilter(filter.GenresInput));
			}

			if (filter.PlatformTypesInput.Count != 0)
			{
				_filters.Add(new PlatformTypeFilter(filter.PlatformTypesInput));
			}

			if (filter.PublishersInput.Count != 0)
			{
				_filters.Add(new PublisherFilter(filter.PublishersInput));
			}

			if (filter.MinPrice != default(decimal))
			{
				_filters.Add(new MinPriceFilter(filter.MinPrice));
			}

			if (filter.MaxPrice != default(decimal))
			{
				_filters.Add(new MaxPriceFilter(filter.MaxPrice));
			}

			if (filter.DateOptions != DateOptions.None)
			{
				_filters.Add(new DateOptionsFilter(filter.DateOptions));
			}

			if (filter.GameName != null)
			{
				_filters.Add(new GameNameFilter(filter.GameName));
			}

			_filters.Add(new SortOptionsFilter(filter.SortOptions));

			return _filters;
		}
	}
}
