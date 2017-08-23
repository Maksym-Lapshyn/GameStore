using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GameStore.Common.Entities;
using GameStore.Common.Enums;

namespace GameStore.Web.Models
{
	public class RoleViewModel : BaseEntity
	{
		[Required]
		public string Name { get; set; }

		[Required]
		[DisplayName("Access Level")]
		public AccessLevel AccessLevel { get; set; }
	}
}