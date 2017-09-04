using GameStore.Common.App_LocalResources;
using GameStore.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class PlatformTypeViewModel : BaseEntity
	{
		[Display(Name = "PlatformType", ResourceType = typeof(GlobalResource))]
		public string Type { get; set; }
	}
}