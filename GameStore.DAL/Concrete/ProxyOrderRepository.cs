using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class ProxyOrderRepository : IEfOrderRepository
	{
		private readonly IEfOrderRepository _efRepository;
		private readonly IMongoOrderRepository _mongoRepository;

		public ProxyOrderRepository(IEfOrderRepository efRepository, IMongoOrderRepository mongoRepository)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
		}

		public void Insert(Order order)
		{
			_efRepository.Insert(order);
		}

		public IQueryable<Order> Get(OrderFilter orderFilter = null)
		{
			var efQuery = _efRepository.Get();
			var mongoQuery = _mongoRepository.Get();

			if (orderFilter != null)
			{
				efQuery = Filter(efQuery, orderFilter);
				mongoQuery = Filter(mongoQuery, orderFilter);
			}

			return efQuery.ToList().Union(mongoQuery.ToList()).AsQueryable();
		}

		public Order Get(string customerId)
		{
			return _efRepository.Contains(customerId) ? _efRepository.Get(customerId) : _mongoRepository.Get(customerId);
		}

		public void Update(Order order)
		{
			_efRepository.Update(order);
		}

		public bool Contains(string customerId)
		{
			return _efRepository.Contains(customerId);
		}

		private IQueryable<Order> Filter(IQueryable<Order> orders, OrderFilter orderFilter)
		{
			return orders.Where(o => o.OrderDate > orderFilter.From).Where(o => o.OrderDate < orderFilter.To);
		}
	}
}
