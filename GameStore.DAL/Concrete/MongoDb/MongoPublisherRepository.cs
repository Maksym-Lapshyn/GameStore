using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class MongoPublisherRepository : IMongoPublisherRepository
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

		public Publisher Get(string companyName)
		{
			return _collection.Find(p => p.CompanyName == companyName).Single();
		}
	}
}