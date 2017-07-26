using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class PublisherProxy : IEfPublisherRepository
	{
		private readonly IEfPublisherRepository _efRepository;
		private readonly IMongoPublisherRepository _mongoRepository;

		public PublisherProxy(IEfPublisherRepository efRepository, IMongoPublisherRepository mongoRepository)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
		}

		public IQueryable<Publisher> Get()
		{
			var efQuery = _efRepository.Get();
			var mongoQuery = _mongoRepository.Get();

			return efQuery.Union(mongoQuery);
		}

		public Publisher Get(string companyName)
		{
			return _efRepository.Contains(companyName) ? _efRepository.Get(companyName) : _mongoRepository.Get(companyName);
		}

		public bool Contains(string companyName)
		{
			return _efRepository.Contains(companyName);
		}

		public void Insert(Publisher publisher)
		{
			_efRepository.Insert(publisher);
		}
	}
}