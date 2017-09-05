using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.Common
{
	public class UserRepository : IUserRepository
	{
		private readonly IEfUserRepository _efRepository;

		public UserRepository(IEfUserRepository efRepository)
		{
			_efRepository = efRepository;
		}

		public User GetSingle(Expression<Func<User, bool>> predicate)
		{
			return _efRepository.GetSingle(predicate);
		}

		public bool Contains(Expression<Func<User, bool>> predicate = null)
		{
			return _efRepository.Contains(predicate);
		}

		public IEnumerable<User> GetAll(Expression<Func<User, bool>> predicate = null)
		{
			return _efRepository.GetAll(predicate).ToList();
		}

		public void Update(User user)
		{
			_efRepository.Update(user);
		}

		public void Create(User user)
		{
			_efRepository.Create(user);
		}

		public void Delete(string name)
		{
			_efRepository.Delete(name);
		}

		public User GetSingleOrDefault(Expression<Func<User, bool>> predicate)
		{
			return _efRepository.GetSingleOrDefault(predicate);
		}
	}
}