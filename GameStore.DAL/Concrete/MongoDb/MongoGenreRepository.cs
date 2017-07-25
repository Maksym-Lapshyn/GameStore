using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class MongoGenreRepository : IGenericRepository<Genre>
	{
		private readonly IMongoCollection<Genre> _collection;

		public MongoGenreRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<Genre>("categories");
		}

		public IQueryable<Genre> Get()
		{
			return _collection.AsQueryable();
		}

		public Genre Get(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Insert(Genre entity)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Update(Genre entityToUpdate)
		{
			throw new System.NotImplementedException();
		}
	}
}