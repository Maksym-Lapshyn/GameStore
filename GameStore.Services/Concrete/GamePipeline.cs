using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class GamePipeline : IPipeline<IQueryable<Game>>
	{
		private readonly List<IFilter<IQueryable<Game>>> _filters = new List<IFilter<IQueryable<Game>>>();

		public IPipeline<IQueryable<Game>> Register(IFilter<IQueryable<Game>> filter)
		{
			_filters.Add(filter);

			return this;
		}

		public IQueryable<Game> Process(IQueryable<Game> input)
		{
			_filters.ForEach(f => input = f.Execute(input));

			return input;
		}
	}
}