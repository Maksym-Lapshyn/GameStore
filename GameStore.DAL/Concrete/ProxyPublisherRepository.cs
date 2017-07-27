using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class ProxyPublisherRepository : IEfPublisherRepository
	{
		private readonly IEfPublisherRepository _efRepository;
		private readonly IMongoPublisherRepository _mongoRepository;

		public ProxyPublisherRepository(IEfPublisherRepository efRepository, IMongoPublisherRepository mongoRepository)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
		}

		public IQueryable<Publisher> Get()
		{
			var efQuery = _efRepository.Get().ToList();
			var mongoQuery = _mongoRepository.Get().ToList();

			return efQuery.Union(mongoQuery).AsQueryable();
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