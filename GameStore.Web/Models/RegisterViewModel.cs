using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class RegisterViewModel
	{
		[Required]
		public string Login { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		[DisplayName("Confirm password")]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public string PasswordConfirmation { get; set; }
	}
}