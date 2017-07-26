using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace GameStore.DAL.Infrastructure.Serializers
{
	public class Int32ToStringSerializer : IBsonSerializer
	{
		public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var result = context.Reader.CurrentBsonType == BsonType.Int32
				? context.Reader.ReadInt32().ToString()
				: context.Reader.ReadString();

			return result;
		}

		public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
		{
			context.Writer.WriteInt32(Convert.ToInt32(value));
		}

		public Type ValueType => typeof(string);
	}
}