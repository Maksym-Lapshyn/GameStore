using GameStore.DAL.Entities;
using System.Collections.Generic;
using GameStore.Common.Entities;

namespace GameStore.DAL.Abstract.Common
{
	public interface IUserRepository
	{
		User GetSingle(string userName, string password = null);

		bool Contains(string userName, string password);

		IEnumerable<User> GetAll();
	}
}