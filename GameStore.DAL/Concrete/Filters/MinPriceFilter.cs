using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Linq;

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