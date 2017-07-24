using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IShipperRepository
	{
		IQueryable<Shipper> Get();

		Shipper Get(string gameId);
	}
}
