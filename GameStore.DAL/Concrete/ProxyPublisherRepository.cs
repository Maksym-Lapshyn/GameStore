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
			var efList = _efRepository.Get().ToList();
			var mongoList = _mongoRepository.Get().ToList();
			mongoList.RemoveAll(mongoPublisher => efList.Any(efPublisher => efPublisher.CompanyName == mongoPublisher.CompanyName));//Removes duplicates

			return efList.Union(mongoList).AsQueryable();
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