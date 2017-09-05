using System;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Common.Entities;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfOrderRepository
	{
		void Insert(Order order);

		IQueryable<Order> GetAll(Expression<Func<Order, bool>> predicate = null);

		Order GetSingleOrDefault(Expression<Func<Order, bool>> predicate);

		Order GetSingle(Expression<Func<Order, bool>> predicate);

		void Update(Order order);

		bool Contains(Expression<Func<Order, bool>> predicate);
	}
}