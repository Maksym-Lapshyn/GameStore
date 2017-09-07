using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;

namespace GameStore.Common.Infrastructure.Serializers
{
	public class StringOrInt32ToStringSerializer : IBsonSerializer
	{
		public Type ValueType => typeof(string);

		object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			return context.Reader.CurrentBsonType == BsonType.String ? context.Reader.ReadString() : context.Reader.ReadInt32().ToString();
		}

		public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
		{
			context.Writer.WriteString(value.ToString());
		}
	}
}