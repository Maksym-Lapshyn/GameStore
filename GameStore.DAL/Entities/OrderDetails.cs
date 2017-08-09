using GameStore.DAL.Infrastructure.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.Common.Entities;

namespace GameStore.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class OrderDetails : BaseEntity
	{
		[BsonElement("ProductID")]
		[BsonSerializer(typeof(Int32ToStringSerializer))]
		public string GameKey { get; set; }

		[BsonElement("OrderID")]
		[NotMapped]
		public int OrderId { get; set; }

		public virtual Game Game { get; set; }

		[BsonElement("UnitPrice")]
		[BsonRepresentation(BsonType.Double)]
		public decimal Price { get; set; }

		[BsonRepresentation(BsonType.Int64)]
		public short Quantity { get; set; }

		[BsonSerializer(typeof(DoubleOrInt32ToFloatSerializer))]
		public float Discount { get; set; }
	}
}