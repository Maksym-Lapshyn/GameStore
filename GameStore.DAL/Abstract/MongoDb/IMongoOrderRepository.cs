using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IMongoOrderRepository
	{
		IQueryable<Order> Get();

		Order Get(string customerId);
	}
}