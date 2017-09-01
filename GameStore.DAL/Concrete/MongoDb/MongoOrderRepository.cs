using GameStore.Common.Entities;
using GameStore.DAL.Abstract.MongoDb;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class MongoOrderRepository : IMongoOrderRepository
	{
		private readonly IMongoCollection<Order> _orderCollection;
		private readonly IMongoCollection<OrderDetails> _orderDetailsCollection;

		public MongoOrderRepository(IMongoDatabase database)
		{
			_orderCollection = database.GetCollection<Order>("orders");
			_orderDetailsCollection = database.GetCollection<OrderDetails>("order-details");
		}

		public IQueryable<Order> GetAll(Expression<Func<Order, bool>> predicate = null)
		{
			var orders = predicate != null
				? _orderCollection.AsQueryable().Where(predicate.Compile()).ToList()
				: _orderCollection.AsQueryable().ToList();

			for (var i = 0; i < orders.Count; i++)
			{
				orders[i] = GetEmbeddedDocuments(orders[i]);
			}

			return orders.AsQueryable();
		}

		public Order GetSingle(Expression<Func<Order, bool>> predicate)
		{
			var order = _orderCollection.AsQueryable().Where(predicate.Compile()).First();
			order = GetEmbeddedDocuments(order);

			return order;
		}

		private Order GetEmbeddedDocuments(Order order)
		{
			order.OrderDetails = _orderDetailsCollection.AsQueryable().Where(o => o.OrderId == order.OrderId).ToList();

			return order;
		}

		public bool Contains(Expression<Func<Order, bool>> predicate)
		{
			return _orderCollection.AsQueryable().Any(predicate.Compile());
		}
	}
}