using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models
{
	public class PublisherViewModel
	{
		public PublisherViewModel()
		{
			Publishers = new List<PublisherViewModel>();
		}

		public int Id { get; set; }

		public string NorthwindId { get; set; }

		[Required]
		[DisplayName("Company Name")]
		public string CompanyName { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		[DisplayName("Home Page")]
		public string HomePage { get; set; }

		public List<PublisherViewModel> Publishers { get; set; }
	}
}