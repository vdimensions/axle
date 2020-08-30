using System;
using System.Collections.Concurrent;
using System.Linq;
using Axle.References;

namespace Axle.Caching
{
    /// <summary>
    /// An implementation of the <see cref="ICache"/> interface that stores its values as <see cref="IWeakReference{T}">
    /// weak references</see>, leveraging the garbage collector of the .NET runtime as the enforcer of the caching
    /// expiration policy.
    /// </summary>
    public sealed class WeakReferenceCache : ICache
    {
        private sealed class WeakReferenceCacheNode
        {
            public object Value { get; internal set; }
        }

        private readonly ConcurrentDictionary<object, WeakRef<WeakReferenceCacheNode>> _nodes 
            = new ConcurrentDictionary<object, WeakRef<WeakReferenceCacheNode>>();

        private void ClearExpired()
        {
            var items = _nodes
                .Select(x =>
                    new // Storing the weak reference value in a temp object here makes sure the GC wont destroy it
                    {
                        x.Key,
                        x.Value.Value,
                        x.Value.IsAlive
                    })
                .Where(x => !x.IsAlive)
                .ToArray();
            foreach (var item in items)
            {
                _nodes.TryRemove(item.Key, out _);
            }
        }

        /// <inheritdoc />
        public void Evict() => _nodes.Clear();

        /// <inheritdoc />
        public bool Delete(object key)
        {
            var result = _nodes.TryRemove(key, out var existing) && existing.IsAlive;
            ClearExpired();
            return result;
        }

        /// <inheritdoc />
        public ICache Add(object key, object value)
        {
            _nodes[key] = new WeakRef<WeakReferenceCacheNode>(new WeakReferenceCacheNode { Value = value });
            return this;
        }

        /// <inheritdoc />
        public object GetOrAdd(object key, object valueToAdd) { return GetOrAdd<object>(key, valueToAdd); }

        /// <inheritdoc />
        public object GetOrAdd(object key, Func<object, object> valueFactory) { return GetOrAdd<object>(key, valueFactory); }
        
        private T GetOrAdd<T>(object key, T valueToAdd) { return GetOrAdd(key, _ => valueToAdd); }
        private T GetOrAdd<T>(object key, Func<object, T> valueFactory)
        {
            ClearExpired();
            WeakReferenceCacheNode resultNode;
            var result = (T) _nodes.AddOrUpdate(
                key,
                k =>
                {
                    var v = new WeakReferenceCacheNode { Value = valueFactory(k)};
                    return new WeakRef<WeakReferenceCacheNode>(resultNode = v);
                },
                (k, existing) =>
                {
                    resultNode = existing.Value;
                    if (existing.IsAlive && (resultNode != null))
                    {
                        return existing;
                    }
                    var v = new WeakReferenceCacheNode { Value = valueFactory(k) };
                    return new WeakRef<WeakReferenceCacheNode>(resultNode = v);
                }).Value.Value;
            return result;
        }

        /// <inheritdoc />
        public object this[object key]
        {
            get => _nodes.TryGetValue(key, out var res) && res.TryGetValue(out var node) ? node.Value : null;
            set
            {
                var node = new WeakReferenceCacheNode { Value = value};
                _nodes[key] = new WeakRef<WeakReferenceCacheNode>(node);
            }
        }
    }
}