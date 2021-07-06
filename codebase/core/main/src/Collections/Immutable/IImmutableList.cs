using System;
using System.Collections.Generic;

namespace Axle.Collections.Immutable
{
    /// <summary>
    /// Represents an immutable collection of elements that can be accessed by index.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the immutable list.
    /// </typeparam>
    public interface IImmutableList<T> : Axle.Collections.ReadOnly.IReadOnlyList<T>
    {
        /// <summary>
        /// Removes all elements from the immutable list.
        /// </summary>
        /// <returns>
        /// A new list with all elements removed.
        /// </returns>
        IImmutableList<T> Clear();

        /// <summary>
        /// Makes a copy of the list, and adds the specified object to the end of the  copied list.
        /// </summary>
        /// <param name="value">
        /// The object to add to the list.
        /// </param>
        /// <returns>
        /// A new list with the object added.
        /// </returns>
        IImmutableList<T> Add(T value);

        /// <summary>
        /// Makes a copy of the list and adds the specified objects to the end of the copied list.
        /// </summary>
        /// <param name="items">
        /// The objects to add to the list.
        /// </param>
        /// <returns>
        /// A new list with the elements added.
        /// </returns>
        IImmutableList<T> AddRange(IEnumerable<T> items);

        /// <summary>
        /// Inserts the specified element at the specified index in the immutable list.
        /// </summary>
        /// <param name="index">
        /// The zero-based index at which to insert the value.
        /// </param>
        /// <param name="element">
        /// The object to insert.
        /// </param>
        /// <returns>
        /// A new immutable list that includes the specified element.
        /// </returns>
        IImmutableList<T> Insert(int index, T element);

        /// <summary>
        /// Inserts the specified elements at the specified index in the immutable list.
        /// </summary>
        /// <param name="index">
        /// The zero-based index at which the new elements should be inserted.
        /// </param>
        /// <param name="items">
        /// The elements to insert.
        /// </param>
        /// <returns>
        /// A new immutable list that includes the specified elements.
        /// </returns>
        IImmutableList<T> InsertRange(int index, IEnumerable<T> items);

        // TODO: implement for pre-netstandard
        #if NETSTANDARD
        /// <summary>
        /// Removes the first occurrence of a specified object from this immutable list.
        /// </summary>
        /// <param name="value">
        /// The object to remove from the list.
        /// </param>
        /// <param name="equalityComparer">
        /// The equality comparer to use to locate <paramref name="value" />.
        /// </param>
        /// <returns>
        /// A new list with the specified object removed.
        /// </returns>
        IImmutableList<T> Remove(T value, IEqualityComparer<T> equalityComparer);
        #endif
        
        /// <summary>
        /// Removes the first occurrence of the specified object from this immutable list.
        /// </summary>
        /// <param name="value">
        /// The object to remove from the list.
        /// </param>
        /// <returns>
        /// A new list with the specified object removed.
        /// </returns>
        IImmutableList<T> Remove(T value);

        /// <summary>
        /// Removes all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match">
        /// The delegate that defines the conditions of the elements to remove.
        /// </param>
        /// <returns>
        /// A new immutable list with the elements removed.
        /// </returns>
        IImmutableList<T> RemoveAll(Predicate<T> match);

        // TODO: implement for pre-netstandard
        #if NETSTANDARD
        /// <summary>
        /// Removes the specified object from the list.
        /// </summary>
        /// <param name="items">
        /// The objects to remove from the list.
        /// </param>
        /// <param name="equalityComparer">
        /// The equality comparer to use to determine if <paramref name="items" /> match any objects in the list.
        /// </param>
        /// <returns>
        /// A new immutable list with the specified objects removed, if <paramref name="items" /> matched objects in
        /// the list.
        /// </returns>
        IImmutableList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer);
        #endif
        
        /// <summary>
        /// Removes a range of elements from the <see cref="T:System.Collections.Immutable.IImmutableList`1" />.
        /// </summary>
        /// <param name="index">
        /// The zero-based starting index of the range of elements to remove.
        /// </param>
        /// <param name="count">
        /// The number of elements to remove.
        /// </param>
        /// <returns>
        /// A new immutable list with the elements removed.
        /// </returns>
        IImmutableList<T> RemoveRange(int index, int count);

        /// <summary>
        /// Removes the element at the specified index of the immutable list.
        /// </summary>
        /// <param name="index">
        /// The index of the element to remove.
        /// </param>
        /// <returns>
        /// A new list with the element removed.
        /// </returns>
        IImmutableList<T> RemoveAt(int index);
        
        /// <summary>
        /// Reverses the order of the elements in the entire immutable list.
        /// </summary>
        /// <returns>
        /// The reversed list.
        /// </returns>
        IImmutableList<T> Reverse();

        /// <summary>
        /// Replaces an element in the list at a given position with the specified element.
        /// </summary>
        /// <param name="index">
        /// The position in the list of the element to replace.
        /// </param>
        /// <param name="value">
        /// The element to replace the old element with.
        /// </param>
        /// <returns>
        /// A new list that contains the new element, even if the element at the specified location is the same as the
        /// new element.
        /// </returns>
        IImmutableList<T> SetItem(int index, T value);

        // TODO: implement for pre-netstandard
        #if NETSTANDARD
        /// <summary>
        /// Returns a new list with the first matching element in the list replaced with the specified element.
        /// </summary>
        /// <param name="oldValue">
        /// The element to be replaced.
        /// </param>
        /// <param name="newValue">
        /// The element to replace the first occurrence of <paramref name="oldValue" /> with.
        /// </param>
        /// <param name="equalityComparer">
        /// The equality comparer to use for matching <paramref name="oldValue" />.
        /// </param>
        /// <returns>
        /// A new list that contains <paramref name="newValue" />, even if <paramref name="oldValue" /> is the same as
        /// <paramref name="newValue" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="oldValue" /> does not exist in the list.
        /// </exception>
        IImmutableList<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer);

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the range
        /// of elements in the <see cref="T:System.Collections.Immutable.IImmutableList`1" /> that starts at the
        /// specified index and contains the specified number of elements.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="T:System.Collections.Immutable.IImmutableList`1" />.
        /// This value can be null for reference types.
        /// </param>
        /// <param name="index">
        /// The zero-based starting indexes of the search. 0 (zero) is valid in an empty list.
        /// </param>
        /// <param name="count">
        /// The number of elements in the section to search.
        /// </param>
        /// <param name="equalityComparer">
        /// The equality comparer to use to locate <paramref name="item" />.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="item" /> within the range of elements in the
        /// <see cref="T:System.Collections.Immutable.IImmutableList`1" /> that starts at <paramref name="index" /> and
        /// contains <paramref name="count" /> number of elements if found; otherwise -1.
        /// </returns>
        int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer);

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the last occurrence within the range
        /// of elements in the <see cref="T:System.Collections.Immutable.IImmutableList`1" /> that contains the
        /// specified number of elements and ends at the specified index.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the list. The value can be <see langword="null" /> for reference types.
        /// </param>
        /// <param name="index">
        /// The zero-based starting index of the search. 0 (zero) is valid in an empty list.
        /// </param>
        /// <param name="count">
        /// The number of elements in the section to search.
        /// </param>
        /// <param name="equalityComparer">
        /// The equality comparer to match <paramref name="item" />.
        /// </param>
        /// <returns>
        /// Returns <see cref="T:System.Int32" />.
        /// </returns>
        int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer);
        #endif
    }
}