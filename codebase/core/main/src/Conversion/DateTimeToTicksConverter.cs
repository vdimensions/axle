using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="DateTime"/> and <see cref="long"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class DateTimeToTicksConverter : AbstractTwoWayConverter<DateTime, long>
    {
        private readonly DateTimeKind _preferredDateTimeKind;

        public DateTimeToTicksConverter(DateTimeKind preferredDateTimeKind)
        {
            _preferredDateTimeKind = preferredDateTimeKind;
        }
        public DateTimeToTicksConverter() : this(DateTimeKind.Unspecified) { }
        
        /// <inheritdoc />
        protected override long DoConvert(DateTime source) => source.Ticks;

        /// <inheritdoc />
        protected override DateTime DoConvertBack(long source) => new DateTime(source, _preferredDateTimeKind);
    }
}
