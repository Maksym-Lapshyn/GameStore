using GameStore.Common.Entities;
using GameStore.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.Common
{
	public interface IOrderRepository
	{
		void Insert(Order order);

		IEnumerable<Order> GetAll(OrderFilter orderFilter = null, Expression<Func<Order, bool>> predicate = null);

		Order GetSingle(Expression<Func<Order, bool>> predicate);

		Order GetSingleOrDefault(Expression<Func<Order, bool>> predicate);

		void Update(Order order);

		bool Contains(Expression<Func<Order, bool>> predicate);
	}
}