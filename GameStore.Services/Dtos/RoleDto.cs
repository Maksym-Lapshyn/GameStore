using GameStore.Common.Entities;
using GameStore.Common.Enums;

namespace GameStore.Services.Dtos
{
	public class RoleDto : BaseEntity
	{
		public string Name { get; set; }

		public AccessLevel AccessLevel { get; set; }
	}
}