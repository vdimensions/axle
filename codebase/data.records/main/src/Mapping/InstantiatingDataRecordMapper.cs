#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
namespace Axle.Data.Records.Mapping
{
    [System.Obsolete]
    public abstract class InstantiatingDataRecordMapper<T> : DataRecordMapper<T> where T: class, new()
    {
        protected override T CreateObject() => new T();
    }
}
#endif