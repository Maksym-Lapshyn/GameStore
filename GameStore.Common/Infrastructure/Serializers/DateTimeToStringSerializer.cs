using System;
using System.Globalization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace GameStore.Common.Infrastructure.Serializers
{
	public class DateTimeToStringSerializer : IBsonSerializer
	{
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss.fff";

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
			context.Writer.WriteString(((DateTime)value).ToString(DateFormat));
		}

		private DateTime ParseDateTime(string dateString)
		{
			return DateTime.ParseExact(dateString, DateFormat, CultureInfo.InvariantCulture);
		}

		public Type ValueType => typeof(DateTime);
	}
}