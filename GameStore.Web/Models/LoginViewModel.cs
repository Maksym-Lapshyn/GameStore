using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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