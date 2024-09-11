namespace CustomDateTimeOffset.Models
{
    public class CustomDateTime : IComparable<CustomDateTime>
    {
        /// <summary>
        /// Gets or sets the date and time value.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the time offset in minutes.
        /// </summary>
        public short Offset { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDateTime"/> class with the specified date and time and offset.
        /// </summary>
        /// <param name="dateTime">The date and time value.</param>
        /// <param name="offset">The offset from UTC, in minutes.</param>
        public CustomDateTime(DateTime dateTime, short offset)
        {
            DateTime = dateTime;
            Offset = offset;
        }

        /// <summary>
        /// Static property representing the current UTC time.
        /// </summary>
        public static CustomDateTime UtcNow => new CustomDateTime(DateTime.UtcNow, 0);

        /// <summary>
        /// Adds the specified number of days to the current <see cref="CustomDateTime"/> instance.
        /// </summary>
        /// <param name="days">The number of days to add.</param>
        /// <returns>A new <see cref="CustomDateTime"/> with the added days.</returns>
        public CustomDateTime AddDays(int days)
        {
            return new CustomDateTime(DateTime.AddDays(days), Offset);
        }

        /// <summary>
        /// Converts the current <see cref="CustomDateTime"/> to a <see cref="DateTimeOffset"/> structure.
        /// </summary>
        /// <returns>A <see cref="DateTimeOffset"/> representing the same date, time, and offset.</returns>
        public DateTimeOffset ToDateTimeOffset()
        {
            string dateTimeString = this.DateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
            return new DateTimeOffset(DateTime.Parse(dateTimeString), TimeSpan.FromMinutes(Offset)); 
        }

        /// <summary>
        /// Creates a new <see cref="CustomDateTime"/> from a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="dateTimeOffset">The <see cref="DateTimeOffset"/> to convert from.</param>
        /// <returns>A new <see cref="CustomDateTime"/> instance with the same date, time, and offset.</returns>
        public static CustomDateTime FromDateTimeOffset(DateTimeOffset dateTimeOffset)
        {
            return new CustomDateTime(dateTimeOffset.DateTime, (short)dateTimeOffset.Offset.TotalMinutes);
        }

        /// <summary>
        /// Compares the current instance with another <see cref="CustomDateTime"/> object.
        /// </summary>
        /// <param name="other">The other <see cref="CustomDateTime"/> object to compare to.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(CustomDateTime? other)
        {
            if (other == null) return 1;

            var currentOffset = ToDateTimeOffset();
            var otherOffset = other.ToDateTimeOffset();
            return currentOffset.CompareTo(otherOffset);
        }

        /// <summary>
        /// Determines whether one <see cref="CustomDateTime"/> is earlier than another.
        /// </summary>
        /// <param name="left">The first <see cref="CustomDateTime"/> object to compare.</param>
        /// <param name="right">The second <see cref="CustomDateTime"/> object to compare.</param>
        /// <returns>True if the first <see cref="CustomDateTime"/> is earlier; otherwise, false.</returns>
        public static bool operator <(CustomDateTime left, CustomDateTime right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether one <see cref="CustomDateTime"/> is later than another.
        /// </summary>
        /// <param name="left">The first <see cref="CustomDateTime"/> object to compare.</param>
        /// <param name="right">The second <see cref="CustomDateTime"/> object to compare.</param>
        /// <returns>True if the first <see cref="CustomDateTime"/> is later; otherwise, false.</returns>
        public static bool operator >(CustomDateTime left, CustomDateTime right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether one <see cref="CustomDateTime"/> is earlier than or equal to another.
        /// </summary>
        /// <param name="left">The first <see cref="CustomDateTime"/> object to compare.</param>
        /// <param name="right">The second <see cref="CustomDateTime"/> object to compare.</param>
        /// <returns>True if the first <see cref="CustomDateTime"/> is earlier or equal; otherwise, false.</returns>
        public static bool operator <=(CustomDateTime left, CustomDateTime right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether one <see cref="CustomDateTime"/> is later than or equal to another.
        /// </summary>
        /// <param name="left">The first <see cref="CustomDateTime"/> object to compare.</param>
        /// <param name="right">The second <see cref="CustomDateTime"/> object to compare.</param>
        /// <returns>True if the first <see cref="CustomDateTime"/> is later or equal; otherwise, false.</returns>
        public static bool operator >=(CustomDateTime left, CustomDateTime right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
