using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GameStore.DAL.Infrastructure.Extensions;

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

		public IQueryable<Game> Get(GameFilter filter = null)
		{
			return _context.Games;
		}

		public Game Get(string gameKey)
		{
			return _context.Games.First(g => g.Key == gameKey);
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
            var oldGame = _context.Games.First(g => g.Key == game.Key);
			var container = CreateContainer("Update", game, oldGame);
			_logger.LogChange(container);
            game = MergeGenres(game);
            game = MergePlatformTypes(game);
            game = MergePublisher(game);
            game = MergePlainProperties(game);
            _context.SaveChanges();
        }

		public bool Contains(string gameKey)
		{
			return _context.Games.Any(g => g.Key == gameKey);
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
			//_context.Entry(game).State = EntityState.Detached;
			var existingGame = _context.Games.First(g => g.Key == game.Key);
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
            var existingGame = _context.Games.First(g => g.Key == game.Key);
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
            var existingGame = _context.Games.First(g => g.Key == game.Key);
            if (_context.Entry(game.Publisher).State == EntityState.Detached)
            {
                _context.Publishers.Attach(game.Publisher);
            }

            existingGame.Publisher = game.Publisher;

            return game;
        }

        private Game MergePlainProperties(Game game)
        {
            var existingGame = _context.Games.First(g => g.Key == game.Key);
            _context.Entry(existingGame).CurrentValues.SetValues(game);
            _context.Entry(existingGame).State = EntityState.Modified;

            return game;
        }
	}
}