using GameStore.Common.Enums;
using GameStore.Common.Infrastructure.Serializers;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Common.Entities
{
	[BsonIgnoreExtraElements]
	public class Order : BaseEntity
	{
		[BsonElement("CustomerID")]
		public string NorthwindCustomerId { get; set; }

		[BsonElement("OrderID")]
		[NotMapped]
		public int OrderId { get; set; }

		[BsonSerializer(typeof(DateTimeToStringSerializer))]
		[Column(TypeName = "datetime2")]
		public DateTime OrderedDate { get; set; }

		[BsonSerializer(typeof(DateTimeToStringSerializer))]
		[Column(TypeName = "datetime2")]
		public DateTime? ShippedDate { get; set; }

		public OrderStatus OrderStatus { get; set; }

		public virtual User User { get; set; }

		public virtual ICollection<OrderDetails> OrderDetails { get; set; }
	}
}