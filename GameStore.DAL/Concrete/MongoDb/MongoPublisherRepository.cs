using GameStore.Common.Entities;
using GameStore.DAL.Abstract.MongoDb;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class MongoPublisherRepository : IMongoPublisherRepository
	{
		private readonly IMongoCollection<Publisher> _collection;

		public MongoPublisherRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<Publisher>("suppliers");
		}

		public IQueryable<Publisher> GetAll(Expression<Func<Publisher, bool>> predicate = null)
		{
			return predicate != null 
				? _collection.AsQueryable().Where(predicate.Compile()).AsQueryable()
				: _collection.AsQueryable();
		}

		public Publisher GetSingle(Expression<Func<Publisher, bool>> predicate)
		{
			return _collection.AsQueryable().Where(predicate.Compile()).First();
		}

		public bool Contains(Expression<Func<Publisher, bool>> predicate)
		{
			return _collection.AsQueryable().Any(predicate.Compile());
		}

		/*private Game GetEmbeddedDocuments(Publisher publisher)
		{
			game.Publisher = _publisherCollection.AsQueryable().First(p => p.SupplierId == game.SupplierId);
			game.Genres = _genreCollection.AsQueryable().Where(g => g.CategoryId == game.CategoryId).ToList();

			return game;
		}*/
	}
}