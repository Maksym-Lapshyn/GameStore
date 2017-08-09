using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IMongoPublisherRepository
	{
		IQueryable<Publisher> GetAll();

		Publisher GetSingle(string companyName);
	}
}