using System.Data.Entity;
using System.Linq;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfRoleRepository : IEfRoleRepository
	{
		private readonly GameStoreContext _context;

		public EfRoleRepository(GameStoreContext context)
		{
			_context = context;
		}

		public Role GetSingle(string name)
		{
			return _context.Roles.First(r => r.Name == name);
		}

		public IQueryable<Role> GetAll()
		{
			return _context.Roles.AsQueryable();
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
	}
}