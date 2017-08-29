using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Filters
{
	public class MongoGenreFilter : IFilter<IQueryable<Game>>
	{
		private readonly IEnumerable<string> _genreNames;

		public MongoGenreFilter(IEnumerable<string> genreNames)
		{
			_genreNames = genreNames;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			return input.Where(game => game.Genres.Select(genre => genre.Name).Intersect(_genreNames).Any());
		}
	}
}