using System.Linq;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Concrete.Filters
{
	public class MaxPriceFilter : IFilter<IQueryable<Game>>
	{
		private readonly decimal _maxPrice;

		public MaxPriceFilter(decimal maxPrice)
		{
			_maxPrice = maxPrice;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			return input.Where(g => g.Price < _maxPrice);
		}
	}
}