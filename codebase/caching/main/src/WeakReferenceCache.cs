using System;
using System.Collections.Concurrent;
using System.Linq;

using Axle.References;


namespace Axle.Caching
{
    public sealed class WeakReferenceCache : ICache
    {
        private sealed class WeakReferenceCacheNode : IEquatable<object>
        {
            public override bool Equals(object obj)
            {
                return Equals(obj, this.Key);
            }

            public object Key { get; internal set; }
            public object Value { get; internal set; }
        }

        private readonly ConcurrentDictionary<object, WeakRef<WeakReferenceCacheNode>> _nodes = new ConcurrentDictionary<object, WeakRef<WeakReferenceCacheNode>>();

        private void ClearExpired()
        {
            _nodes.Where(x => !x.Value.IsAlive).Select(x => x.Key).ToArray().All(x => _nodes.TryRemove(x, out var _));
        }

        public bool Delete(object key)
        {
            var result = _nodes.TryRemove(key, out var _);
            ClearExpired();
            return result;
        }

        public ICache Add(object key, object value) { return Add<object>(key, value); }
        public ICache Add<T>(object key, T value)
        {
            _nodes[key] = new WeakRef<WeakReferenceCacheNode>(new WeakReferenceCacheNode { Key = key, Value = value });
            return this;
        }

        public object GetOrAdd(object key, object valueToAdd) { return GetOrAdd<object>(key, valueToAdd); }
        public object GetOrAdd(object key, Func<object, object> valueFactory) { return GetOrAdd<object>(key, valueFactory); }
        public T GetOrAdd<T>(object key, T valueToAdd) { return GetOrAdd(key, _ => valueToAdd); }
        public T GetOrAdd<T>(object key, Func<object, T> valueFactory)
        {
            ClearExpired();
            WeakReferenceCacheNode resultNode;
            var result = (T) _nodes.AddOrUpdate(
                key,
                k =>
                {
                    var v = new WeakReferenceCacheNode {Key = key, Value = valueFactory(k)};
                    return new WeakRef<WeakReferenceCacheNode>(resultNode = v);
                },
                (k, existing) =>
                {
                    resultNode = existing.Value;
                    if (existing.IsAlive && (resultNode != null))
                    {
                        return existing;
                    }
                    var v = new WeakReferenceCacheNode { Key = key, Value = valueFactory(k) };
                    return new WeakRef<WeakReferenceCacheNode>(resultNode = v);
                }).Value.Value;
            return result;
        }

        public object this[object key]
        {
            get => _nodes.TryGetValue(key, out var res) && res.TryGetValue(out WeakReferenceCacheNode node) ? node.Value : null;
            set
            {
                var node = new WeakReferenceCacheNode {Key = key, Value = value};
                _nodes[key] = new WeakRef<WeakReferenceCacheNode>(node);
            }
        }
    }
}