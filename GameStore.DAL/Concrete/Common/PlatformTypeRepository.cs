using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using System;
using System.Collections.Generic;
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

		public PlatformType GetSingle(Expression<Func<PlatformType, bool>> predicate, string language)
		{
			return _efRepository.GetSingle(predicate, language);
		}

		public IEnumerable<PlatformType> GetAll(string language, Expression<Func<PlatformType, bool>> predicate = null)
		{
			return _efRepository.GetAll(predicate, language);
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