using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class MongoGenreRepository : IMongoGenreRepository
	{
		private readonly IMongoCollection<Genre> _collection;

		public MongoGenreRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<Genre>("categories");
		}

		public IQueryable<Genre> GetAll()
		{
			return _collection.AsQueryable();
		}

		public Genre GetSingle(string name)
		{
			return _collection.Find(g => g.Name == name).Single();
		}
	}
}