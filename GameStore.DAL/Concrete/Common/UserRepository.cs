using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using GameStore.Common.Entities;

namespace GameStore.DAL.Concrete.Common
{
	public class UserRepository : IUserRepository
	{
		private readonly IEfUserRepository _userRepository;

		public UserRepository(IEfUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public User GetSingle(string userName, string password = null)
		{
			return _userRepository.GetSingle(userName, password);
		}

		public bool Contains(string userName, string password)
		{
			return _userRepository.Contains(userName, password);
		}

		public IEnumerable<User> GetAll()
		{
			return _userRepository.GetAll().ToList();
		}
	}
}