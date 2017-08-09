using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;

namespace GameStore.DAL.Infrastructure.Serializers
{
	public class DoubleOrInt32ToFloatSerializer : IBsonSerializer
	{
		object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			if (context.Reader.CurrentBsonType == BsonType.Double)
			{
				return (float)context.Reader.ReadDouble();
			}
			
			return (float)context.Reader.ReadInt32();
		}

		public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
		{
			context.Writer.WriteDouble((double)value);
		}

		public Type ValueType => typeof(float);
	}
}