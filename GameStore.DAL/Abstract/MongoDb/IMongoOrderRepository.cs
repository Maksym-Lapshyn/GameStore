using System;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Common.Entities;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IMongoOrderRepository
	{
		IQueryable<Order> GetAll(Expression<Func<Order, bool>> predicate = null);

		Order GetSingle(Expression<Func<Order, bool>> predicate);

		bool Contains(Expression<Func<Order, bool>> predicate);
	}
}