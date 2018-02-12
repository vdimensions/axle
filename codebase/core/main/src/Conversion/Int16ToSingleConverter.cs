namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="short"/> and <see cref="float"/>.
    /// </summary>
    public class Int16ToSingleConverter : AbstractTwoWayConverter<short, float>
    {
        protected override float DoConvert(short source) { return source; }

        protected override short DoConvertBack(float source) { return (short) source; }
    }
}