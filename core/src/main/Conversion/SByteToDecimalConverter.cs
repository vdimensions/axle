namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="sbyte"/> and <see cref="decimal"/>.
    /// </summary>
    public class SByteToDecimalConverter : AbstractTwoWayConverter<sbyte, decimal>
    {
        protected override decimal DoConvert(sbyte source) { return source; }

        protected override sbyte DoConvertBack(decimal source) { return (sbyte) source; }
    }
}