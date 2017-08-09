using System.Linq;
using GameStore.Common.Entities;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfUserRepository
	{
		User GetSingle(string name, string password = null);

		bool Contains(string name, string password);

		IQueryable<User> GetAll();

		void Update(User user);

		void Create(User user);

		void Delete(string name);
	}
}