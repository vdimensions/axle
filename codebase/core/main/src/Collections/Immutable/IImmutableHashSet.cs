using System.Collections.Generic;

namespace Axle.Collections.Immutable
{
    /// <summary>
    /// Represents an immutable strongly-typed set of values;
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the immutable set.
    /// </typeparam>
    public interface IImmutableHashSet<T>: Axle.Collections.ReadOnly.IReadOnlySet<T>
    {
        /// <summary>
        /// Retrieves an empty immutable set that has the same sorting and ordering semantics as this instance.
        /// </summary>
        /// <returns>
        /// An empty set that has the same sorting and ordering semantics as this instance.
        /// </returns>
        IImmutableHashSet<T> Clear();

        /// <summary>
        /// Adds the specified element to this immutable set.
        /// </summary>
        /// <param name="value">
        /// The element to add.
        /// </param>
        /// <returns>
        /// A new set with the element added, or this set if the element is already in the set.
        /// </returns>
        IImmutableHashSet<T> Add(T value);

        /// <summary>
        /// Removes the specified element from this immutable set.
        /// </summary>
        /// <param name="value">
        /// The element to remove.
        /// </param>
        /// <returns>
        /// A new set with the specified element removed, or the current set if the element cannot be found in the set.
        /// </returns>
        IImmutableHashSet<T> Remove(T value);

        /// <summary>
        /// Creates an immutable set that contains only elements that exist in this set and the specified set.
        /// </summary>
        /// <param name="other">
        /// The collection to compare to the current <see cref="T:System.Collections.Immutable.IImmutableSet`1" />.
        /// </param>
        /// <returns>
        /// A new immutable set that contains elements that exist in both sets.
        /// </returns>
        IImmutableHashSet<T> Intersect(IEnumerable<T> other);

        /// <summary>
        /// Removes the elements in the specified collection from the current immutable set.
        /// </summary>
        /// <param name="other">
        /// The collection of items to remove from this set.
        /// </param>
        /// <returns>
        /// A new set with the items removed; or the original set if none of the items were in the set.
        /// </returns>
        IImmutableHashSet<T> Except(IEnumerable<T> other);

        // TODO: implement for pre-netstandard as well
        #if NETSTANDARD 
        /// <summary>
        /// Creates an immutable set that contains only elements that are present either in the current set or in the
        /// specified collection, but not both.
        /// </summary>
        /// <param name="other">
        /// The collection to compare to the current set.
        /// </param>
        /// <returns>
        /// A new set that contains the elements that are present only in the current set or in the specified
        /// collection, but not both.
        /// </returns>
        IImmutableHashSet<T> SymmetricExcept(IEnumerable<T> other);
        #endif

        /// <summary>
        /// Creates a new immutable set that contains all elements that are present in either the current set or in the
        /// specified collection.
        /// </summary>
        /// <param name="other">
        /// The collection to add elements from.
        /// </param>
        /// <returns>
        /// A new immutable set with the items added; or the original set if all the items were already in the set.
        /// </returns>
        IImmutableHashSet<T> Union(IEnumerable<T> other);
    }
}