using Axle.Conversion;

namespace Axle.Data.Records.Conversion
{
    /// <summary>
    /// An interface for converter objects that are able to convert
    /// an instance of <see cref="DataRecord"/> to a target type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object to covert a record from.
    /// </typeparam>
    public interface IDataRecordConverter<T> : IConverter<DataRecord, T>
    {
        T Convert(DataRecord value, string fieldNameFormat);
    }
}