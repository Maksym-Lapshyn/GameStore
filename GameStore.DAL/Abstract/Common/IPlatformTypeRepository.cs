using GameStore.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.Common
{
	public interface IPlatformTypeRepository
	{
		PlatformType GetSingle(string language, Expression<Func<PlatformType, bool>> predicate);

		IEnumerable<PlatformType> GetAll(string language, Expression<Func<PlatformType, bool>> predicate = null);

		bool Contains(Expression<Func<PlatformType, bool>> predicate);

		void Insert(PlatformType platformType);
	}
}
