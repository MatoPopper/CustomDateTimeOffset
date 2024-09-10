using CustomDateTimeOffset.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomDateTimeOffset.Converters
{
    /// <summary>
    /// A custom value converter for converting between <see cref="CustomDateTime"/> and <see cref="DateTimeOffsetValue"/> in Entity Framework Core.
    /// </summary>
    public class CustomDateTimeConverter : ValueConverter<CustomDateTime, DateTimeOffsetValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDateTimeConverter"/> class.
        /// Converts <see cref="CustomDateTime"/> to <see cref="DateTimeOffsetValue"/> and vice versa.
        /// </summary>
        public CustomDateTimeConverter() : base(
            customDateTime => new DateTimeOffsetValue(customDateTime.DateTime, customDateTime.Offset),
            value => new CustomDateTime(value.DateTime, value.Offset))
        {
        }
    }

    /// <summary>
    /// A helper class to store both the DateTime and its offset as a value object.
    /// Used internally by <see cref="CustomDateTimeConverter"/> for conversion.
    /// </summary>
    public class DateTimeOffsetValue
    {
        /// <summary>
        /// Gets or sets the DateTime value.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the offset in minutes.
        /// </summary>
        public short Offset { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeOffsetValue"/> class with the specified DateTime and offset.
        /// </summary>
        /// <param name="dateTime">The DateTime value to be stored.</param>
        /// <param name="offset">The offset from UTC, in minutes.</param>
        public DateTimeOffsetValue(DateTime dateTime, short offset)
        {
            DateTime = dateTime;
            Offset = offset;
        }
    }
}
