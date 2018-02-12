namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ulong"/> and <see cref="float"/>.
    /// </summary>
    public class UInt64ToSingleConverter : AbstractTwoWayConverter<ulong, float>
    {
        protected override float DoConvert(ulong source) { return source; }

        protected override ulong DoConvertBack(float source) { return (ulong) source; }
    }
}