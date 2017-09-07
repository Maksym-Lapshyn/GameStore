using GameStore.Common.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfPlatformTypeRepository
	{
		PlatformType GetSingleOrDefault(Expression<Func<PlatformType, bool>> predicate);

		PlatformType GetSingle(Expression<Func<PlatformType, bool>> predicate);

		IQueryable<PlatformType> GetAll(Expression<Func<PlatformType, bool>> predicate = null);

		bool Contains(Expression<Func<PlatformType, bool>> predicate);

		void Insert(PlatformType platformType);
	}
}