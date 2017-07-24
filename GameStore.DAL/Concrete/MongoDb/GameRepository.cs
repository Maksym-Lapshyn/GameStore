using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class GameRepository : IGameRepository
	{
		private readonly IMongoCollection<Game> _collection;

		public GameRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<Game>("products");
		}

		public IQueryable<Game> Get()
		{
			return _collection.AsQueryable();
		}

		public Game Get(string gameId)
		{
			var entity = _collection.AsQueryable().First(g => g.Id.ToString() == gameId);

			return entity;
		}
	}
}
