using GameStore.Common.Enums;
using System.Collections.Generic;

namespace GameStore.Common.Entities
{
	public class Role : BaseEntity
	{
		public string Name { get; set; }

		public AccessLevel AccessLevel { get; set; }

		public virtual ICollection<User> Users { get; set; }
	}
}