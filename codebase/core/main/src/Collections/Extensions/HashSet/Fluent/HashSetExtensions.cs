#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Collections.Generic;


namespace Axle.Collections.Extensions.HashSet.Fluent
{
    /// <summary>
    /// A static class to contain extension methods that allow manipulating <see cref="HashSet{T}"/> collection
    /// instances using the fluent interface pattern.
    /// </summary>
    public static class HashSetExtensions
    {
        /// <summary>
        /// Modifies the current <typeparamref name="TSet"/> object to contain only elements that are present
        /// in that object and in the specified collection.
        /// </summary>
        /// <typeparam name="TSet">The <see cref="HashSet{T}"/> type or any class that inherits from it.</typeparam>
        /// <typeparam name="T">The type of the elements in the set.</typeparam>
        /// <param name="set">
        /// The <typeparamref name="TSet"/> object to modify.
        /// </param>
        /// <param name="other">
        /// The collection to compare to the current <typeparamref name="TSet"/> object.
        /// </param>
        /// <returns>
        /// A reference to the current <typeparamref name="TSet"/> object.
        /// </returns>
        /// <seealso cref="HashSet{T}.IntersectWith"/>
        public static TSet FluentIntersectWith<TSet, T>(this TSet set, IEnumerable<T> other) where TSet: HashSet<T>
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            set.IntersectWith(set);
            return set;
        }

        /// <summary>
        /// Removes all elements in the specified collection from the current <typeparamref name="TSet"/> object.
        /// </summary>
        /// <typeparam name="TSet">The <see cref="HashSet{T}"/> type or any class that inherits from it.</typeparam>
        /// <typeparam name="T">The type of the elements in the set.</typeparam>
        /// <param name="set">
        /// The <typeparamref name="TSet"/> object to modify.
        /// </param>
        /// <param name="other">
        /// The collection of items to remove from the current <typeparamref name="TSet"/> object.
        /// </param>
        /// <returns>
        /// A reference to the current <typeparamref name="TSet"/> object.
        /// </returns>
        /// <seealso cref="HashSet{T}.ExceptWith"/>
        public static TSet FluentExceptWith<TSet, T>(this TSet set, IEnumerable<T> other) where TSet : HashSet<T>
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            set.ExceptWith(set);
            return set;
        }

        /// <summary>
        /// Modifies the current <typeparamref name="TSet"/> object to contain only elements that are
        /// present either in that object or in the specified collection, but not both.
        /// </summary>
        /// <typeparam name="TSet">The <see cref="HashSet{T}"/> type or any class that inherits from it.</typeparam>
        /// <typeparam name="T">The type of the elements in the set.</typeparam>
        /// <param name="set">
        /// The <typeparamref name="TSet"/> object to modify.
        /// </param>
        /// <param name="other">
        /// The collection to compare to the current <typeparamref name="TSet"/> object.
        /// </param>
        /// <returns>
        /// A reference to the current <typeparamref name="TSet"/> object.
        /// </returns>
        /// <seealso cref="HashSet{T}.SymmetricExceptWith"/>
        public static TSet FluentSymmetricExceptWith<TSet, T>(this TSet set, IEnumerable<T> other) where TSet : HashSet<T>
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            set.SymmetricExceptWith(set);
            return set;
        }

        /// <summary>
        /// Modifies the current <typeparamref name="TSet"/> object to contain all elements
        /// that are present in itself, the specified collection, or both.
        /// </summary>
        /// <typeparam name="TSet">The <see cref="HashSet{T}"/> type or any class that inherits from it.</typeparam>
        /// <typeparam name="T">The type of the elements in the set.</typeparam>
        /// <param name="set">
        /// The <typeparamref name="TSet"/> object to modify.
        /// </param>
        /// <param name="other">
        /// The collection to compare to the current <typeparamref name="TSet"/> object.
        /// </param>
        /// <returns>
        /// A reference to the current <typeparamref name="TSet"/> object.
        /// </returns>
        /// <seealso cref="HashSet{T}.UnionWith"/>
        public static TSet FluentUnionWith<TSet, T>(this TSet set, IEnumerable<T> other) where TSet: HashSet<T>
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            set.UnionWith(set);
            return set;
        }
    }
}
#endif