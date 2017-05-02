namespace Axle.Conversion
{
    /// <summary>
    /// An interface for converters that allow converting from one type to another and vice-versa.
    /// </summary>
    /// <seealso cref="IConverter{TSource, TTarget}"/>
    public interface ITwoWayConverter<TSource, TTarget> : IConverter<TSource, TTarget>
    {
        TSource ConvertBack(TTarget source);

        IConverter<TTarget, TSource> Invert();

        bool TryConvertBack(TTarget source, out TSource target);
    }
}