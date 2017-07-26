using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IMongoShipperRepository
	{
		IQueryable<Shipper> Get();

		Shipper Get(string id);
	}
}