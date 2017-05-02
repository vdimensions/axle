namespace Axle.Conversion
{
    /// <summary>
    /// An interface for a converter object, that is, a class which is used to convert 
    /// object instancess of one type to instances of another type.
    /// </summary>
    /// <typeparam name="TSource">
    /// The type of the object to be converted. 
    /// <typeparam>
    /// <typeparam>
    /// The type of the conversion result. 
    /// </typeparam>
    public interface IConverter<TSource, TTarget>
    {
        /// <summary>
        /// Convert the specified by the <paramref name="source"/> parameter objects to an instance of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <param name="source">
        /// The source instance to be converted.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TTarget" /> which is produced as result of the conversion of the object passed via the <paramref name="source"/> parameter. 
        /// </returns>
        TTarget Convert(TSource source);

        bool TryConvert(TSource source, out TTarget target);
    }
}
