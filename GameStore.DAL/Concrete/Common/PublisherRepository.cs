using System.Collections.Generic;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Concrete.Common
{
	public class PublisherRepository : IPublisherRepository
	{
		private readonly IEfPublisherRepository _efRepository;
		private readonly IMongoPublisherRepository _mongoRepository;

		public PublisherRepository(IEfPublisherRepository efRepository, IMongoPublisherRepository mongoRepository)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
		}

		public IEnumerable<Publisher> Get()
		{
			var efList = _efRepository.Get().ToList();
			var northwindIds = efList.Select(p => p.NorthwindId);
			var mongoList = _mongoRepository.Get().Where(g => !northwindIds.Contains(g.NorthwindId));

			return efList.Union(mongoList);
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

		public void Update(Publisher publisher)
		{
			_efRepository.Update(publisher);
		}
	}
}