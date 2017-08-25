using GameStore.Common.Entities.Localization;
using GameStore.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Common.Entities
{
	public class Role : BaseEntity
	{
		[NotMapped]
		public string Name { get; set; }

		public AccessLevel AccessLevel { get; set; }

		public virtual ICollection<User> Users { get; set; }

		public virtual ICollection<RoleLocale> RoleLocales { get; set; }
	}
}