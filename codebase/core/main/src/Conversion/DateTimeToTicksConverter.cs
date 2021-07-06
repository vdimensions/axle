using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="DateTime"/> and <see cref="long"/>.
    /// </summary>
    /// <seealso cref="DateTimeKind"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class DateTimeToTicksConverter : AbstractTwoWayConverter<DateTime, long>
    {
        /// <summary>
        /// A <see cref="DateTimeToTicksConverter"/> that converts ticks to local <see cref="DateTime"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly DateTimeToTicksConverter Local = new DateTimeToTicksConverter(DateTimeKind.Local);
        
        /// <summary>
        /// A <see cref="DateTimeToTicksConverter"/> that converts ticks to utc <see cref="DateTime"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly DateTimeToTicksConverter Utc = new DateTimeToTicksConverter(DateTimeKind.Utc);
        
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
