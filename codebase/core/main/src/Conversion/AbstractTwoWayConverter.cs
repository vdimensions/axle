using System;

namespace Axle.Conversion
{
    /// <summary>
    /// An abstract class to serve as a basis in implementing the <see cref="ITwoWayConverter{TSource,TTarget}"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TS">The source type of the conversion.</typeparam>.
    /// <typeparam name="TD">The destination type of the conversion</typeparam>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class AbstractTwoWayConverter<TS, TD> : AbstractConverter<TS, TD>, ITwoWayConverter<TS, TD>
    {
        /// <summary>
        /// An abstract method to contain the actual conversion of an instance of
        /// <typeparamref name="TD">the destination type</typeparamref> back to an instance of
        /// <typeparamref name="TS">the source type</typeparamref>.
        /// </summary>
        /// <param name="source">The destination type's value to be converted.</param>
        /// <returns>
        /// An instance of <typeparamref name="TS">the source type</typeparamref> which is the result of converting the
        /// <paramref name="source"/> object.
        /// </returns>
        /// <seealso cref="AbstractConverter{TS,TD}.Convert"/>
        protected abstract TS DoConvertBack(TD source);

        /// <summary>
        /// Converts of an instance of <typeparamref name="TD">the destination type</typeparamref>
        /// to an instance of <typeparamref name="TS">the source type</typeparamref>.
        /// This method does the opposite conversion of the <see cref="AbstractConverter{TS,TD}.Convert"/> method.
        /// </summary>
        /// <param name="value">The source value to be converted.</param>
        /// <returns>
        /// An instance of <typeparamref name="TD">the destination type</typeparamref> which is the result
        /// of converting the <paramref name="value"/> object.
        /// </returns>
        /// <seealso cref="AbstractConverter{TS,TD}.Convert"/>
        public TS ConvertBack(TD value)
        {
            try
            {
                return DoConvertBack(value);
            }
            catch (ConversionException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConversionException(typeof(TS), typeof(TD), e);
            }
        }

        /// <summary>
        /// Returns an instance of <see cref="ReverseConverter{T, T}"/>. This is effectively the same converter
        /// instance exposed under an interface where the <typeparamref name="TS" /> and <typeparamref name="TS" />
        /// have been swapped.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="ReverseConverter{T, T}"/>.
        /// </returns>
        public IConverter<TD, TS> Invert() => new ReverseConverter<TD, TS>(this);

        /// <summary>
        /// Attempts to convert of an instance of <typeparamref name="TD">the destination type</typeparamref>
        /// to an instance of <typeparamref name="TS">the source type</typeparamref>.
        /// This method does the opposite conversion of the <see cref="AbstractConverter{TS,TD}.TryConvert"/> method.
        /// </summary>
        /// <param name="value">
        /// The source value to be converted.
        /// </param>
        /// <param name="result">
        /// A value of type <typeparamref name="TS"/> that is the result of converting the given
        /// <paramref name="value"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the conversion was successful, and a value was written to the <paramref name="result"/>
        /// output parameter;
        /// <c>false</c> otherwise.
        /// </returns>
        /// <seealso cref="AbstractConverter{TS,TD}.TryConvert"/>
        public virtual bool TryConvertBack(TD value, out TS result)
        {
            try
            {
                result = DoConvertBack(value);
                return true;
            }
            catch
            {
                result = default(TS);
                return false;
            }
        }
    }
}
