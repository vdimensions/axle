#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
namespace Axle.Data.Records.Mapping
{
    public abstract class InstantiatingDataRecordMapper<T> : DataRecordMapper<T> where T: class, new()
    {
        protected override T Instantiate() => new T();
    }
}
#endif