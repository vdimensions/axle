using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="DateTimeOffset"/> and <see cref="DateTime"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class DateTimeOffsetToDateTimeConverter : AbstractTwoWayConverter<DateTimeOffset, DateTime>
    {
        private readonly DateTimeKind _preferredDateTimeKind;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeOffsetToDateTimeConverter"/> class. Converted
        /// <see cref="DateTime"/> values will assume the provided <paramref name="preferredDateTimeKind"/> as their
        /// <see cref="DateTime.Kind"/> value.
        /// </summary>
        /// <seealso cref="DateTimeKind"/>
        public DateTimeOffsetToDateTimeConverter(DateTimeKind preferredDateTimeKind)
        {
            _preferredDateTimeKind = preferredDateTimeKind;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeOffsetToDateTimeConverter"/> class. Converted
        /// <see cref="DateTime"/> values will assume <see cref="DateTimeKind.Unspecified"/> as their
        /// <see cref="DateTime.Kind"/> value.
        /// </summary>
        public DateTimeOffsetToDateTimeConverter() : this(DateTimeKind.Unspecified) { }
        
        /// <inheritdoc />
        protected override DateTime DoConvert(DateTimeOffset source)
        {
            switch (_preferredDateTimeKind)
            {
                case DateTimeKind.Utc:
                    return source.UtcDateTime;
                default:
                    return source.LocalDateTime;
            }
        }

        /// <inheritdoc />
        protected override DateTimeOffset DoConvertBack(DateTime source)
        {
            DateTime dateToConvert = source;
            if (source.Kind == DateTimeKind.Unspecified)
            {
                dateToConvert = DateTime.SpecifyKind(source, DateTimeKind.Local);
            }
            return new DateTimeOffset(dateToConvert);
        }
    }
}
