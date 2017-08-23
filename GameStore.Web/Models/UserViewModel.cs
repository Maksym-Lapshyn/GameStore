using GameStore.Common.Entities;
using GameStore.Web.App_LocalResources;
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
			Orders = new List<OrderViewModel>();
		}

		[Required(ErrorMessageResourceName = "LoginIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "Login", ResourceType = typeof(GlobalResource))]
		public string Login { get; set; }

		[Required(ErrorMessageResourceName = "PasswordIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "Password", ResourceType = typeof(GlobalResource))]
		public string Password { get; set; }

		public List<RoleViewModel> RolesData { get; set; }


		[Display(Name = "RoleName", ResourceType = typeof(GlobalResource))]
		[CannotBeEmpty(ErrorMessageResourceName = "RolesInputCannotBeEmpty", ErrorMessageResourceType = typeof(GlobalResource))]
		public List<string> RolesInput { get; set; }

		public List<OrderViewModel> Orders { get; set; }
	}
}