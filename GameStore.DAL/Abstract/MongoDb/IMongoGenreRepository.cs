using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IMongoGenreRepository
	{
		IQueryable<Genre> Get();

		Genre Get(string id);
	}
}