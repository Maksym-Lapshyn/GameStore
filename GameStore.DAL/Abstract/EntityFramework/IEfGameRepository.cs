using GameStore.Common.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfGameRepository
	{
		IQueryable<Game> GetAll(Expression<Func<Game, bool>> predicate = null);

		Game GetSingleOrDefault(Expression<Func<Game, bool>> predicate);

		Game GetSingle(Expression<Func<Game, bool>> predicate);

		void Insert(Game game);

		void Delete(string gameKey);

		void Update(Game game);

		bool Contains(Expression<Func<Game, bool>> predicate);
	}
}