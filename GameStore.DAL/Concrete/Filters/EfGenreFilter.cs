using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Filters
{
	public class EfGenreFilter : IFilter<IQueryable<Game>>
	{
		private readonly IEnumerable<string> _genreNames;

		public EfGenreFilter(IEnumerable<string> genreNames)
		{
			_genreNames = genreNames;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			return input.Where(game => game.Genres.Select(genre => genre.GenreLocales.SelectMany(l => l.Name)).Intersect(_genreNames).Any());
		}
	}
}