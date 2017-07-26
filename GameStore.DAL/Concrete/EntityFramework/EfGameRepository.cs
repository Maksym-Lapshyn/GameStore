using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using System;
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
			throw new NotImplementedException();
		}

		public Game Get(string gameKey)
		{
			throw new NotImplementedException();
		}

		public void Insert(Game game)
		{
			throw new NotImplementedException();
		}

		public void Delete(string gameKey)
		{
			throw new NotImplementedException();
		}

		public void Update(Game game)
		{
			throw new NotImplementedException();
		}

		public bool Contains(string gameKey)
		{
			throw new NotImplementedException();
		}
	}
}