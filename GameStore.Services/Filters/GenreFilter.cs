using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Filters
{
	public class GenreFilter : IFilter<IQueryable<Game>>
	{
		private readonly IEnumerable<int> _genreIds;

		public GenreFilter(IEnumerable<int> genreIds)
		{
			_genreIds = genreIds;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			input = input.Where(game => game.Genres.Select(genre => genre.Id).Intersect(_genreIds).Any()); //TODO Consider: simplify to 'return input.Where....'

			return input;
		}
	}
}