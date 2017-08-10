using System.Collections.Generic;

namespace GameStore.Common.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; set; }

		public string Password { get; set; }
		
		public virtual ICollection<Role> Roles { get; set; }
	}
}