namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="long"/> and <see cref="float"/>.
    /// </summary>
    public class Int64ToSingleConverter : AbstractTwoWayConverter<long, float>
    {
        protected override float DoConvert(long source) { return source; }

        protected override long DoConvertBack(float source) { return (long) source; }
    }
}