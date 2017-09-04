using GameStore.Common.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Common.Enums
{
	public enum AccessLevel
	{
		[Display(Name = "User", ResourceType = typeof(GlobalResource))]
		User,
		[Display(Name = "Manager", ResourceType = typeof(GlobalResource))]
		Manager,
		[Display(Name = "Moderator", ResourceType = typeof(GlobalResource))]
		Moderator,
		[Display(Name = "Administrator", ResourceType = typeof(GlobalResource))]
		Administrator
	}
}