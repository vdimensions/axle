namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="short"/> and <see cref="decimal"/>.
    /// </summary>
    public class Int16ToDecimalConverter : AbstractTwoWayConverter<short, decimal>
    {
        protected override decimal DoConvert(short source) { return source; }

        protected override short DoConvertBack(decimal source) { return (short) source; }
    }
}