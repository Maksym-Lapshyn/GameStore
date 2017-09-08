using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStore.DAL.Concrete.Common
{
	public class AsyncGameRepository : IAsyncGameRepository
	{
		private readonly IAsyncEfGameRepository _efRepository;
		private readonly IAsyncMongoGameRepository _mongoRepository;

		public AsyncGameRepository(IAsyncEfGameRepository efRepository,
			IAsyncMongoGameRepository mongoRepository)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
		}

		public async Task<Game> GetSingleOrDefaultAsync(Expression<Func<Game, bool>> predicate)
		{
			var efTask = _efRepository.GetSingleOrDefaultAsync(predicate);
			var mongoTask = _mongoRepository.GetSingleOrDefaultAsync(predicate);
			var result = await Task.WhenAll(efTask, mongoTask);

			return result[0] ?? result[1];
		}

		public async Task UpdateAsync(Game game)
		{
			await _efRepository.UpdateAsync(game);
		}
	}
}