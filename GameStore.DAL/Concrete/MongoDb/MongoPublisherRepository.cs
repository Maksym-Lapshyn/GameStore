using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class MongoPublisherRepository : IGenericRepository<Publisher>
	{
		private readonly IMongoCollection<Publisher> _collection;

		public MongoPublisherRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<Publisher>("suppliers");
		}

		public IQueryable<Publisher> Get()
		{
			return _collection.AsQueryable();
		}

		public Publisher Get(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Insert(Publisher entity)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Update(Publisher entityToUpdate)
		{
			throw new System.NotImplementedException();
		}
	}
}