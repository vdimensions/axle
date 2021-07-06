using System.Diagnostics.CodeAnalysis;
using Axle.Conversion;
using Axle.Verification;

namespace Axle.Data.Records.Conversion
{
    /// <summary>
    /// An abstract class to serve as a base for implementing the <see cref="IDataRecordConverter{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object to convert an instance of <see cref="DataRecord"/> to.
    /// </typeparam>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public abstract class DataRecordConverter<T> : AbstractConverter<DataRecord, T>, IDataRecordConverter<T>
    {
        public T Convert(DataRecord value, string fieldNameFormat) 
            => DoConvert(value.VerifyArgument(nameof(value)).IsNotNull().Value, fieldNameFormat);

        protected sealed override T DoConvert(DataRecord source) => DoConvert(source, "{0}");

        protected abstract T DoConvert(DataRecord record, string fieldNameFormat);
    }
}