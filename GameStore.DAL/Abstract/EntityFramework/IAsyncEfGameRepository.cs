using GameStore.Common.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IAsyncEfGameRepository
	{
		Task<Game> GetSingleOrDefaultAsync(Expression<Func<Game, bool>> predicate);

		Task UpdateAsync(Game game);
	}
}