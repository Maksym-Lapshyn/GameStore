using GameStore.Common.Entities;
using GameStore.Common.Infrastructure.Extensions;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfGameRepository : IEfGameRepository
	{
		private readonly GameStoreContext _context;
		private readonly ILogger<Game> _logger;

		public EfGameRepository(GameStoreContext context,
			ILogger<Game> logger)
		{
			_context = context;
			_logger = logger;
		}

		public IQueryable<Game> GetAll(Expression<Func<Game, bool>> predicate = null)
		{
			return predicate != null ? _context.Games.Where(predicate) : _context.Games;
		}

		public Game GetSingle(Expression<Func<Game, bool>> predicate)
		{
			return _context.Games.First(predicate);
		}

		public void Insert(Game game)
		{
			_context.Games.Add(game);

			var container = CreateContainer("Insert", game);

			_logger.LogChange(container);
		}

		public void Delete(string gameKey)
		{
			var game = _context.Games.First(g => g.Key == gameKey);
			game.IsDeleted = true;

			_context.Entry(game).State = EntityState.Modified;

			var container = CreateContainer("Delete", game);

			_logger.LogChange(container);
		}

		public void Update(Game game)
		{
			Game oldGame;

			if (_context.Entry(game).State == EntityState.Modified)//populates old game entity for logger
			{
				oldGame = (Game) _context.Entry(game).OriginalValues.ToObject();
				oldGame.Publisher = game.Publisher;
				oldGame.GameLocales = game.GameLocales;
				oldGame.Genres = game.Genres;
				oldGame.PlatformTypes = game.PlatformTypes;
			}
			else
			{
				oldGame = _context.Games.First(g => g.Id == game.Id);
			}

			var container = CreateContainer("Update", game, oldGame);

			_logger.LogChange(container);

			game = MergeGenres(game);
			game = MergePlatformTypes(game);
			game = MergePublisher(game);

			MergePlainProperties(game);
		}

		public bool Contains(Expression<Func<Game, bool>> predicate)
		{
			return _context.Games.Any(predicate);
		}

		public Game GetSingleOrDefault(Expression<Func<Game, bool>> predicate)
		{
			return _context.Games.FirstOrDefault(predicate);
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

		private Game MergeGenres(Game game)
		{
			var existingGame = _context.Games.First(g => g.Id == game.Id);
			var deletedGenres = existingGame.Genres.Except(game.Genres, g => g.Id).ToList();
			var addedGenres = game.Genres.Except(existingGame.Genres, g => g.Id).ToList();

			deletedGenres.ForEach(g => existingGame.Genres.Remove(g));

			foreach (var g in addedGenres)
			{
				if (_context.Entry(g).State == EntityState.Detached)
				{
					_context.Genres.Attach(g);
				}

				existingGame.Genres.Add(g);
			}

			return game;
		}

		private Game MergePlatformTypes(Game game)
		{
			var existingGame = _context.Games.First(g => g.Id == game.Id);
			var deletedPlatformTypes = existingGame.PlatformTypes.Except(game.PlatformTypes, p => p.Id).ToList();
			var addedPlatformTypes = game.PlatformTypes.Except(existingGame.PlatformTypes, p => p.Id).ToList();

			deletedPlatformTypes.ForEach(p => existingGame.PlatformTypes.Remove(p));

			foreach (var p in addedPlatformTypes)
			{
				if (_context.Entry(p).State == EntityState.Detached)
				{
					_context.PlatformTypes.Attach(p);
				}

				existingGame.PlatformTypes.Add(p);
			}

			return game;
		}

		private Game MergePublisher(Game game)
		{
			var existingGame = _context.Games.First(g => g.Id == game.Id);

			if (_context.Entry(game.Publisher).State == EntityState.Detached)
			{
				_context.Publishers.Attach(game.Publisher);
			}

			existingGame.Publisher = game.Publisher;

			return game;
		}

		private void MergePlainProperties(Game game)
		{
			var existingGame = _context.Games.First(g => g.Id == game.Id);

			_context.Entry(existingGame).CurrentValues.SetValues(game);
			_context.Entry(existingGame).State = EntityState.Modified;
		}
	}
}