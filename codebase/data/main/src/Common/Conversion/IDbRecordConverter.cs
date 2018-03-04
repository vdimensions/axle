using Axle.Conversion;


namespace Axle.Data.Common.Conversion
{
    public interface IDbRecordConverter<T> : IConverter<IDbRecord, T>
    {
        T Convert(IDbRecord value, string fieldNameFormat);
    }
}