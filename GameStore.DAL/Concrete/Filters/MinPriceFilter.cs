using System.Linq;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Concrete.Filters
{
	public class MinPriceFilter : IFilter<IQueryable<Game>>
	{
		private readonly decimal _minPrice;

		public MinPriceFilter(decimal minPrice)
		{
			_minPrice = minPrice;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			return input.Where(g => g.Price > _minPrice);
		}
	}
}