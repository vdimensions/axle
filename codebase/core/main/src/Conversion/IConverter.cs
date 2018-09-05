#if NETSTANDARD || NET35_OR_NEWER
namespace Axle.Conversion
{
    /// <summary>
    /// An interface for a converter object, that is, a class which is used to convert 
    /// object instances of one type to instances of another type.
    /// </summary>
    /// <typeparam name="TSource">
    /// The type of the object to be converted. 
    /// </typeparam>
    /// <typeparam name="TTarget">
    /// The type of the conversion result. 
    /// </typeparam>
    public interface IConverter<in TSource, TTarget>
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

        /// <summary>
        /// Attempts to covert the specified by the <paramref name="source"/> parameter instance of the <typeparamref name="TSource" /> 
        /// type to an instance of the <typeparamref name="TTarget"/> type.
        /// </summary>
        /// <param name="source">
        /// The source instance to be converted.
        /// </param>
        /// <param name="target">
        /// An output parameter to contain the conversion result if the conversion succeeds.
        /// </param>
        /// <returns>
        /// <c>true</c> if the conversion was successful; <c>false</c> otherwise.
        /// </returns>
        bool TryConvert(TSource source, out TTarget target);
    }
}
#endif