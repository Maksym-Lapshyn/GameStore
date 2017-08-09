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

		public IQueryable<Publisher> GetAll()
		{
			return _collection.AsQueryable();
		}

		public Publisher GetSingle(string companyName)
		{
			return _collection.AsQueryable().First(p => p.CompanyName.ToLower() == companyName.ToLower());
		}
	}
}