using GameStore.Common.Entities;
using System.Collections.Generic;

namespace GameStore.Services.Dtos
{
	public class UserDto : BaseEntity
	{
		public UserDto()
		{
			RolesData = new List<RoleDto>();
			RolesInput = new List<string>();
		}

		public string Name { get; set; }

		public List<RoleDto> RolesData { get; set; }

		public List<string> RolesInput { get; set; }
	}
}