using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class MongoGameRepository : IMongoGameRepository
	{
		private readonly IMongoCollection<Game> _collection;

		public MongoGameRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<Game>("products");
		}

		public IQueryable<Game> Get()
		{
			return _collection.AsQueryable();
		}

		public Game Get(string id)
		{
			throw new System.NotImplementedException();
		}

		public int Count()
		{
			return _collection.AsQueryable().Count();
		}
	}
}