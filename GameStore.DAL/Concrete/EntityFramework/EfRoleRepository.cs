using GameStore.Common.Entities;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfRoleRepository : IEfRoleRepository
	{
		private readonly GameStoreContext _context;

		public EfRoleRepository(GameStoreContext context)
		{
			_context = context;
		}

		public Role GetSingle(Expression<Func<Role, bool>> predicate)
		{
			return _context.Roles.First(predicate);
		}

		public IQueryable<Role> GetAll(Expression<Func<Role, bool>> predicate = null)
		{
			return predicate != null ? _context.Roles.Where(predicate) : _context.Roles;
		}

		public void Update(Role role)
		{
			_context.Entry(role).State = EntityState.Modified;
		}

		public void Create(Role role)
		{
			_context.Roles.Add(role);
		}

		public void Delete(string name)
		{
			var role = _context.Roles.First(r => r.Name == name);
			role.IsDeleted = true;
			_context.Entry(role).State = EntityState.Modified;
		}

		public bool Contains(Expression<Func<Role, bool>> predicate)
		{
			return _context.Roles.Any(predicate);
		}
	}
}