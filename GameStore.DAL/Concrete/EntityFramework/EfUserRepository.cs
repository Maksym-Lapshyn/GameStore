using System;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfUserRepository : IEfUserRepository
	{
		private readonly GameStoreContext _context;

		public EfUserRepository(GameStoreContext context)
		{
			_context = context;
		}

		public User GetSingle(Expression<Func<User, bool>> predicate)
		{
			return _context.Users.First(predicate);
		}

		public bool Contains(Expression<Func<User, bool>> predicate)
		{
			return _context.Users.Any(predicate);
		}

		public IQueryable<User> GetAll(Expression<Func<User, bool>> predicate = null)
		{
			return predicate != null ? _context.Users.Where(predicate) : _context.Users;
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
			var user = _context.Users.First(u => u.Login == name);
			user.IsDeleted = true;
			_context.Entry(user).State = EntityState.Modified;
		}
	}
}