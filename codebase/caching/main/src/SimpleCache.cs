using System;
using System.Collections.Concurrent;

namespace Axle.Caching
{
    public sealed class SimpleCache : ICache
    {
        private readonly ConcurrentDictionary<object, object> _nodes = new ConcurrentDictionary<object, object>();

        public void Evict() => _nodes.Clear();

        public bool Delete(object key)
        {
            var result = _nodes.TryRemove(key, out _);
            return result;
        }

        public ICache Add(object key, object value) { return Add<object>(key, value); }
        public ICache Add<T>(object key, T value)
        {
            _nodes[key] = value;
            return this;
        }

        public object GetOrAdd(object key, object valueToAdd) => _nodes.GetOrAdd(key, valueToAdd);
        public object GetOrAdd(object key, Func<object, object> valueFactory) =>  _nodes.GetOrAdd(key, valueFactory);

        public object this[object key]
        {
            get => _nodes.TryGetValue(key, out var res) ? res : null;
            set => _nodes[key] = value;
        }
    }
}