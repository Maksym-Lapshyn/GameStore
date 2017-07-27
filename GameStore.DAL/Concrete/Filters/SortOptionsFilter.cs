using GameStore.Common.Enums;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Concrete.Filters
{
	public class SortOptionsFilter : IFilter<IQueryable<Game>>
	{
		private readonly SortOptions _sortOption;

		public SortOptionsFilter(SortOptions sortOption)
		{
			_sortOption = sortOption;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			switch (_sortOption)
			{
				case SortOptions.PriceAscending:
					input = input.OrderBy(g => g.Price);
					break;
				case SortOptions.PriceDescending:
					input = input.OrderByDescending(g => g.Price);
					break;
				case SortOptions.DateAdded:
					input = input.OrderBy(g => g.DateAdded);
					break;
				case SortOptions.MostCommented:
					input = input.OrderByDescending(g => g.Comments.Count);
					break;
				case SortOptions.MostViewed:
					input = input.OrderByDescending(g => g.ViewsCount);
					break;
				case SortOptions.None:
					input = input.OrderBy(g => g.DateAdded);
					break;
			}

			return input;
		}
	}
}