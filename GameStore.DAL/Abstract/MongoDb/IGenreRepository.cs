using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IGenreRepository
	{
		IQueryable<Genre> Get();

		Genre Get(string gameId);
	}
}
