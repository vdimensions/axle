using System;


namespace Axle.Conversion.Sdk
{

#if !netstandard
    [Serializable]
#endif
    public abstract class AbstractTwoWayConverter<TS, TD> : AbstractConverter<TS, TD>, ITwoWayConverter<TS, TD>
    {
        /// <summary>
        /// An abstract method to contain the actual conversion of an instance of <see cref="TD"><typeparamref name="TD">the destination type</typeparamref></see> 
        /// back to an instance of <see cref="TS"><typeparamref name="TS">the source type</typeparamref></see>.
        /// </summary>
        /// <param name="source">The destination type's value to be converted.</param>
        /// <returns>
        /// An instance of <see cref="TS"><typeparamref name="TS">the source type</typeparamref></see> which is the result of converting the <paramref name="source"/> object.
        /// </returns>
        /// <seealso cref="Convert"/>
        protected abstract TS DoConvertBack(TD source);

        public TS ConvertBack(TD value)
        {
            Exception ex;
            try
            {
                return DoConvertBack(value);
            }
            catch (Exception e)
            {
                ex = e;
            }
            throw ex as ConversionException ?? new ConversionException(typeof(TS), typeof(TD), ex);
        }

        public IConverter<TD, TS> Invert() { return new ReverseConverter<TD, TS>(this); }

        public virtual bool TryConvertBack(TD value, out TS result)
        {
            result = default(TS);
            try
            {
                result = DoConvertBack(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}