using GameStore.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.Common
{
	public interface IUserRepository
	{
		User GetSingleOrDefault(Expression<Func<User, bool>> predicate);

		User GetSingle(Expression<Func<User, bool>> predicate);

		bool Contains(Expression<Func<User, bool>> predicate);

		IEnumerable<User> GetAll(Expression<Func<User, bool>> predicate = null);

		void Update(User user);

		void Create(User user);

		void Delete(string name);
	}
}