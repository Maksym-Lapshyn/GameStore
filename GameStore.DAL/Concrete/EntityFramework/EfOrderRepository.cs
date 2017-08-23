using GameStore.Common.Entities;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfOrderRepository : IEfOrderRepository
	{
		private readonly GameStoreContext _context;

		public EfOrderRepository(GameStoreContext context)
		{
			_context = context;
		}

		public void Insert(Order order)
		{
			_context.Orders.Add(order);
		}

		public IQueryable<Order> GetAll(Expression<Func<Order, bool>> predicate = null)
		{
			return predicate != null ? _context.Orders.Where(predicate) : _context.Orders;
		}

		public Order GetSingle(Expression<Func<Order, bool>> predicate)
		{
			return _context.Orders.First(predicate);
		}

		public void Update(Order order)
		{
			_context.Entry(order).State = EntityState.Modified;
		}

		public bool Contains(Expression<Func<Order, bool>> predicate)
		{
			return _context.Orders.Any(predicate);
		}
	}
}