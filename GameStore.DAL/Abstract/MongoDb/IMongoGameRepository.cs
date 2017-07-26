using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IMongoGameRepository
	{
		IQueryable<Game> Get();

		Game Get(string gameKey);
	}
}