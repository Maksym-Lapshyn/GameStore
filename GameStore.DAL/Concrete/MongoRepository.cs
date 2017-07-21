using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly IMongoCollection<TEntity> _collection;

		public MongoRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
		}

		public IQueryable<TEntity> Get()
		{
			return _collection.AsQueryable();
		}

		public TEntity Get(int id)
		{
			var entity = _collection.Find(e => e.Id == id).First();

			return entity;
		}

		public void Insert(TEntity entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(TEntity entityToUpdate)
		{
			throw new NotImplementedException();
		}
	}
}
