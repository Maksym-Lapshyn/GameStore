using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IMongoPublisherRepository
	{
		IQueryable<Publisher> Get();

		Publisher Get(string id);
	}
}
