using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class MongoShipperRepository : IGenericRepository<Shipper>
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

		public Shipper Get(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Insert(Shipper entity)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Update(Shipper entityToUpdate)
		{
			throw new System.NotImplementedException();
		}
	}
}