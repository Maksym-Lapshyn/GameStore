using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IRoleService
	{
		RoleDto GetSingle(string language, string name);

		IEnumerable<RoleDto> GetAll(string language);

		void Update(string language, RoleDto role);

		void Create(string language, RoleDto role);

		void Delete(string name);

		bool Contains(string name);
	}
}