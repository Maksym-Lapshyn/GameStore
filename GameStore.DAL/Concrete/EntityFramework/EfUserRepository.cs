using System;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DAL.Infrastructure.Extensions;

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
            user = MergeRoles(user);
            MergePlainProperties(user);
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

        private User MergeRoles(User user)
        {
            var existingUser = _context.Users.First(u => u.Id == user.Id);
            var deletedRoles = existingUser.Roles.Except(user.Roles, r => r.Id).ToList();
            var addedRoles = user.Roles.Except(existingUser.Roles, r => r.Id).ToList();
            deletedRoles.ForEach(r => existingUser.Roles.Remove(r));

            foreach (var r in addedRoles)
            {
                if (_context.Entry(r).State == EntityState.Detached)
                {
                    _context.Roles.Attach(r);
                }

                existingUser.Roles.Add(r);
            }

            return user;
        }

        private void MergePlainProperties(User user)
        {
            var existingUser = _context.Users.First(g => g.Id == user.Id);
            _context.Entry(existingUser).CurrentValues.SetValues(user);
            _context.Entry(existingUser).State = EntityState.Modified;
        }
    }
}