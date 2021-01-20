using System;

namespace Axle.Conversion
{
    /// <summary>
    /// An abstract class to serve as a base for converter implementations
    /// </summary>
    /// <typeparam name="TS">
    /// The type of the source value - the one that is to be converted to an instance of <typeparamref name="TD"/>.
    /// </typeparam>
    /// <typeparam name="TD">
    /// The destination type of the conversion.
    /// </typeparam>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class AbstractConverter<TS, TD> : IConverter<TS, TD>
    {
        /// <summary>
        /// An abstract method to contain the actual conversion of an instance of
        /// <typeparamref name="TS">the source type</typeparamref> to an instance of
        /// <typeparamref name="TD">the destination type</typeparamref>.
        /// </summary>
        /// <param name="source">The source value to be converted.</param>
        /// <returns>
        /// An instance of <typeparamref name="TD">the destination type</typeparamref> which is the result of converting
        /// the <paramref name="source"/> object.
        /// </returns>
        /// <seealso cref="Convert"/>
        protected abstract TD DoConvert(TS source);

        /// <summary>
        /// Converts a given <paramref name="value"/> of type <typeparamref name="TS"/> to
        /// a value of type <typeparamref name="TD"/>.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// A value of type <typeparamref name="TD"/> that is the result of converting the given
        /// <paramref name="value"/>.
        /// </returns>
        public TD Convert(TS value)
        {
            try
            {
                return DoConvert(value);
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
        /// Attempts to convert a given <paramref name="value"/> of type <typeparamref name="TS"/> to
        /// a value of type <typeparamref name="TD"/>.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="result">
        /// A value of type <typeparamref name="TD"/> that is the result of converting the given
        /// <paramref name="value"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the conversion was successful, and a value was written to the <paramref name="result"/>
        /// output parameter; <c>false</c> otherwise.
        /// </returns>
        public virtual bool TryConvert(TS value, out TD result)
        {
            try
            {
                result = DoConvert(value);
                return true;
            }
            catch
            {
                result = default(TD);
                return false;
            }
        }
    }
}
