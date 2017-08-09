using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IRoleService
	{
		RoleDto GetSingle(string name);

		IEnumerable<RoleDto> GetAll();

		void Update(RoleDto role);

		void Create(RoleDto role);

		void Delete(string name);
	}
}