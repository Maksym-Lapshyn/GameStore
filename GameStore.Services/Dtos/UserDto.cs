using GameStore.Common.Entities;

namespace GameStore.Services.Dtos
{
	public class UserDto : BaseEntity
	{
		public string Name { get; set; }

		public Role Role { get; set; }
	}
}