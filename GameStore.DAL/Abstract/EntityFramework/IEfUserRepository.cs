using GameStore.DAL.Entities;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfUserRepository
	{
		User GetSingle(string username, string password);

		bool Contains(string username, string password);
	}
}