using GameStore.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.Common
{
	public interface IRoleRepository
	{
		Role GetSingleOrDefault(Expression<Func<Role, bool>> predicate);

		Role GetSingle(Expression<Func<Role, bool>> predicate);

		IEnumerable<Role> GetAll(Expression<Func<Role, bool>> predicate = null);

		void Update(Role role);

		void Create(Role role);

		void Delete(string name);

		bool Contains(Expression<Func<Role, bool>> predicate);
	}
}