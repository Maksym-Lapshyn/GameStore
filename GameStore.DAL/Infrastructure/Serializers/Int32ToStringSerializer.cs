using System;
using MongoDB.Bson.Serialization;

namespace GameStore.DAL.Infrastructure.Serializers
{
	public class Int32ToStringSerializer : IBsonSerializer
	{
		public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			return context.Reader.ReadString();
		}

		public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
		{
			context.Writer.WriteInt32(Convert.ToInt32(value));
		}

		public Type ValueType => typeof(string);
	}
}
