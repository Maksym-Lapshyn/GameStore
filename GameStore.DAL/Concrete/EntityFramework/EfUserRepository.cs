using System.Data.Entity;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using System.Linq;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfUserRepository : IEfUserRepository
	{
		private readonly GameStoreContext _context;

		public EfUserRepository(GameStoreContext context)
		{
			_context = context;
		}

		public User GetSingle(string name, string password = null)
		{
			return password != null ? _context.Users.First(u => u.Name.ToLower() == name.ToLower()) : _context.Users.First(u => u.Name == name);
		}

		public bool Contains(string name, string password)
		{
			return _context.Users.Any(u => u.Name.ToLower() == name.ToLower());
		}

		public IQueryable<User> GetAll()
		{
			return _context.Users.AsQueryable();
		}

		public void Update(User user)
		{
			_context.Entry(user).State = EntityState.Modified;
		}

		public void Create(User user)
		{
			_context.Users.Add(user);
		}

		public void Delete(string name)
		{
			var user = _context.Users.First(u => u.Name == name);
			user.IsDeleted = true;
			_context.Entry(user).State = EntityState.Modified;
		}
	}
}