#if NETSTANDARD || NET35_OR_NEWER
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
#if NETSTANDARD
using Axle.Collections.ReadOnly;
#else
using System.Linq;
#endif
using Axle.Verification;

namespace Axle.Collections.Immutable
{
    /// <summary>
    /// Provides a set of initialization methods for instances of the <see cref="ImmutableHashSet{T}"/> class.
    /// </summary>
    /// <seealso cref="ImmutableHashSet{T}"/>
    /// <seealso cref="IImmutableHashSet{T}"/>
    public static class ImmutableHashSet
    {
        /// <summary>
        /// Creates an empty immutable hash set.
        /// </summary>
        /// <typeparam name="T">
        /// The type of elements to be stored in the immutable hash set.</typeparam>
        /// <returns>
        /// An empty immutable hash set.
        /// </returns>
        public static ImmutableHashSet<T> Create<T>()
        {
            #if NETSTANDARD
            return new ImmutableHashSet<T>(System.Collections.Immutable.ImmutableHashSet.Create<T>());
            #else
            return new ImmutableHashSet<T>(new HashSet<T>());
            #endif
        }
        /// <summary>
        /// Creates an empty immutable hash set that uses the specified equality <paramref name="comparer"/>.
        /// </summary>
        /// <param name="comparer">
        /// The <see cref="IEqualityComparer{T}"/> instance to use for comparing elements in the set for equality.
        /// </param>
        /// <typeparam name="T">
        /// The type of elements in the immutable hash set.
        /// </typeparam>
        /// <returns>
        /// An empty immutable hash set.
        /// </returns>
        public static ImmutableHashSet<T> Create<T>(IEqualityComparer<T> comparer)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(comparer, nameof(comparer)));
            
            #if NETSTANDARD
            return new ImmutableHashSet<T>(System.Collections.Immutable.ImmutableHashSet.Create(comparer));
            #else
            return new ImmutableHashSet<T>(new HashSet<T>(comparer));
            #endif
        }

        /// <summary>
        /// Creates a new immutable hash set that contains the specified <paramref name="items"/> and uses the specified
        /// equality <paramref name="comparer"/> for the set type.
        /// </summary>
        /// <param name="comparer">
        /// The <see cref="IEqualityComparer{T}"/> instance to use for comparing objects in the set for equality.
        /// </param>
        /// <param name="items">
        /// The items add to the collection before immutability is applied.
        /// </param>
        /// <typeparam name="T">
        /// The type of elements stored in the collection.
        /// </typeparam>
        /// <returns>
        /// The new immutable hash set.
        /// </returns>
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

    /// <summary>
    /// Represents a strongly-typed, immutable, unordered hash set.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements in the immutable hash set.
    /// </typeparam>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ImmutableHashSet<T> : IImmutableHashSet<T>
    {
        /// <summary>
        /// Gets a reference to an empty <see cref="ImmutableHashSet{T}"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
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

        /// <inheritdoc cref="IImmutableHashSet{T}.Clear" />
        public ImmutableHashSet<T> Clear()
        {
            #if NETSTANDARD
            return ImmutableHashSet.CreateRange(Comparer, _impl.Clear());
            #else
            return ImmutableHashSet.Create(Comparer);
            #endif
        }
        IImmutableHashSet<T> IImmutableHashSet<T>.Clear() => Clear();

        /// <inheritdoc />
        public bool Contains(T value) => _impl.Contains(value);

        /// <inheritdoc cref="IImmutableHashSet{T}.Add" />
        public ImmutableHashSet<T> Add(T value)
        {
            #if NETSTANDARD
            return ImmutableHashSet.CreateRange(Comparer, _impl.Add(value));
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

        /// <inheritdoc cref="IImmutableHashSet{T}.Remove" />
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
        /// <inheritdoc cref="IReadOnlySet{T}.TryGetValue" />
        public bool TryGetValue(T equalValue, out T actualValue) => _impl.TryGetValue(equalValue, out actualValue);
        bool IReadOnlySet<T>.TryGetValue(T equalValue, out T actualValue) => TryGetValue(equalValue, out actualValue);
        #endif

        /// <inheritdoc cref="IImmutableHashSet{T}.Intersect" />
        public ImmutableHashSet<T> Intersect(IEnumerable<T> other)
        {
            return ImmutableHashSet.CreateRange(Comparer, _impl.Intersect(other));
        }
        IImmutableHashSet<T> IImmutableHashSet<T>.Intersect(IEnumerable<T> other) => Intersect(other);

        /// <inheritdoc cref="IImmutableHashSet{T}.Except" />
        public ImmutableHashSet<T> Except(IEnumerable<T> other)
        {
            return ImmutableHashSet.CreateRange(Comparer, _impl.Except(other));
        }
        IImmutableHashSet<T> IImmutableHashSet<T>.Except(IEnumerable<T> other) => Except(other);

        #if NETSTANDARD
        /// <inheritdoc cref="IImmutableHashSet{T}.SymmetricExcept" />
        public ImmutableHashSet<T> SymmetricExcept(IEnumerable<T> other)
        {
            return ImmutableHashSet.CreateRange(Comparer, _impl.SymmetricExcept(other));
        }
        IImmutableHashSet<T> IImmutableHashSet<T>.SymmetricExcept(IEnumerable<T> other) => SymmetricExcept(other);
        #endif

        /// <inheritdoc cref="IImmutableHashSet{T}.Union" />
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