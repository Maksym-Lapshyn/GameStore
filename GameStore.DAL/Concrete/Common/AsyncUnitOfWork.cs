using GameStore.DAL.Context;
using GameStore.DAL.Abstract.Common;
using System.Threading.Tasks;

namespace GameStore.DAL.Concrete.Common
{
	public class AsyncUnitOfWork : IAsyncUnitOfWork
	{
		private readonly GameStoreContext _context;

		public AsyncUnitOfWork(GameStoreContext context)
		{
			_context = context;
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}