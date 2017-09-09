using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class AsyncEfGameRepository : IAsyncEfGameRepository
	{
		private readonly GameStoreContext _context;
		private readonly IAsyncLogger<Game> _logger;

		public AsyncEfGameRepository(GameStoreContext context,
			IAsyncLogger<Game> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<Game> GetSingleOrDefaultAsync(Expression<Func<Game, bool>> predicate)
		{
			return await _context.Games.Include(g => g.Image).SingleOrDefaultAsync(predicate);
		}

		public async Task UpdateAsync(Game game)
		{
			Game oldGame;

			if (_context.Entry(game).State == EntityState.Modified)//populates old game entity for logger
			{
				oldGame = (Game)_context.Entry(game).OriginalValues.ToObject();
				oldGame.Publisher = game.Publisher;
				oldGame.GameLocales = game.GameLocales;
				oldGame.Genres = game.Genres;
				oldGame.PlatformTypes = game.PlatformTypes;
                oldGame.Image = game.Image;
			}
			else
			{
				oldGame = await _context.Games.FirstAsync(g => g.Id == game.Id);
			}

			var container = CreateContainer("Update", game, oldGame);

			await _logger.LogChangeAsync(container);
		}

		private GameLogContainer CreateContainer(string action, Game newGame, Game oldGame = null)
		{
			var container = new GameLogContainer
			{
				DateChanged = DateTime.UtcNow,
				Action = action,
				EntityType = newGame.GetType().ToString(),
				New = newGame,
				Old = oldGame
			};

			return container;
		}
	}
}