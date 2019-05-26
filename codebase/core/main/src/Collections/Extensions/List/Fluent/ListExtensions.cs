#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;


namespace Axle.Collections.Extensions.List.Fluent
{
    using Array = System.Array;

    /// <summary>
    /// A static class to contain extension methods that allow manipulating <see cref="List{T}"/> collection
    /// instances using the fluent interface pattern.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Adds an item to the <see cref="IList"/>.
        /// </summary>
        /// <typeparam name="TCollection">
        /// The type of the collection this extension method is called against. Must implement the <see cref="IList"/> interface.
        /// </typeparam>
        /// <param name="list">The <see cref="IList"/> instance this method is invoked against.</param>
        /// <param name="item">The object to add to the <see cref="IList"/>. </param>
        /// <returns>the <see cref="IList"/> instance this method is called against.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is <c>null</c>.</exception>
        /// <seealso cref="IList.Add"/>
        public static TCollection FluentAdd<TCollection>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            TCollection list, object item) where TCollection: class, IList
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            list.Add(item);
            return list;
        }

        public static TCollection FluentClear<TCollection>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            TCollection list) where TCollection: class, IList
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            list.Clear();
            return list;
        }

        public static TCollection FluentCopyTo<TCollection>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            TCollection list, Array array, int arrayIndex) where TCollection: class, IList
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            list.CopyTo(array, arrayIndex);
            return list;
        }

        public static TList FluentInsert<TList, T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            TList list, int index, T item) where TList: class, IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            list.Insert(index, item);
            return list;
        }
        public static TList FluentInsert<TList>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            TList list, int index, object item) where TList : class, IList
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            list.Insert(index, item);
            return list;
        }

        public static TCollection FluentRemove<TCollection>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            TCollection list, object item) where TCollection: class, IList
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            list.Remove(item);
            return list;
        }

        public static TList FluentRemoveAt<TList, T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            TList list, int index) where TList : class, IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            list.RemoveAt(index);
            return list;
        }
        public static TList FluentRemoveAt<TList>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            TList list, int index) where TList : class, IList
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            list.RemoveAt(index);
            return list;
        }
    }
}
#endif