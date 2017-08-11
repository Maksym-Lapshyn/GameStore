using System.Collections.Generic;

namespace GameStore.Common.Entities
{
	public class User : BaseEntity
	{
		public string Login { get; set; }

		public string Password { get; set; }
		
		public virtual ICollection<Role> Roles { get; set; }

		public virtual ICollection<Order> Orders { get; set; }
	}
}