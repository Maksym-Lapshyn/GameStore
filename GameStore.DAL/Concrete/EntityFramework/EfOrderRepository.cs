using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;

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

		public IQueryable<Order> Get(OrderFilter orderFilter = null)
		{
			return _context.Orders;
		}

		public Order Get(string customerId)
		{
			return _context.Orders.First(o => o.CustomerId == customerId);
		}

		public void Update(Order order)
		{
			_context.Orders.AddOrUpdate(order);
		}

		public bool Contains(string customerId)
		{
			return _context.Orders.Any(o => o.CustomerId == customerId);
		}
	}
}