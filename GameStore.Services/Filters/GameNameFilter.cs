using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Filters
{
	public class GameNameFilter : IFilter<IQueryable<Game>>
	{
		private readonly string _gameName;

		public GameNameFilter(string gameName)
		{
			_gameName = gameName;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			return input.Where(g => g.Name.Contains(_gameName));
		}
	}
}