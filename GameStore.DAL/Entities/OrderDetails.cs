using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace GameStore.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class OrderDetails : BaseEntity
	{
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

		[BsonRepresentation(BsonType.Int32)]
		public float Discount { get; set; }
	}
}