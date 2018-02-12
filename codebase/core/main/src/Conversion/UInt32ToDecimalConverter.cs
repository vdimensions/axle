namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="uint"/> and <see cref="decimal"/>.
    /// </summary>
    public class UInt32ToDecimalConverter : AbstractTwoWayConverter<uint, decimal>
    {
        protected override decimal DoConvert(uint source) { return source; }

        protected override uint DoConvertBack(decimal source) { return (uint) source; }
    }
}