using GameStore.Common.Entities;
using GameStore.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.Common
{
	public interface IGameRepository
	{
		IEnumerable<Game> GetAll(GameFilter filter = null, int? itemsToSkip = null, int? itemsToTake = null, Expression<Func<Game, bool>> predicate = null);

		Game GetSingle(Expression<Func<Game, bool>> predicate);

		void Insert(Game game);

		void Delete(string gameKey);

		void Update(Game game);

		bool Contains(Expression<Func<Game, bool>> predicate);
	}
}