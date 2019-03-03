using Axle.Conversion;
using Axle.Verification;


namespace Axle.Data.Conversion
{
    public abstract class DbRecordConverter<T> : AbstractConverter<IDbRecord, T>, IDbRecordConverter<T>
    {
        public T Convert(IDbRecord value, string fieldNameFormat) =>
            DoConvert(value.VerifyArgument(nameof(value)).IsNotNull().Value, fieldNameFormat);

        protected sealed override T DoConvert(IDbRecord source) =>
            DoConvert(source, "{0}");

        protected abstract T DoConvert(IDbRecord record, string fieldNameFormat);
    }
}