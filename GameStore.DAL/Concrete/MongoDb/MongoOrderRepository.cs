using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

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

		public IQueryable<Order> Get()
		{
			var orders = _orderCollection.AsQueryable().ToList();

			for (var i = 0; i < orders.Count; i++)
			{
				orders[i] = GetEmbeddedDocuments(orders[i]);
			}

			return orders.AsQueryable();
		}

		public Order Get(string customerId)
		{
			var order = _orderCollection.AsQueryable().First(o => o.CustomerId == customerId);
			order = GetEmbeddedDocuments(order);

			return order;
		}

		private Order GetEmbeddedDocuments(Order order)
		{
			order.OrderDetails = _orderDetailsCollection.AsQueryable().Where(o => o.OrderId == order.OrderId).ToList();

			return order;
		}
	}
}