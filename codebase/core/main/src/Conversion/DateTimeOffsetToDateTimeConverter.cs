using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="DateTimeOffset"/> and <see cref="DateTime"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class DateTimeOffsetToDateTimeConverter : AbstractTwoWayConverter<DateTimeOffset, DateTime>
    {
        /// <summary>
        /// A shared <see cref="DateTimeOffsetToDateTimeConverter"/> instance that uses the
        /// <see cref="DateTimeOffset.LocalDateTime"/> value during the conversion.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly DateTimeOffsetToDateTimeConverter Local
            = new DateTimeOffsetToDateTimeConverter(DateTimeKind.Local);
        
        /// <summary>
        /// A shared <see cref="DateTimeOffsetToDateTimeConverter"/> instance that uses the 
        /// <see cref="DateTimeOffset.UtcDateTime"/> value during the conversion.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly DateTimeOffsetToDateTimeConverter Utc
            = new DateTimeOffsetToDateTimeConverter(DateTimeKind.Utc);
        
        private readonly DateTimeKind _preferredDateTimeKind;

        /// <summary>
        /// Creates a new instance of the <see cref="DateTimeOffsetToDateTimeConverter"/> class with the option to
        /// determine whether to use local or utc dates.
        /// </summary>
        /// <param name="preferredDateTimeKind">
        /// Setting <see cref="DateTimeKind.Utc"/> will cause the date time value to be provided by the
        /// <see cref="DateTimeOffset.UtcDateTime"/> property of the <see cref="DateTimeOffset"/> value being converted,
        /// in all other cases the <see cref="DateTimeOffset.LocalDateTime"/> property is used.
        /// </param>
        public DateTimeOffsetToDateTimeConverter(DateTimeKind preferredDateTimeKind)
        {
            _preferredDateTimeKind = preferredDateTimeKind;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="DateTimeOffsetToDateTimeConverter"/> that converts a
        /// <see cref="DateTimeOffset"/> value to a local <see cref="DateTime"/> value.
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
