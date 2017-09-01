using GameStore.Common.Entities;
using GameStore.DAL.Abstract.MongoDb;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class MongoGenreRepository : IMongoGenreRepository
	{
		private readonly IMongoCollection<Genre> _collection;

		public MongoGenreRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<Genre>("categories");
		}

		public IQueryable<Genre> GetAll(Expression<Func<Genre, bool>> predicate = null)
		{
			return predicate != null ? _collection.AsQueryable().Where(predicate.Compile()).AsQueryable() : _collection.AsQueryable();
		}

		public Genre GetSingle(Expression<Func<Genre, bool>> predicate)
		{
			return _collection.AsQueryable().Where(predicate.Compile()).First();
		}

		public bool Contains(Expression<Func<Genre, bool>> predicate)
		{
			return _collection.AsQueryable().Any(predicate);
		}
	}
}