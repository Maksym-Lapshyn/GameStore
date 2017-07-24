using GameStore.DAL.Abstract;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Data.Entity;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class GameDecorator : IGenericRepository<Game>
	{
		private DbSet<Game> _dbSet;
		private IMongoCollection<Game> _collection;

		public GameDecorator(GameStoreContext context, IMongoDatabase database)
		{
			_dbSet = context.Games;
			_collection = database.GetCollection<Game>("products");
		}

		public IQueryable<Game> Get()
		{
			throw new System.NotImplementedException();
		}

		public Game Get(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Insert(Game entity)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Update(Game entityToUpdate)
		{
			throw new System.NotImplementedException();
		}
	}
}
