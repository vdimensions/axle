namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="float"/> and <see cref="double"/>.
    /// </summary>
    public class SingleToDoubleConverter : AbstractTwoWayConverter<float, double>
    {
        protected override double DoConvert(float source) { return source; }

        protected override float DoConvertBack(double source) { return (float) source; }
    }
}