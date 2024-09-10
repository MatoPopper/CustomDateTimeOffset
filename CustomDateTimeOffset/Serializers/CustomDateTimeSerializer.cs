using CustomDateTimeOffset.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CustomDateTimeOffset.Serializers
{
    /// <summary>
    /// A custom JSON converter for serializing and deserializing <see cref="CustomDateTime"/> objects.
    /// </summary>
    public class CustomDateTimeSerializer : JsonConverter<CustomDateTime>
    {
        /// <summary>
        /// Writes the <see cref="CustomDateTime"/> object to JSON as a string in the <see cref="DateTimeOffset"/> format.
        /// </summary>
        /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write to.</param>
        /// <param name="value">The <see cref="CustomDateTime"/> object to serialize.</param>
        /// <param name="options">Options to control serialization behavior.</param>
        public override void Write(Utf8JsonWriter writer, CustomDateTime value, JsonSerializerOptions options)
        {
            var dateTimeOffset = value.ToDateTimeOffset();
            writer.WriteStringValue(dateTimeOffset);
        }

        /// <summary>
        /// Reads a <see cref="CustomDateTime"/> object from its JSON representation as a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from.</param>
        /// <param name="typeToConvert">The type being converted (should be <see cref="CustomDateTime"/>).</param>
        /// <param name="options">Options to control deserialization behavior.</param>
        /// <returns>The deserialized <see cref="CustomDateTime"/> object.</returns>
        public override CustomDateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateTimeOffset = reader.GetDateTimeOffset();
            return CustomDateTime.FromDateTimeOffset(dateTimeOffset);
        }
    }
}
