using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Common
{
	public interface IOrderRepository
	{
		void Insert(Order order);

		IEnumerable<Order> Get(OrderFilter orderFilter = null);

		Order Get(string customerId);

		void Update(Order order);

		bool Contains(string customerId);
	}
}
