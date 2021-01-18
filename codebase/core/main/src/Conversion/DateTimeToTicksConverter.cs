using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="DateTime"/> and <see cref="long"/>.
    /// </summary>
    /// <seealso cref="DateTimeKind"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class DateTimeToTicksConverter : AbstractTwoWayConverter<DateTime, long>
    {
        private readonly DateTimeKind _preferredDateTimeKind;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeToTicksConverter"/> class. Converted
        /// <see cref="DateTime"/> values will assume the provided <paramref name="preferredDateTimeKind"/> as their
        /// <see cref="DateTime.Kind"/> value.
        /// </summary>
        public DateTimeToTicksConverter(DateTimeKind preferredDateTimeKind)
        {
            _preferredDateTimeKind = preferredDateTimeKind;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeToTicksConverter"/> class. Converted
        /// <see cref="DateTime"/> values will assume <see cref="DateTimeKind.Unspecified"/> as their
        /// <see cref="DateTime.Kind"/> value.
        /// </summary>
        public DateTimeToTicksConverter() : this(DateTimeKind.Unspecified) { }
        
        /// <inheritdoc />
        protected override long DoConvert(DateTime source) => source.Ticks;

        /// <inheritdoc />
        protected override DateTime DoConvertBack(long source) => new DateTime(source, _preferredDateTimeKind);
    }
}
