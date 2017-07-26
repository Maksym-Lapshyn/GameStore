using GameStore.DAL.Abstract;
using GameStore.DAL.Concrete.EntityFramework;
using GameStore.DAL.Concrete.MongoDb;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;
using GameStore.DAL.Abstract.EntityFramework;

namespace GameStore.DAL.Concrete
{
	public class GameDecorator : IGenericRepository<Game>
	{
		private readonly EfGenericRepository<Game> _efRepository;
		private readonly MongoGameRepository _mongoRepository;

		public GameDecorator(GameStoreContext context, IMongoDatabase database)
		{
			_efRepository = new EfGenericRepository<Game>(context);
			_mongoRepository = new MongoGameRepository(database);
		}

		public IQueryable<Game> Get()
		{
			var efQuery = _efRepository.Get();
			var mongoQuery = _mongoRepository.Get();

			return efQuery.Union(mongoQuery);
		}

		public Game Get(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Insert(Game entity)
		{
			_efRepository.Insert(entity);
		}

		public void Delete(int id)
		{
			_efRepository.Delete(id);
		}

		public void Update(Game entityToUpdate)
		{
			_efRepository.Update(entityToUpdate);
		}

		public int Count()
		{
			return _efRepository.Count() + _mongoRepository.Count();
		}
	}
}