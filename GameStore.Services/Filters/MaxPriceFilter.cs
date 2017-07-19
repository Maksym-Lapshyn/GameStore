using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Filters
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
			input = input.Where(g => g.Price < _maxPrice); //TODO Consider: simplify to 'return input.Where....'

			return input;
		}
	}
}
