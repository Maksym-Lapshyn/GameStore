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
			Orders = new List<OrderDto>();
		}

		public string Login { get; set; }

		public string Password { get; set; }

		public List<string> RolesInput { get; set; }

		public List<RoleDto> RolesData { get; set; }

		public List<OrderDto> Orders { get; set; }
	}
}