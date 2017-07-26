using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfGenreRepository
	{
		IQueryable<Genre> Get();

		Genre Get(string name);

		bool Contains(string name);
	}
}