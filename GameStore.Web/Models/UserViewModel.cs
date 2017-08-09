using System.ComponentModel.DataAnnotations;
using GameStore.Common.Entities;

namespace GameStore.Web.Models
{
	public class UserViewModel : BaseEntity
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public Role Role { get; set; }
	}
}