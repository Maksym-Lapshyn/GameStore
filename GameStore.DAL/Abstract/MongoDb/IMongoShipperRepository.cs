using GameStore.Common.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IMongoShipperRepository
	{
		IQueryable<Shipper> GetAll();

		Shipper GetSingle(string id);
	}
}