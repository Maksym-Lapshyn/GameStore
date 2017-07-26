using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;
using GameStore.DAL.Abstract.MongoDb;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class MongoShipperRepository : IMongoShipperRepository
	{
		private readonly IMongoCollection<Shipper> _collection;

		public MongoShipperRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<Shipper>("shippers");
		}

		public IQueryable<Shipper> Get()
		{
			return _collection.AsQueryable();
		}

		public Shipper Get(string id)
		{
			throw new System.NotImplementedException();
		}
	}
}