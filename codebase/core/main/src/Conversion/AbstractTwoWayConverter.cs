#if NETSTANDARD || NET20_OR_NEWER
using System;


namespace Axle.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class AbstractTwoWayConverter<TS, TD> : AbstractConverter<TS, TD>, ITwoWayConverter<TS, TD>
    {
        /// <summary>
        /// An abstract method to contain the actual conversion of an instance of <typeparamref name="TD">the destination type</typeparamref> 
        /// back to an instance of <typeparamref name="TS">the source type</typeparamref>.
        /// </summary>
        /// <param name="source">The destination type's value to be converted.</param>
        /// <returns>
        /// An instance of <typeparamref name="TS">the source type</typeparamref> which is the result of converting the <paramref name="source"/> object.
        /// </returns>
        /// <seealso cref="Convert"/>
        protected abstract TS DoConvertBack(TD source);

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

        public IConverter<TD, TS> Invert() => new ReverseConverter<TD, TS>(this);

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
#endif