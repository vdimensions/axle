namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="int"/> and <see cref="decimal"/>.
    /// </summary>
    public class Int32ToDecimalConverter : AbstractTwoWayConverter<int, decimal>
    {
        protected override decimal DoConvert(int source) { return source; }

        protected override int DoConvertBack(decimal source) { return (int) source; }
    }
}