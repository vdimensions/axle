using System;
using System.Collections.Concurrent;

namespace Axle.Caching
{
    /// <summary>
    /// A simple <see cref="ICache"/> implementation backed by a <see cref="ConcurrentDictionary{TKey,TValue}"/>.
    /// </summary>
    public sealed class SimpleCache : ICache
    {
        private readonly ConcurrentDictionary<object, object> _nodes = new ConcurrentDictionary<object, object>();

        /// <inheritdoc />
        public void Evict() => _nodes.Clear();

        /// <inheritdoc />
        public bool Delete(object key)
        {
            var result = _nodes.TryRemove(key, out _);
            return result;
        }

        /// <inheritdoc />
        public ICache Add(object key, object value)
        {
            _nodes[key] = value;
            return this;
        }

        /// <inheritdoc />
        public object GetOrAdd(object key, object valueToAdd) => _nodes.GetOrAdd(key, valueToAdd);

        /// <inheritdoc />
        public object GetOrAdd(object key, Func<object, object> valueFactory) =>  _nodes.GetOrAdd(key, valueFactory);

        /// <inheritdoc />
        public object this[object key]
        {
            get => _nodes.TryGetValue(key, out var res) ? res : null;
            set => _nodes[key] = value;
        }
    }
}