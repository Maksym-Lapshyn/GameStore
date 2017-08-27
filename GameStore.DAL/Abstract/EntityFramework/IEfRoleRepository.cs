using GameStore.Common.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfRoleRepository
	{
		Role GetSingle(Expression<Func<Role, bool>> predicate);

		IQueryable<Role> GetAll(Expression<Func<Role, bool>> predicate = null);

		void Update(Role role);

		void Create(Role role);

		void Delete(string name);

		bool Contains(Expression<Func<Role, bool>> predicate);
	}
}