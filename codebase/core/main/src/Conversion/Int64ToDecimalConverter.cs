namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="long"/> and <see cref="decimal"/>.
    /// </summary>
    public class Int64ToDecimalConverter : AbstractTwoWayConverter<long, decimal>
    {
        protected override decimal DoConvert(long source) { return source; }

        protected override long DoConvertBack(decimal source) { return (long) source; }
    }
}