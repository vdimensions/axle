namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ushort"/> and <see cref="float"/>.
    /// </summary>
    public class UInt16ToSingleConverter : AbstractTwoWayConverter<ushort, float>
    {
        protected override float DoConvert(ushort source) { return source; }

        protected override ushort DoConvertBack(float source) { return (ushort) source; }
    }
}