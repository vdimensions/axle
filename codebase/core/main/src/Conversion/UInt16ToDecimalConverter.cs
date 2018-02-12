namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ushort"/> and <see cref="decimal"/>.
    /// </summary>
    public class UInt16ToDecimalConverter : AbstractTwoWayConverter<ushort, decimal>
    {
        protected override decimal DoConvert(ushort source) { return source; }

        protected override ushort DoConvertBack(decimal source) { return (ushort) source; }
    }
}