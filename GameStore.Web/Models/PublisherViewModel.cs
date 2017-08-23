using GameStore.Common.Entities;
using GameStore.Web.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class PublisherViewModel : BaseEntity
	{
		[Required(ErrorMessageResourceName = "CompanyNameIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "CompanyName", ResourceType = typeof(GlobalResource))]
		public string CompanyName { get; set; }

		[Required(ErrorMessageResourceName = "PublisherDescriptionIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "PublisherDescription", ResourceType = typeof(GlobalResource))]
		public string Description { get; set; }

		[Required(ErrorMessageResourceName = "HomePageIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "HomePage", ResourceType = typeof(GlobalResource))]
		public string HomePage { get; set; }
	}
}