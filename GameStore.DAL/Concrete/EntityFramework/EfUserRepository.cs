using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using System.Linq;
using GameStore.Common.Entities;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfUserRepository : IEfUserRepository
	{
		private readonly GameStoreContext _context;

		public EfUserRepository(GameStoreContext context)
		{
			_context = context;
		}

		public User GetSingle(string userName, string password = null)
		{
			return password != null ? _context.Users.First(u => u.Name.ToLower() == userName.ToLower()) : _context.Users.First(u => u.Name == userName);
		}

		public bool Contains(string userName, string password)
		{
			return _context.Users.Any(u => u.Name.ToLower() == userName.ToLower());
		}

		public IQueryable<User> GetAll()
		{
			return _context.Users.AsQueryable();
		}
	}
}