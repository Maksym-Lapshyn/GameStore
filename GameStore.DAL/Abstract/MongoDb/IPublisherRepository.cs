using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IPublisherRepository
	{
		IQueryable<Publisher> Get();

		Publisher Get(string gameId);
	}
}
