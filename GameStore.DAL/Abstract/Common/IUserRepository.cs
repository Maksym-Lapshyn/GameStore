using GameStore.Common.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Common
{
	public interface IUserRepository
	{
		User GetSingle(string name, string password = null);

		bool Contains(string name, string password);

		IEnumerable<User> GetAll();

		void Update(User user);

		void Create(User user);

		void Delete(string name);
	}
}