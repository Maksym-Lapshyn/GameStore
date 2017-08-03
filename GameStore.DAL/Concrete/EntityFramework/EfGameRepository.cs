using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using System;
using System.Data.Entity;
using System.Linq;

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
			PopulateEf(game);
			//game = PopulateEntity(game);
			_context.Games.Add(game);
			var container = CreateContainer("Insert", game);
			_logger.LogChange(container);
		}

		public void Delete(string gameKey)
		{
			var game = _context.Games.First(g => g.Key == gameKey);
			game.IsDeleted = true;
			_context.Entry(game).State = EntityState.Detached;
			var container = CreateContainer("Delete", game);

			_logger.LogChange(container);
			_context.Entry(game).State = EntityState.Modified;
		}

		public void Update(Game game)
		{
			var oldGame = (Game)_context.Entry(game).OriginalValues.ToObject();
			var container = CreateContainer("Update", game, oldGame);
			_logger.LogChange(container);
			PopulateEf(game);
			//game = PopulateEntity(game);
			//var oldGame = _context.Games.AsNoTracking().First(g => g.Key == game.Key);
			_context.Entry(game).State = EntityState.Modified;
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

		private void PopulateEf(Game game)
		{
			var genreList = game.Genres.Where(genre => genre.Id == default(int)).ToList();
			_context.Genres.AddRange(genreList);

			if (game.Publisher.Id == default(int))
			{
				_context.Publishers.Add(game.Publisher);
			}

			_context.SaveChanges();
		}

		private Game PopulateEntity(Game game)
		{
			var publisherKey = game.Publisher.CompanyName;
			var genreKeys = game.Genres.Select(g => g.Name).ToList();
			var platfromTypeKeys = game.PlatformTypes.Select(p => p.Type).ToList();
			game.Publisher = null;
			game.Genres.Clear();
			game.PlatformTypes.Clear();
			_context.Entry(game).State = EntityState.Modified;
			_context.SaveChanges();
			game.Publisher = _context.Publishers.First(p => p.CompanyName == publisherKey);
			genreKeys.ForEach(k => game.Genres.Add(_context.Genres.First(g => g.Name == k)));
			platfromTypeKeys.ForEach(k => game.PlatformTypes.Add(_context.PlatformTypes.First(p => p.Type == k)));

			return game;
		}
	}
}