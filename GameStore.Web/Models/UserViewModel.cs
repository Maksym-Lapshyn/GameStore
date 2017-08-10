using GameStore.Common.Entities;
using GameStore.Web.Infrastructure.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class UserViewModel : BaseEntity
	{
		public UserViewModel()
		{
			RolesData = new List<RoleViewModel>();
			RolesInput = new List<string>();
		}

		[Required]
		public string Name { get; set; }

		public List<RoleViewModel> RolesData { get; set; }

		[CannotBeEmpty]
		public List<string> RolesInput { get; set; }
	}
}