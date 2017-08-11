using GameStore.Common.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IMongoGenreRepository
	{
		IQueryable<Genre> GetAll(Expression<Func<Genre, bool>> predicate = null);

		Genre GetSingle(Expression<Func<Genre, bool>> predicate);

		bool Contains(Expression<Func<Genre, bool>> predicate);
	}
}