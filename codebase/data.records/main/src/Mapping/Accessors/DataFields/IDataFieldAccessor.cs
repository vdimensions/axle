#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
namespace Axle.Data.Records.Mapping.Accessors.DataFields
{
    public interface IDataFieldAccessor<T>
    {
        T GetValue(DataRecord record, string key);
        
        void SetValue(DataRecord record, string key, T value);
    }
}
#endif