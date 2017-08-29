using GameStore.Common.Entities.Localization;
using GameStore.Common.Infrastructure.Serializers;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Common.Entities
{
	[BsonIgnoreExtraElements]
	public class Publisher : BaseEntity
	{
		public string CompanyName { get; set; }

		[BsonElement("SupplierID")]
		[NotMapped]
		public int SupplierId { get; set; }

		[BsonElement("Phone")]
		[BsonSerializer(typeof(StringOrInt32ToStringSerializer))]
		[Column(TypeName = "NTEXT")]
		[NotMapped]
		[BsonIgnore]
		public string Description { get; set; }

		[Column(TypeName = "NTEXT")]
		public string HomePage { get; set; }

		[BsonIgnore]
		public ICollection<Game> Games { get; set; } = new List<Game>();

		[BsonIgnore]
		public ICollection<PublisherLocale> PublisherLocales { get; set; } = new List<PublisherLocale>();
	}
}