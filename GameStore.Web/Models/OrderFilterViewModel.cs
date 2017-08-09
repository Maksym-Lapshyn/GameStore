using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class OrderFilterViewModel
	{
		[Required]
		[DisplayName("Date from")]
		public DateTime From { get; set; }

		[Required]
		[DisplayName("Date to")]
		public DateTime To { get; set; }
	}
}