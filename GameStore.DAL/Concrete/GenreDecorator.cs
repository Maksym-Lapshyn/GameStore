using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Concrete.EntityFramework;
using GameStore.DAL.Concrete.MongoDb;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class GenreDecorator : IGenericRepository<Genre>
	{
		private readonly EfGenericRepository<Genre> _efRepository;
		private readonly MongoGenreRepository _mongoRepository;

		public GenreDecorator(GameStoreContext context, IMongoDatabase database)
		{
			_efRepository = new EfGenericRepository<Genre>(context);
			_mongoRepository = new MongoGenreRepository(database);
		}

		public IQueryable<Genre> Get()
		{
			var efQuery = _efRepository.Get();
			var mongoQuery = _mongoRepository.Get();

			return efQuery.Union(mongoQuery);
		}

		public Genre Get(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Insert(Genre entity)
		{
			_efRepository.Insert(entity);
		}

		public void Delete(int id)
		{
			_efRepository.Delete(id);
		}

		public void Update(Genre entityToUpdate)
		{
			_efRepository.Update(entityToUpdate);
		}

		public int Count()
		{
			return _efRepository.Count();
		}
	}
}