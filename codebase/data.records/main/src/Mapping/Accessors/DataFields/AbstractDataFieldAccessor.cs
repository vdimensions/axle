#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
namespace Axle.Data.Records.Mapping.Accessors.DataFields
{
    public abstract class AbstractDataFieldAccessor<T> : IDataFieldAccessor<T>
    {
        public virtual T GetValue(DataRecord record, string key) => record.GetValue<T>(key);

        public virtual void SetValue(DataRecord record, string key, T value) => record.SetValue(key, value);
    }
}
#endif