using CustomDateTimeOffset.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomDateTimeOffset.Converters
{
    public class CustomDateTimeConverter : ValueConverter<CustomDateTime, DateTimeOffsetValue>
    {
        public CustomDateTimeConverter() : base(
            customDateTime => new DateTimeOffsetValue(customDateTime.DateTime, customDateTime.Offset),
            value => new CustomDateTime(value.DateTime, value.Offset))
        {
        }
    }

    public class DateTimeOffsetValue
    {
        public DateTime DateTime { get; set; }
        public short Offset { get; set; }

        public DateTimeOffsetValue(DateTime dateTime, short offset)
        {
            DateTime = dateTime;
            Offset = offset;
        }
    }
}
