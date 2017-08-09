using GameStore.Common.Enums;

namespace GameStore.Common.Entities
{
	public class Role : BaseEntity
	{
		public string Name { get; set; }

		public AccessLevel AccessLevel { get; set; }
	}
}