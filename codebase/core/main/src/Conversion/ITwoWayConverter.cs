#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Conversion
{
    /// <summary>
    /// An interface for converters that allow converting from one type to another and vice-versa.
    /// </summary>
    /// <seealso cref="IConverter{TSource, TTarget}"/>
    public interface ITwoWayConverter<TSource, TTarget> : IConverter<TSource, TTarget>
    {
        /// <summary>
        /// Performs the reverse conversion operation of the <see cref="ITwoWayConverter{TSource,TTarget}.Convert"/> method.
        /// </summary>
        /// <param name="obj">
        /// The <typeparamref name="TTarget"/> instance to be re-converted back to an instance of <typeparamref name="TSource"/>.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TSource"/> which is the result of reverse-converting the <typeparamref name="TTarget"/> instance 
        /// passed via the <paramref name="obj"/> parameter.
        /// </returns>
        TSource ConvertBack(TTarget obj);

        /// <summary>
        /// Gets a <see cref="IConverter{TTarget,TSource}"/> instance, which represents the inverted direction 
        /// of the current <see cref="ITwoWayConverter{TSource,TTarget}"/>. 
        /// <para>
        /// Practically, this instance is an implementation of the <see cref="IConverter{TSource,TTarget}"/> interface.
        /// This method returns a <see cref="IConverter{TTarget,TSource}"/> object (the type argument parameters swapped),  
        /// by using the <see cref="ConvertBack"/> and <see cref="TryConvertBack"/> methods as an implementation to the 
        /// <see cref="IConverter{TTarget,TSource}.Convert"/> and <see cref="IConverter{TTarget,TSource}.TryConvert"/> 
        /// methods of the returned converter respectively.
        /// </para>
        /// </summary>
        /// <returns>
        /// A <see cref="IConverter{TTarget,TSource}"/> instance which represents the inverted direction 
        /// of the current <see cref="ITwoWayConverter{TSource,TTarget}"/>.
        /// </returns>
        IConverter<TTarget, TSource> Invert();

        /// <summary>
        /// Performs the reverse conversion operation of the <see cref="ITwoWayConverter{TSource,TTarget}.TryConvert"/> method.
        /// </summary>
        /// <param name="obj">
        /// The <typeparamref name="TTarget"/> instance to be re-converted back to an instance of <typeparamref name="TSource"/>.
        /// </param>
        /// <param name="result">
        /// An output parameter containing the conversion result, whenever successful.
        /// </param>
        /// <returns>
        /// <c>true</c> if the conversion succeeded; <c>false</c> otherwise.
        /// </returns>
        bool TryConvertBack(TTarget obj, out TSource result);
    }
}
#endif