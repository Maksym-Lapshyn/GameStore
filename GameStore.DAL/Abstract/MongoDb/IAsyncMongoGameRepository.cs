using GameStore.Common.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IAsyncMongoGameRepository
	{
		Task<Game> GetSingleOrDefaultAsync(Expression<Func<Game, bool>> predicate);
	}
}