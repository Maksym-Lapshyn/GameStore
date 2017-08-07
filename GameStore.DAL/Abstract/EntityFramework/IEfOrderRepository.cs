using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using System.Linq;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfOrderRepository
	{
		void Insert(Order order);

		IQueryable<Order> GetAll(OrderFilter orderFilter = null);

		Order GetSingle(string customerId);

		void Update(Order order);

		bool Contains(string customerId);
	}
}