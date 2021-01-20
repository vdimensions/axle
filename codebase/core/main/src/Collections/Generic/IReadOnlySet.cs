using System.Collections.Generic;

namespace Axle.Collections.Generic
{
    /// <summary>
    /// Represents a strongly-typed, read-only set of elements.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements.
    /// </typeparam>
    #if NETSTANDARD
    public interface IReadOnlySet<T> : IReadOnlyCollection<T>
    #else
    public interface IReadOnlySet<T> : IEnumerable<T>
    #endif
    {
        /// <summary>Determines whether this read-only set contains a specified element.</summary>
        /// <param name="value">The element to locate in the set.</param>
        /// <returns>
        /// <see langword="true" /> if the set contains the specified value;
        /// otherwise, <see langword="false" />.
        /// </returns>
        bool Contains(T value);

        // TODO: implement for pre-netstandard
        #if NETSTANDARD
        /// <summary>
        /// Determines whether the set contains a specified value.
        /// </summary>
        /// <param name="equalValue">The value to search for.</param>
        /// <param name="actualValue">The matching value from the set, if found, or <paramref name="equalValue"/> if there are no matches.</param>
        /// <returns>
        /// <see langword="true" /> if a matching value was found;
        /// otherwise, <see langword="false" />.
        /// </returns>
        bool TryGetValue(T equalValue, out T actualValue);
        #endif

        /// <summary>Determines whether the current read-only set and the specified collection contain the same elements.</summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        /// <see langword="true" /> if the sets are equal;
        /// otherwise, <see langword="false" />.
        /// </returns>
        bool SetEquals(IEnumerable<T> other);

        /// <summary>Determines whether the current read-only set is a proper (strict) subset of the specified collection.</summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        /// <see langword="true" /> if the current set is a proper subset of the specified collection;
        /// otherwise, <see langword="false" />.
        /// </returns>
        bool IsProperSubsetOf(IEnumerable<T> other);

        /// <summary>Determines whether the current read-only set is a proper (strict) superset of the specified collection.</summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        /// <see langword="true" /> if the current set is a proper superset of the specified collection;
        /// otherwise, false.
        /// </returns>
        bool IsProperSupersetOf(IEnumerable<T> other);

        /// <summary>Determines whether the current read-only set is a subset of a specified collection.</summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        /// <see langword="true" /> if the current set is a subset of the specified collection;
        /// otherwise, <see langword="false" />.
        /// </returns>
        bool IsSubsetOf(IEnumerable<T> other);

        /// <summary>
        /// Determines whether the current read-only hash set is a superset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        /// <see langword="true" /> if the current set is a superset of the specified collection;
        /// otherwise, <see langword="false" />.
        /// </returns>
        bool IsSupersetOf(IEnumerable<T> other);

        /// <summary>Determines whether the current read-only set overlaps with the specified collection.</summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        /// <see langword="true" /> if the current set and the specified collection share at least one common element;
        /// otherwise, <see langword="false" />.
        /// </returns>
        bool Overlaps(IEnumerable<T> other);
        
        /// <summary>
        /// Gets the object that is used to obtain hash codes for the keys and to check the equality of values
        /// in the read-only hash set.
        /// </summary>
        IEqualityComparer<T> Comparer { get; }
    }
}