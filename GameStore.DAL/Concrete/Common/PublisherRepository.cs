using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Infrastructure.Comparers;

namespace GameStore.DAL.Concrete.Common
{
	public class PublisherRepository : IPublisherRepository
	{
		private readonly IEfPublisherRepository _efRepository;
		private readonly IMongoPublisherRepository _mongoRepository;
		private readonly ICopier<Publisher> _copier;

		public PublisherRepository(IEfPublisherRepository efRepository,
			IMongoPublisherRepository mongoRepository,
			ICopier<Publisher> copier)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
			_copier = copier;
		}

		public IEnumerable<Publisher> GetAll()
		{
			var efList = _efRepository.GetAll().ToList();
			var mongoList = _mongoRepository.GetAll().ToList();

			return efList.Union(mongoList, new PublisherEqualityComparer());
		}

		public Publisher GetSingle(string companyName)
		{
			return !_efRepository.Contains(companyName) ? _copier.Copy(_mongoRepository.GetSingle(companyName)) : _efRepository.GetSingle(companyName);
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