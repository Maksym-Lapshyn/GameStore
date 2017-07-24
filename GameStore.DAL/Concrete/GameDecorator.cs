using System.Linq;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Concrete
{
	public class GameDecorator : IGenericRepository<Game>
	{
		private IGenericRepository<Game> _efGameRepository;
		private IGameRepository _mongoGameRepository;

		public GameDecorator(IGenericRepository<> )
		{
			
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
