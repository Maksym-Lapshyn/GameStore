using GameStore.Common.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfPlatformTypeRepository
	{
		PlatformType GetSingle(string language, Expression<Func<PlatformType, bool>> predicate);

		IQueryable<PlatformType> GetAll(string language, Expression<Func<PlatformType, bool>> predicate = null);

		bool Contains(Expression<Func<PlatformType, bool>> predicate);

		void Insert(PlatformType platformType);
	}
}