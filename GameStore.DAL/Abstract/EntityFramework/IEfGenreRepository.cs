using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfGenreRepository
	{
		IQueryable<Genre> GetAll();

		Genre GetSingle(string name);

		bool Contains(string name);

		void Insert(Genre genre);

		void Update(Genre genre);

		void Delete(string name);
	}
}