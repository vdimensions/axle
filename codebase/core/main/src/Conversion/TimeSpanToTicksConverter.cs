using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="TimeSpan"/> and <see cref="long"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class TimeSpanToTicksConverter : AbstractTwoWayConverter<TimeSpan, long>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="TimeSpanToTicksConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly TimeSpanToTicksConverter Instance = new TimeSpanToTicksConverter();
        
        /// <inheritdoc />
        protected override long DoConvert(TimeSpan value) => value.Ticks;
        
        /// <inheritdoc />
        protected override TimeSpan DoConvertBack(long value) => new TimeSpan(value);
    }
}