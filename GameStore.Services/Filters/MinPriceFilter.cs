using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Filters
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
			return input.Where(g => g.Price > _minPrice); //TODO Consider: simplify to 'return input.Where....'
		}
	}
}