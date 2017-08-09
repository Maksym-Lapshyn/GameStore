using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Common
{
	public class UserRepository : IUserRepository
	{
		private readonly IEfUserRepository _userRepository;

		public UserRepository(IEfUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public User GetSingle(string name, string password = null)
		{
			return _userRepository.GetSingle(name, password);
		}

		public bool Contains(string name, string password)
		{
			return _userRepository.Contains(name, password);
		}

		public IEnumerable<User> GetAll()
		{
			return _userRepository.GetAll().ToList();
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