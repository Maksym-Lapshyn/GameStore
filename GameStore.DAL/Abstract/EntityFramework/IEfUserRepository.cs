using GameStore.Common.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfUserRepository
	{
		User GetSingle(Expression<Func<User, bool>> predicate);

		bool Contains(Expression<Func<User, bool>> predicate);

		IQueryable<User> GetAll(Expression<Func<User, bool>> predicate = null);

		void Update(User user);

		void Create(User user);

		void Delete(string name);
	}
}