using GameStore.DAL.Abstract;
using GameStore.DAL.Concrete.EntityFramework;
using GameStore.DAL.Concrete.MongoDb;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class PublisherDecorator : IGenericRepository<Publisher>
	{
		private readonly EfGenericRepository<Publisher> _efRepository;
		private readonly MongoPublisherRepository _mongoRepository;

		public PublisherDecorator(GameStoreContext context, IMongoDatabase database)
		{
			_efRepository = new EfGenericRepository<Publisher>(context);
			_mongoRepository = new MongoPublisherRepository(database);
		}

		public IQueryable<Publisher> Get()
		{
			var efQuery = _efRepository.Get();
			var mongoQuery = _mongoRepository.Get();

			return efQuery.Union(mongoQuery);
		}

		public Publisher Get(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Insert(Publisher entity)
		{
			_efRepository.Insert(entity);
		}

		public void Delete(int id)
		{
			_efRepository.Delete(id);
		}

		public void Update(Publisher entityToUpdate)
		{
			_efRepository.Update(entityToUpdate);
		}
	}
}
