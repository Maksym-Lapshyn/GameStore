using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Common.Entities;

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

		public IEnumerable<Order> GetAll(OrderFilter orderFilter = null, Expression<Func<Order, bool>> predicate = null)
		{
			var efQuery = _efRepository.GetAll(predicate);
			var mongoQuery = _mongoRepository.GetAll(predicate);

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

		public Order GetSingle(Expression<Func<Order, bool>> predicate)
		{
			return _efRepository.Contains(predicate) ? _efRepository.GetSingle(predicate) : _mongoRepository.GetSingle(predicate);
		}

		public void Update(Order order)
		{
			_efRepository.Update(order);
		}

		public bool Contains(Expression<Func<Order, bool>> predicate)
		{
			return _efRepository.Contains(predicate) || _mongoRepository.Contains(predicate);
		}

		private IQueryable<Order> Filter(IQueryable<Order> orders, OrderFilter orderFilter)
		{
			return orders.Where(o => o.OrderedDate > orderFilter.From).Where(o => o.OrderedDate < orderFilter.To);
		}
	}
}