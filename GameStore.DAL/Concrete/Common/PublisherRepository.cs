using System.Collections.Generic;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using System.Linq;
using GameStore.DAL.Abstract;

namespace GameStore.DAL.Concrete.Common
{
	public class PublisherRepository : IPublisherRepository
	{
		private readonly IEfPublisherRepository _efRepository;
		private readonly IMongoPublisherRepository _mongoRepository;
		private readonly ICloner<Publisher> _cloner;

		public PublisherRepository(IEfPublisherRepository efRepository,
			IMongoPublisherRepository mongoRepository,
			ICloner<Publisher> cloner)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
			_cloner = cloner;
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
			return !_efRepository.Contains(companyName) ? _cloner.Clone(_mongoRepository.Get(companyName)) : _efRepository.Get(companyName);
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