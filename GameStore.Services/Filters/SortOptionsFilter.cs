using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.Enums;
using System.Linq;

namespace GameStore.Services.Filters
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
				case SortOptions.PriceAsc:
					input = input.OrderBy(g => g.Price);
					break;
				case SortOptions.PriceDesc:
					input = input.OrderByDescending(g => g.Price);
					break;
				case SortOptions.DateAdded:
					input = input.OrderBy(g => g.Price);
					break;
				case SortOptions.MostCommented:
					input = input.OrderByDescending(g => g.Comments.Count);
					break;
				case SortOptions.MostViewed:
					input = input.OrderByDescending(g => g.ViewsCount);
					break;
			}

			return input;
		}
	}
}
