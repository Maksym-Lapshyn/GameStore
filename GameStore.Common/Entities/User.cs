using System.Collections.Generic;

namespace GameStore.Common.Entities
{
	public class User : BaseEntity
	{
		public string Login { get; set; }

		public string Password { get; set; }

		public string AuthenticationTicket { get; set; }
		
		public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

		public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
	}
}