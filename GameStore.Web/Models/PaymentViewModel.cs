using GameStore.Common.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class PaymentViewModel
	{
		public string SellersCardNumber { get; set; }

		[Required(ErrorMessageResourceName = "CardNumberIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "CardNumber", ResourceType = typeof(GlobalResource))]
		public string BuyersCardNumber { get; set; }

		[Required(ErrorMessageResourceName = "FullNameIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "FullName", ResourceType = typeof(GlobalResource))]
		public string BuyersFullName { get; set; }

		[Required(ErrorMessageResourceName = "CvvCodeIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "CvvCode", ResourceType = typeof(GlobalResource))]
		public int CvvCode { get; set; }

		[Required(ErrorMessageResourceName = "ExpirationMonthIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "ExpirationMonth", ResourceType = typeof(GlobalResource))]
		public int ExpirationMonth { get; set; }

		[Required(ErrorMessageResourceName = "ExpirationYear", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "ExpirationYear", ResourceType = typeof(GlobalResource))]
		public int ExpirationYear { get; set; }

		public string PaymentPurpose { get; set; }

		[Display(Name = "TotalPrice", ResourceType = typeof(GlobalResource))]
		public decimal PaymentAmount { get; set; }

		[Display(Name = "Email", ResourceType = typeof(GlobalResource))]
		public string Email { get; set; }

		[Display(Name = "Phone", ResourceType = typeof(GlobalResource))]
		public string Phone { get; set; }
	}
}