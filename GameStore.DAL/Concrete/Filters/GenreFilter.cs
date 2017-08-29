using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Collections.Generic;
using System.Linq;

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
			return input.Where(game => game.Genres.Any(genre => genre.GenreLocales.Any(l => _genreNames.Contains(l.Name)) 
				|| _genreNames.Contains(genre.Name)));
		}
	}
}