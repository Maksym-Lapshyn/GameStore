using GameStore.Common.Entities;
using GameStore.Common.Infrastructure.Comparers;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.Common
{
	public class PublisherRepository : IPublisherRepository
	{
		private readonly IEfPublisherRepository _efRepository;
		private readonly IMongoPublisherRepository _mongoRepository;
		private readonly ICopier<Publisher> _copier;
        private readonly IOutputLocalizer<Publisher> _localizer;

		public PublisherRepository(IEfPublisherRepository efRepository,
			IMongoPublisherRepository mongoRepository,
			ICopier<Publisher> copier,
            IOutputLocalizer<Publisher> localizer)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
			_copier = copier;
            _localizer = localizer;
		}

		public IEnumerable<Publisher> GetAll(string language, Expression<Func<Publisher, bool>> predicate = null)
		{
			var efList = _efRepository.GetAll(predicate).ToList();
			var mongoList = _mongoRepository.GetAll(predicate).ToList();
            var totalList = efList.Union(mongoList, new PublisherEqualityComparer()).ToList();

            for (var i = 0; i < totalList.Count; i++)
            {
                totalList[i] = _localizer.Localize(language, totalList[i]);
            }

            return totalList;
		}

		public Publisher GetSingle(string language, Expression<Func<Publisher, bool>> predicate)
		{
			var publisher = !_efRepository.Contains(predicate) ? _copier.Copy(_mongoRepository.GetSingle(predicate)) : _efRepository.GetSingle(predicate);

            return _localizer.Localize(language, publisher);
		}

		public bool Contains(Expression<Func<Publisher, bool>> predicate)
		{
			return _efRepository.Contains(predicate);
		}

		public void Insert(Publisher publisher)
		{
			_efRepository.Insert(publisher);
		}

		public void Update(Publisher publisher)
		{
			_efRepository.Update(publisher);
		}

		public void Delete(string companyName)
		{
			_efRepository.Delete(companyName);
		}
	}
}