namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="uint"/> and <see cref="float"/>.
    /// </summary>
    public class UInt32ToSingleConverter : AbstractTwoWayConverter<uint, float>
    {
        protected override float DoConvert(uint source) { return source; }

        protected override uint DoConvertBack(float source) { return (uint) source; }
    }
}