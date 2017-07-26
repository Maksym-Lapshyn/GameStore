using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Concrete.Filters
{
	public class GenreFilter : IFilter<IQueryable<Game>>
	{
		private readonly IEnumerable<string> _genreNames;

		public GenreFilter(IEnumerable<string> genreNames)
		{
			_genreNames = genreNames;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			return input.Where(game => game.Genres.Select(genre => genre.Name).Intersect(_genreNames).Any());
		}
	}
}