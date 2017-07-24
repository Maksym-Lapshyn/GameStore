using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class ShipperRepository : IShipperRepository
	{
		private readonly IMongoCollection<Shipper> _collection;

		public ShipperRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<Shipper>("shippers");
		}

		public IQueryable<Shipper> Get()
		{
			return _collection.AsQueryable();
		}

		public Shipper Get(string gameId)
		{
			var entity = _collection.AsQueryable().First(g => g.Id.ToString() == gameId);

			return entity;
		}
	}
}
