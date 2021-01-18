using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="TimeSpan"/> and <see cref="long"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class TimeSpanToTicksConverter : AbstractTwoWayConverter<TimeSpan, long>
    {
        /// <inheritdoc />
        protected override long DoConvert(TimeSpan value) => value.Ticks;
        
        /// <inheritdoc />
        protected override TimeSpan DoConvertBack(long value) => new TimeSpan(value);
    }
}