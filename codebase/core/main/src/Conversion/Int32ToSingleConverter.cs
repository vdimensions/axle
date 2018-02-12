namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="int"/> and <see cref="float"/>.
    /// </summary>
    public class Int32ToSingleConverter : AbstractTwoWayConverter<int, float>
    {
        protected override float DoConvert(int source) { return source; }

        protected override int DoConvertBack(float source) { return (int) source; }
    }
}