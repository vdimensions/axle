namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ulong"/> and <see cref="decimal"/>.
    /// </summary>
    public class UInt64ToDecimalConverter : AbstractTwoWayConverter<ulong, decimal>
    {
        protected override decimal DoConvert(ulong source) { return source; }

        protected override ulong DoConvertBack(decimal source) { return (ulong) source; }
    }
}