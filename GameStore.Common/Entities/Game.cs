using GameStore.Common.Entities.Localization;
using GameStore.Common.Infrastructure.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Common.Entities
{
	[BsonIgnoreExtraElements]
	public class Game : BaseEntity
	{
		[BsonElement("ProductID")]
		[BsonSerializer(typeof(Int32ToStringSerializer))]
		public string Key { get; set; }

		[BsonElement("CategoryID")]
		[NotMapped]
		public int CategoryId { get; set; }

		[BsonElement("SupplierID")]
		[NotMapped]
		public int SupplierId { get; set; }

		[BsonElement("ProductName")]
		public string Name { get; set; }

		[NotMapped]
		public string Description { get; set; }

		public bool IsUpdated { get; set; }

		[BsonElement("UnitPrice")]
		[BsonRepresentation(BsonType.Double)]
		[Column(TypeName = "MONEY")]
		public decimal Price { get; set; }

		[Column(TypeName="datetime2")]
		public DateTime DateAdded { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime DatePublished { get; set; }

		public int ViewsCount { get; set; }

		[BsonRepresentation(BsonType.Int32)]
		public short UnitsInStock { get; set; }

		[BsonRepresentation(BsonType.Int32)]
		public bool Discontinued { get; set; }

		public virtual Publisher Publisher { get; set; }

		public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

		public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

		public virtual ICollection<PlatformType> PlatformTypes { get; set; } = new List<PlatformType>();

		public virtual ICollection<GameLocale> GameLocales { get; set; } = new List<GameLocale>();
	}
}