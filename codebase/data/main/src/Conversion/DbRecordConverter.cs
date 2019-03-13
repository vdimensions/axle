using Axle.Conversion;
using Axle.Verification;


namespace Axle.Data.Conversion
{
    /// <summary>
    /// An abstract class to serve as a base for implementing the <see cref="IDbRecordConverter{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object to convert an instance of <see cref="IDbRecord"/> to.
    /// </typeparam>
    public abstract class DbRecordConverter<T> : AbstractConverter<IDbRecord, T>, IDbRecordConverter<T>
    {
        public T Convert(IDbRecord value, string fieldNameFormat) =>
            DoConvert(value.VerifyArgument(nameof(value)).IsNotNull().Value, fieldNameFormat);

        protected sealed override T DoConvert(IDbRecord source) => DoConvert(source, "{0}");

        protected abstract T DoConvert(IDbRecord record, string fieldNameFormat);
    }
}