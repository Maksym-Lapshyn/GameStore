using GameStore.DAL.Infrastructure.Serializers;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Order : BaseEntity
	{
		[BsonElement("CustomerID")]
		public string CustomerId { get; set; }

		[BsonElement("OrderID")]
		[NotMapped]
		public int OrderId { get; set; }

		[BsonSerializer(typeof(DateTimeToStringSerializer))]
		public DateTime OrderDate { get; set; }

		public virtual ICollection<OrderDetails> OrderDetails { get; set; }
	}
}