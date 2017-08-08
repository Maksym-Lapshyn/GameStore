using GameStore.Common.Enums;

namespace GameStore.Common.Entities
{
	public class User
	{
		public string Id { get; set; }

		public bool IsDeleted { get; set; }

		public string Name { get; set; }

		public string Password { get; set; }
		
		public UserRoles Role { get; set; }
	}
}