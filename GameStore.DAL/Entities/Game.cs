using GameStore.DAL.Infrastructure.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Game : BaseEntity
	{
		[StringLength(450)]
		[Index(IsUnique = true)]
		[BsonElement("ProductID")]
		[BsonSerializer(typeof(Int32ToStringSerializer))]
		public string Key { get; set; }

		[BsonElement("ProductName")]
		public string Name { get; set; }

		public string Description { get; set; }

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

		public int? PublisherId { get; set; }

		public virtual Publisher Publisher { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }

		public virtual ICollection<Genre> Genres { get; set; }

		public virtual ICollection<PlatformType> PlatformTypes { get; set; }
	}
}
