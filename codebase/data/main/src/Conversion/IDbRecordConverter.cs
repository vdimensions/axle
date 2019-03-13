using Axle.Conversion;


namespace Axle.Data.Conversion
{
    /// <summary>
    /// An interface for converter objects that are able to convert
    /// an instance of <see cref="IDbRecord"/> to a target type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object to covert a record from.
    /// </typeparam>
    public interface IDbRecordConverter<T> : IConverter<IDbRecord, T>
    {
        T Convert(IDbRecord value, string fieldNameFormat);
    }
}