
namespace CustomDateTimeOffset.Models
{
    public class CustomDateTime : IComparable<CustomDateTime>
    {
        public DateTime DateTime { get; set; }
        public short Offset { get; set; }

        public CustomDateTime(DateTime dateTime, short offset)
        {
            DateTime = dateTime;
            Offset = offset;
        }

        // Statická vlastnosť pre UtcNow
        public static CustomDateTime UtcNow => new CustomDateTime(DateTime.UtcNow, 0);

        // Metóda pre pridanie dní
        public CustomDateTime AddDays(int days)
        {
            return new CustomDateTime(DateTime.AddDays(days), Offset);
        }

        // Konverzia na DateTimeOffset
        public DateTimeOffset ToDateTimeOffset()
        {
            return new DateTimeOffset(DateTime, TimeSpan.FromMinutes(Offset));
        }

        public static CustomDateTime FromDateTimeOffset(DateTimeOffset dateTimeOffset)
        {
            return new CustomDateTime(dateTimeOffset.DateTime.ToUniversalTime(), (short)dateTimeOffset.Offset.TotalMinutes);
        }

        public int CompareTo(CustomDateTime? other)
        {
            if (other == null) return 1;

            // Porovnaj podľa DateTime alebo DateTimeOffset
            var currentOffset = ToDateTimeOffset();
            var otherOffset = other.ToDateTimeOffset();
            return currentOffset.CompareTo(otherOffset);
        }

        public static bool operator <(CustomDateTime left, CustomDateTime right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(CustomDateTime left, CustomDateTime right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(CustomDateTime left, CustomDateTime right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(CustomDateTime left, CustomDateTime right)
        {
            return left.CompareTo(right) >= 0;
        }
    }


}
