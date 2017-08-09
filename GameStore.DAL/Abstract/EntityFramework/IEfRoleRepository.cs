using GameStore.Common.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfRoleRepository
	{
		Role GetSingle(string name);

		IQueryable<Role> GetAll();

		void Update(Role role);

		void Create(Role role);

		void Delete(string name);
	}
}