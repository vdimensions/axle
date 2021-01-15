#if NETSTANDARD || NET35_OR_NEWER
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
#if NETSTANDARD
using Axle.Collections.Generic;
#else
using System.Linq;
#endif
using Axle.Verification;

namespace Axle.Collections.Immutable
{
    public static class ImmutableHashSet
    {
        public static ImmutableHashSet<T> Create<T>()
        {
            #if NETSTANDARD
            return new ImmutableHashSet<T>(System.Collections.Immutable.ImmutableHashSet.Create<T>());
            #else
            return new ImmutableHashSet<T>(new HashSet<T>());
            #endif
        }
        public static ImmutableHashSet<T> Create<T>(IEqualityComparer<T> comparer)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(comparer, nameof(comparer)));
            
            #if NETSTANDARD
            return new ImmutableHashSet<T>(System.Collections.Immutable.ImmutableHashSet.Create<T>(comparer));
            #else
            return new ImmutableHashSet<T>(new HashSet<T>(comparer));
            #endif
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static ImmutableHashSet<T> CreateRange<T>(IEqualityComparer<T> comparer, IEnumerable<T> items)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(comparer, nameof(comparer)));
            Verifier.IsNotNull(Verifier.VerifyArgument(items, nameof(items)));
            
            #if NETSTANDARD
            return new ImmutableHashSet<T>(System.Collections.Immutable.ImmutableHashSet.CreateRange(comparer, items));
            #else
            return new ImmutableHashSet<T>(new HashSet<T>(items, comparer));
            #endif
        }
    }

    public class ImmutableHashSet<T> : IImmutableHashSet<T>
    {
        public static readonly ImmutableHashSet<T> Empty = ImmutableHashSet.Create<T>();
        
        #if NETSTANDARD
        private readonly System.Collections.Immutable.ImmutableHashSet<T> _impl;

        internal ImmutableHashSet(System.Collections.Immutable.ImmutableHashSet<T> impl)
        {
            _impl = impl;
        }
        #else
        private readonly HashSet<T> _impl;

        internal ImmutableHashSet(HashSet<T> impl)
        {
            _impl = impl;
        }
        #endif

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => _impl.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public IImmutableHashSet<T> Clear()
        {
            #if NETSTANDARD
            return ImmutableHashSet.CreateRange(Comparer, _impl.Clear());
            #else
            return ImmutableHashSet.Create(Comparer);
            #endif
        }

        /// <inheritdoc />
        public bool Contains(T value) => _impl.Contains(value);

        /// <inheritdoc />
        public ImmutableHashSet<T> Add(T value)
        {
            #if NETSTANDARD
            return ImmutableHashSet.CreateRange<T>(Comparer, _impl.Add(value));
            #else
            var result = new HashSet<T>(_impl, Comparer);
            if (result.Add(value))
            {
                return new ImmutableHashSet<T>(result);
            }
            return this;
            #endif
        }
        IImmutableHashSet<T> IImmutableHashSet<T>.Add(T value) => Add(value);

        /// <inheritdoc />
        public ImmutableHashSet<T> Remove(T value)
        {
            #if NETSTANDARD
            return ImmutableHashSet.CreateRange(Comparer, _impl.Remove(value));
            #else
            var result = new HashSet<T>(_impl, Comparer);
            if (result.Remove(value))
            {
                return new ImmutableHashSet<T>(result);
            }
            return this;
            #endif
        }
        IImmutableHashSet<T> IImmutableHashSet<T>.Remove(T value) => Remove(value);

        #if NETSTANDARD
        /// <inheritdoc />
        public bool TryGetValue(T equalValue, out T actualValue) => _impl.TryGetValue(equalValue, out actualValue);
        bool IReadOnlySet<T>.TryGetValue(T equalValue, out T actualValue) => TryGetValue(equalValue, out actualValue);
        #endif

        /// <inheritdoc />
        public ImmutableHashSet<T> Intersect(IEnumerable<T> other)
        {
            return ImmutableHashSet.CreateRange(Comparer, _impl.Intersect(other));
        }
        IImmutableHashSet<T> IImmutableHashSet<T>.Intersect(IEnumerable<T> other) => Intersect(other);

        /// <inheritdoc />
        public ImmutableHashSet<T> Except(IEnumerable<T> other)
        {
            return ImmutableHashSet.CreateRange(Comparer, _impl.Except(other));
        }
        IImmutableHashSet<T> IImmutableHashSet<T>.Except(IEnumerable<T> other) => Except(other);

        #if NETSTANDARD
        /// <inheritdoc />
        public ImmutableHashSet<T> SymmetricExcept(IEnumerable<T> other)
        {
            return ImmutableHashSet.CreateRange(Comparer, _impl.SymmetricExcept(other));
        }
        IImmutableHashSet<T> IImmutableHashSet<T>.SymmetricExcept(IEnumerable<T> other) => SymmetricExcept(other);
        #endif

        /// <inheritdoc />
        public ImmutableHashSet<T> Union(IEnumerable<T> other)
        {
            return ImmutableHashSet.CreateRange(Comparer, _impl.Union(other));
        }
        IImmutableHashSet<T> IImmutableHashSet<T>.Union(IEnumerable<T> other) => Union(other);

        /// <inheritdoc />
        public bool SetEquals(IEnumerable<T> other) => _impl.SetEquals(other);

        /// <inheritdoc />
        public bool IsProperSubsetOf(IEnumerable<T> other) => _impl.IsProperSubsetOf(other);

        /// <inheritdoc />
        public bool IsProperSupersetOf(IEnumerable<T> other) => _impl.IsProperSupersetOf(other);

        /// <inheritdoc />
        public bool IsSubsetOf(IEnumerable<T> other) => _impl.IsSubsetOf(other);

        /// <inheritdoc />
        public bool IsSupersetOf(IEnumerable<T> other) => _impl.IsSupersetOf(other);

        /// <inheritdoc />
        public bool Overlaps(IEnumerable<T> other) => _impl.Overlaps(other);

        /// <inheritdoc />
        #if NETSTANDARD
        public IEqualityComparer<T> Comparer => _impl.KeyComparer;
        #else
        public IEqualityComparer<T> Comparer => _impl.Comparer;
        #endif

        #if NETSTANDARD
        /// <inheritdoc />
        #endif
        public int Count => _impl.Count;
    }
}
#endif