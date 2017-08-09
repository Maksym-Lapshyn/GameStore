using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IMongoOrderRepository
	{
		IQueryable<Order> GetAll();

		Order GetSingle(string customerId);

		bool Contains(string customerId);
	}
}