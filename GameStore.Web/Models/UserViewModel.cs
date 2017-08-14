using GameStore.Common.Entities;
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
			Orders = new List<OrderViewModel>();
		}

		[Required]
		public string Login { get; set; }

		[Required]
		public string Password { get; set; }

		public List<RoleViewModel> RolesData { get; set; }

		public List<string> RolesInput { get; set; }

		public List<OrderViewModel> Orders { get; set; }
	}
}