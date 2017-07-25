using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class MongoGameRepository : IGenericRepository<Game>
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

		public Game Get(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Insert(Game entity)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Update(Game entityToUpdate)
		{
			throw new System.NotImplementedException();
		}
	}
}