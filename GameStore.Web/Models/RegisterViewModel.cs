using GameStore.Common.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class RegisterViewModel
	{
		[Required(ErrorMessageResourceName = "LoginIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "Login", ResourceType = typeof(GlobalResource))]
		public string Login { get; set; }

		[Required(ErrorMessageResourceName = "Password", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "Password", ResourceType = typeof(GlobalResource))]
		public string Password { get; set; }

		[Required(ErrorMessageResourceName = "PasswordConfirmationIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "PasswordConfirmation", ResourceType = typeof(GlobalResource))]
		[Compare("Password", ErrorMessageResourceName = "PasswordsDoNotMatch", ErrorMessageResourceType = typeof(GlobalResource))]
		public string PasswordConfirmation { get; set; }
	}
}