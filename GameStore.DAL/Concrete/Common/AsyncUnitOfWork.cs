using GameStore.DAL.Context;
using System.Threading.Tasks;

namespace GameStore.DAL.Concrete.Common
{
	public class AsyncUnitOfWork
	{
		private readonly GameStoreContext _context;

		public AsyncUnitOfWork(GameStoreContext context)
		{
			_context = context;
		}

		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}