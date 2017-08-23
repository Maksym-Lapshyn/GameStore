using GameStore.Common.Entities;
using GameStore.Common.Enums;
using GameStore.Web.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class RoleViewModel : BaseEntity
	{
		[Required(ErrorMessageResourceName = "RoleNameIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "RoleName", ResourceType = typeof(GlobalResource))]
		public string Name { get; set; }

		[Required(ErrorMessageResourceName = "AccessLevelIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "AccessLevel", ResourceType = typeof(GlobalResource))]
		public AccessLevel AccessLevel { get; set; }
	}
}