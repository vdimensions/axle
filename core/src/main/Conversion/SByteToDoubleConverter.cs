namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="sbyte"/> and <see cref="double"/>.
    /// </summary>
    public class SByteToDoubleConverter : AbstractTwoWayConverter<sbyte, double>
    {
        protected override double DoConvert(sbyte source) { return source; }

        protected override sbyte DoConvertBack(double source) { return (sbyte) source; }
    }
}