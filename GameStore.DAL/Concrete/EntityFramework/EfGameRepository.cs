using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfGameRepository : IEfGameRepository
	{
		private readonly GameStoreContext _context;

		public EfGameRepository(GameStoreContext context)
		{
			_context = context;
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
		}

		public void Delete(string gameKey)
		{
			_context.Games.First(g => g.Key == gameKey).IsDeleted = true;
		}

		public void Update(Game game)
		{
			if (_context.Games.Any(g => g.Key == game.Key))
			{
				_context.Entry(game).State = EntityState.Modified;
			}
			else
			{
				_context.Games.Add(game);
			}
		}

		public bool Contains(string gameKey)
		{
			return _context.Games.Any(g => g.Key == gameKey);
		}
	}
}