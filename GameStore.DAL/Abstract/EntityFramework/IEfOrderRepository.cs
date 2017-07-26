using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfOrderRepository
	{
		void Insert(Order order);

		IQueryable<Order> Get();

		Order Get(string customerId);

		void Update(Order order);

		bool Contains(string customerId);
	}
}