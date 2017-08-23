using GameStore.Common.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IMongoGameRepository
	{
		IQueryable<Game> GetAll(Expression<Func<Game, bool>> predicate = null);

		Game GetSingle(Expression<Func<Game, bool>> predicate);

		bool Contains(Expression<Func<Game, bool>> predicate);
	}
}