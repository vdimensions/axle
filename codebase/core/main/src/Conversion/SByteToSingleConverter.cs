namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="sbyte"/> and <see cref="float"/>.
    /// </summary>
    public class SByteToSingleConverter : AbstractTwoWayConverter<sbyte, float>
    {
        protected override float DoConvert(sbyte source) { return source; }

        protected override sbyte DoConvertBack(float source) { return (sbyte) source; }
    }
}