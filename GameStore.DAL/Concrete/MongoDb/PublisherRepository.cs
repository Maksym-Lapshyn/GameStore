using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class PublisherRepository : IPublisherRepository
	{
		private readonly IMongoCollection<Publisher> _collection;

		public PublisherRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<Publisher>("suppliers");
		}

		public IQueryable<Publisher> Get()
		{
			return _collection.AsQueryable();
		}

		public Publisher Get(string gameId)
		{
			var entity = _collection.AsQueryable().First(g => g.Id.ToString() == gameId);

			return entity;
		}
	}
}
