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
		public string UserLogin { get; set; }

		[BsonElement("OrderID")]
		[NotMapped]
		public int OrderId { get; set; }

		[BsonSerializer(typeof(DateTimeToStringSerializer))]
		[Column(TypeName = "datetime2")]
		public DateTime DateOrdered { get; set; }

		[BsonSerializer(typeof(NullableDateTimeToStringSerializer))]
		[Column(TypeName = "datetime2")]
        [BsonElement("ShippedDate")]
        public DateTime? DateShipped { get; set; }

		public OrderStatus OrderStatus { get; set; }

		public virtual ICollection<OrderDetails> OrderDetails { get; set; }
	}
}