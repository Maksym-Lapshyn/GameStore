using GameStore.Common.App_LocalResources;
using GameStore.Common.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace GameStore.Web.Models
{
	public class OrderDetailsViewModel : BaseEntity
	{
		[Display(Name = "GameKey", ResourceType = typeof(GlobalResource))]
		public string GameKey { get; set; }

		[JsonIgnore]
		[XmlIgnore]
		public GameViewModel Game { get; set; }

		[Display(Name = "Price", ResourceType = typeof(GlobalResource))]
		public decimal Price { get; set; }

		[Display(Name = "Quantity", ResourceType = typeof(GlobalResource))]
		public short Quantity { get; set; }

		[Display(Name = "Discount", ResourceType = typeof(GlobalResource))]
		public float Discount { get; set; }
	}
}