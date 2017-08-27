using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.Common
{
	public class PlatformTypeRepository : IPlatformTypeRepository
	{
		private readonly IEfPlatformTypeRepository _efRepository;
        private readonly IOutputLocalizer<PlatformType> _localizer;

		public PlatformTypeRepository(IEfPlatformTypeRepository efRepository,
            IOutputLocalizer<PlatformType> localizer)
		{
			_efRepository = efRepository;
            _localizer = localizer;
		}

		public PlatformType GetSingle(string language, Expression<Func<PlatformType, bool>> predicate)
		{
			var platformType = _efRepository.GetSingle(predicate);

            return _localizer.Localize(language, platformType);
		}

		public IEnumerable<PlatformType> GetAll(string language, Expression<Func<PlatformType, bool>> predicate = null)
		{
            var totalList = _efRepository.GetAll(predicate).ToList();

            for (var i = 0; i < totalList.Count; i++)
            {
                totalList[i] = _localizer.Localize(language, totalList[i]);
            }

            return totalList;
        }

		public bool Contains(Expression<Func<PlatformType, bool>> predicate)
		{
			return _efRepository.Contains(predicate);
		}

		public void Insert(PlatformType platformType)
		{
			_efRepository.Insert(platformType);
		}
	}
}