using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class LoginViewModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }

		public bool IsPersistent { get; set; }
	}
}