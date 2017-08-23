using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class LoginViewModel
	{
		[Required]
		public string Login { get; set; }

		[Required]
		public string Password { get; set; }

		[DisplayName("Stay in the system")]
		public bool IsPersistent { get; set; }
	}
}