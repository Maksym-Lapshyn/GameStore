using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;

namespace GameStore.Common.Infrastructure.Serializers
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
			if (int.TryParse(value.ToString(), out int result))
			{
				context.Writer.WriteInt32(result);
			}
			else
			{
				context.Writer.WriteString(value.ToString());
			}
		}

		public Type ValueType => typeof(string);
	}
}