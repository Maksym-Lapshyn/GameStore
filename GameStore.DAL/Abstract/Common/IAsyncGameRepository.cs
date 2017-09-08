using GameStore.Common.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStore.DAL.Abstract.Common
{
	public interface IAsyncGameRepository
	{
		Task<Game> GetSingleOrDefaultAsync(Expression<Func<Game, bool>> predicate);

		Task UpdateAsync(Game game);
	}
}