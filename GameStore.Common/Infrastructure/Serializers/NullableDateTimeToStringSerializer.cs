using System;
using System.Globalization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace GameStore.Common.Infrastructure.Serializers
{
    class NullableDateTimeToStringSerializer : IBsonSerializer
    {
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss.fff";
        private const string DefaultDateString = "NULL";

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var result = context.Reader.CurrentBsonType == BsonType.String
                ? ParseDateTime(context.Reader.ReadString())
                : DateTime.UtcNow;

            return result;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var date = (DateTime?)value;

            if (!date.HasValue)
            {
                context.Writer.WriteString(DefaultDateString);
            }

            context.Writer.WriteString((date.Value).ToString(DateFormat));
        }

        private DateTime? ParseDateTime(string dateString)
        {
            DateTime date;
            var result = DateTime.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            return !result ? null : (DateTime?)date;
        }

        public Type ValueType => typeof(DateTime?);
    }
}
