using GameStore.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.Common
{
	public interface IGenreRepository
	{
		IEnumerable<Genre> GetAll(string language, Expression<Func<Genre, bool>> predicate = null);

		Genre GetSingle(string language, Expression<Func<Genre, bool>> predicate);

		bool Contains(Expression<Func<Genre, bool>> predicate);

		void Insert(Genre genre);

		void Update(Genre genre);

		void Delete(string name);
	}
}