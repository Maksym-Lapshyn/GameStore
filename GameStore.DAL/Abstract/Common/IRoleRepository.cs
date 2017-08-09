using GameStore.Common.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Common
{
	public interface IRoleRepository
	{
		Role GetSingle(string name);

		IEnumerable<Role> GetAll();

		void Update(Role role);

		void Create(Role role);

		void Delete(string name);
	}
}