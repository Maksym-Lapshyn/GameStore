using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.Common
{
	public class PlatformTypeRepository : IPlatformTypeRepository
	{
		private readonly IEfPlatformTypeRepository _efRepository;

		public PlatformTypeRepository(IEfPlatformTypeRepository efRepository)
		{
			_efRepository = efRepository;
		}

		public PlatformType GetSingle(Expression<Func<PlatformType, bool>> predicate)
		{
			return _efRepository.GetSingle(predicate);
		}

		public IEnumerable<PlatformType> GetAll(Expression<Func<PlatformType, bool>> predicate = null)
		{
			return _efRepository.GetAll(predicate).ToList();
		}

		public bool Contains(Expression<Func<PlatformType, bool>> predicate)
		{
			return _efRepository.Contains(predicate);
		}

		public void Insert(PlatformType platformType)
		{
			_efRepository.Insert(platformType);
		}

		public PlatformType GetSingleOrDefault(Expression<Func<PlatformType, bool>> predicate)
		{
			return _efRepository.GetSingleOrDefault(predicate);
		}
	}
}