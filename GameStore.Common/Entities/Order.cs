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
		[BsonElement("OrderID")]
		[NotMapped]
		public int OrderId { get; set; }

		[BsonSerializer(typeof(NullableDateTimeToStringSerializer))]
		[Column(TypeName = "datetime2")]
		[BsonElement("OrderDate")]
		public DateTime? DateOrdered { get; set; }

		public decimal TotalPrice { get; set; }

		[BsonSerializer(typeof(NullableDateTimeToStringSerializer))]
		[Column(TypeName = "datetime2")]
		[BsonElement("ShippedDate")]
		public DateTime? DateShipped { get; set; }

		public OrderStatus OrderStatus { get; set; }

		public virtual User User { get; set; }

		public virtual ICollection<OrderDetails> OrderDetails { get; set; }
	}
}