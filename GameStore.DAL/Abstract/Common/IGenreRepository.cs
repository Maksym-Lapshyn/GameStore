using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Common
{
	public interface IGenreRepository
	{
		IEnumerable<Genre> Get();

		Genre Get(string name);

		bool Contains(string name);

		void Insert(Genre genre);
	}
}