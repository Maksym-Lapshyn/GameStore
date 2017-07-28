using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Globalization;

namespace GameStore.DAL.Infrastructure.Serializers
{
	public class DateTimeToStringSerializer : IBsonSerializer
	{
		object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			if (context.Reader.CurrentBsonType == BsonType.Null)
			{
				return DateTime.UtcNow;
			}

			var result = context.Reader.CurrentBsonType == BsonType.String
				? ParseDateTime(context.Reader.ReadString())
				: DateTime.UtcNow;

			return result;
		}

		public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
		{
			context.Writer.WriteString(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff"));
		}

		private DateTime ParseDateTime(string dateString)
		{
			return DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
		}

		public Type ValueType => typeof(DateTime);
	}
}