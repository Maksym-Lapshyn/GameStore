using GameStore.Common.Entities;
using GameStore.Web.App_LocalResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class OrderFilterViewModel : BaseEntity
	{
		[Required(ErrorMessageResourceName = "PasswordIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "BeginningDate", ResourceType = typeof(GlobalResource))]
		public DateTime BeginningDate { get; set; }

		[Required(ErrorMessageResourceName = "PasswordIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "EndingDate", ResourceType = typeof(GlobalResource))]
		public DateTime EndingDate { get; set; }
	}
}