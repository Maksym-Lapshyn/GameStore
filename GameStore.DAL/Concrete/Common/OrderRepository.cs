using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Common
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IEfOrderRepository _efRepository;
		private readonly IMongoOrderRepository _mongoRepository;

		public OrderRepository(IEfOrderRepository efRepository, IMongoOrderRepository mongoRepository)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
		}

		public void Insert(Order order)
		{
			_efRepository.Insert(order);
		}

		public IEnumerable<Order> GetAll(OrderFilter orderFilter = null)
		{
			var efQuery = _efRepository.GetAll();
			var mongoQuery = _mongoRepository.GetAll();

			if (orderFilter != null)
			{
				efQuery = Filter(efQuery, orderFilter);
				mongoQuery = Filter(mongoQuery, orderFilter);
			}

			var efList = efQuery.ToList();
			var northwindIds = efList.Select(p => p.NorthwindId);
			var mongoList = mongoQuery.Where(g => !northwindIds.Contains(g.NorthwindId));

			return efList.Union(mongoList);
		}

		public Order GetSingle(string customerId)
		{
			return _efRepository.Contains(customerId) ? _efRepository.GetSingle(customerId) : _mongoRepository.GetSingle(customerId);
		}

		public void Update(Order order)
		{
			_efRepository.Update(order);
		}

		public bool Contains(string customerId)
		{
			return _efRepository.Contains(customerId) || _mongoRepository.Contains(customerId);
		}

		private IQueryable<Order> Filter(IQueryable<Order> orders, OrderFilter orderFilter)
		{
			return orders.Where(o => o.OrderDate > orderFilter.From).Where(o => o.OrderDate < orderFilter.To);
		}
	}
}