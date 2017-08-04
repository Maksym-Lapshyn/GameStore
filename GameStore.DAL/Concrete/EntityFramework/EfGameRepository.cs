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
			game = MergeGenres(game);
			var oldGame = (Game)_context.Entry(game).OriginalValues.ToObject();
			var container = CreateContainer("Update", game, oldGame);
			_logger.LogChange(container);
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

			_context.SaveChanges();

			return existingGame;
		}
	}
}