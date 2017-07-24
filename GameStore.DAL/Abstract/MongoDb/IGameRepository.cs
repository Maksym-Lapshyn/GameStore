using System.Linq;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IGameRepository
	{
		IQueryable<Game> Get();

		Game Get(string gameId);
	}
}
