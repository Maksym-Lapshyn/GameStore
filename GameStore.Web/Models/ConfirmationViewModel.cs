using GameStore.Common.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class ConfirmationViewModel
	{
		public int PaymentId { get; set; }

		[Display(Name = "ConfirmationCode", ResourceType = typeof(GlobalResource))]
		public string ConfirmationCode { get; set; }
	}
}