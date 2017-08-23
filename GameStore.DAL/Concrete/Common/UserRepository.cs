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
		private readonly IEfUserRepository _userRepository;

		public UserRepository(IEfUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public User GetSingle(Expression<Func<User, bool>> predicate)
		{
			return _userRepository.GetSingle(predicate);
		}

		public bool Contains(Expression<Func<User, bool>> predicate = null)
		{
			return _userRepository.Contains(predicate);
		}

		public IEnumerable<User> GetAll(Expression<Func<User, bool>> predicate = null)
		{
			return _userRepository.GetAll(predicate).ToList();
		}

		public void Update(User user)
		{
			_userRepository.Update(user);
		}

		public void Create(User user)
		{
			_userRepository.Create(user);
		}

		public void Delete(string name)
		{
			_userRepository.Delete(name);
		}
	}
}