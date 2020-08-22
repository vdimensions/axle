using System;


namespace Axle.Caching
{
    public interface ICache
    {
        void Evict();
        
        bool Delete(object key);

        ICache Add(object key, object value);
        ICache Add<T>(object key, T value);

        object GetOrAdd(object key, object valueToAdd);
        object GetOrAdd(object key, Func<object, object> valueFactory);

        object this[object key] { get; set; }
    }
}