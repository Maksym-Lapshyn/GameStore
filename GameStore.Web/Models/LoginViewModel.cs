using GameStore.Web.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessageResourceName = "LoginIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "Login", ResourceType = typeof(GlobalResource))]
		public string Login { get; set; }

		[Required(ErrorMessageResourceName = "PasswordIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "Password", ResourceType = typeof(GlobalResource))]
		public string Password { get; set; }

		[Display(Name = "StayInTheSystem", ResourceType = typeof(GlobalResource))]
		public bool IsPersistent { get; set; }
	}
}