using CustomDateTimeOffset.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CustomDateTimeOffset.Serializers
{
    public class CustomDateTimeSerializer : JsonConverter<CustomDateTime>
    {
        public override void Write(Utf8JsonWriter writer, CustomDateTime value, JsonSerializerOptions options)
        {
            var dateTimeOffset = value.ToDateTimeOffset(); 
            writer.WriteStringValue(dateTimeOffset); 
        }

        public override CustomDateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateTimeOffset = reader.GetDateTimeOffset(); 
            return CustomDateTime.FromDateTimeOffset(dateTimeOffset); 
        }
    }
}
