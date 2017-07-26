using GameStore.DAL.Abstract;
using GameStore.DAL.Context;

namespace GameStore.DAL.Concrete
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly GameStoreContext _context;

		public UnitOfWork(GameStoreContext context)
		{
			_context = context;
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}