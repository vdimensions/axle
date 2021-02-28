using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
#if !NETSTANDARD
using System.Linq;
#endif
using Axle.Verification;

namespace Axle.Collections.Immutable
{
    public static class ImmutableList
    {
        public static ImmutableList<T> Create<T>()
        {
            #if NETSTANDARD
            return new ImmutableList<T>(System.Collections.Immutable.ImmutableList.Create<T>());
            #else
            return new ImmutableList<T>(new List<T>());
            #endif
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static ImmutableList<T> CreateRange<T>(IEnumerable<T> items)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(items, nameof(items)));
            #if NETSTANDARD
            return new ImmutableList<T>(System.Collections.Immutable.ImmutableList.CreateRange<T>(items));
            #else
            return new ImmutableList<T>(new List<T>(items));
            #endif
        }
    }
    
    public class ImmutableList<T> : IImmutableList<T>
    {
        /// <summary>
        /// Gets a reference to an empty <see cref="ImmutableList{T}"/> instance.
        /// </summary>
        public static readonly ImmutableList<T> Empty = ImmutableList.Create<T>();
        
        #if NETSTANDARD
        private readonly System.Collections.Immutable.ImmutableList<T> _impl;

        internal ImmutableList(System.Collections.Immutable.ImmutableList<T> impl)
        {
            _impl = impl;
        }
        #else
        private readonly List<T> _impl;

        internal ImmutableList(List<T> impl)
        {
            _impl = impl;
        }
        #endif

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => _impl.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _impl).GetEnumerator();

        /// <inheritdoc cref="IImmutableList{T}.Clear"/>
        public ImmutableList<T> Clear()
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.Clear());
            #else
            return ImmutableList.Create<T>();
            #endif
        }
        IImmutableList<T> IImmutableList<T>.Clear() => Clear();

        #if NETSTANDARD
        /// <inheritdoc />
        public int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer) 
            => _impl.IndexOf(item, index, count, equalityComparer);

        /// <inheritdoc />
        public int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer) 
            => _impl.LastIndexOf(item, index, count, equalityComparer);
        #endif

        /// <inheritdoc cref="IImmutableList{T}.Add"/>
        public ImmutableList<T> Add(T value)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.Add(value));
            #else
            return ImmutableList.CreateRange(new List<T>(_impl) {value});
            #endif
        }
        IImmutableList<T> IImmutableList<T>.Add(T value) => Add(value);

        /// <inheritdoc cref="IImmutableList{T}.AddRange"/>
        public ImmutableList<T> AddRange(IEnumerable<T> items)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.AddRange(items));
            #else
            var result = new List<T>(_impl);
            result.AddRange(items);
            return ImmutableList.CreateRange(result);
            #endif
        }
        IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items) => AddRange(items);

        /// <inheritdoc cref="IImmutableList{T}.Insert"/>
        public ImmutableList<T> Insert(int index, T element)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.Insert(index, element));
            #else
            var result = new List<T>(_impl);
            result.Insert(index, element);
            return ImmutableList.CreateRange(result);
            #endif
        }
        IImmutableList<T> IImmutableList<T>.Insert(int index, T element) => Insert(index, element);

        /// <inheritdoc cref="IImmutableList{T}.InsertRange"/>
        public ImmutableList<T> InsertRange(int index, IEnumerable<T> items)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.InsertRange(index, items));
            #else
            var result = new List<T>(_impl);
            result.InsertRange(index, items);
            return ImmutableList.CreateRange(result);
            #endif
        }
        IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items) => InsertRange(index, items);

        #if NETSTANDARD
        /// <inheritdoc cref="IImmutableList{T}.Remove(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public ImmutableList<T> Remove(T value, IEqualityComparer<T> equalityComparer) 
            => ImmutableList.CreateRange(_impl.Remove(value, equalityComparer));
        IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T> equalityComparer) => Remove(value, equalityComparer);
        #endif
        
        /// <inheritdoc cref="IImmutableList{T}.Remove(T)"/>
        public ImmutableList<T> Remove(T value)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.Remove(value));
            #else
            var result = new List<T>(_impl);
            result.Remove(value);
            return ImmutableList.CreateRange(result);
            #endif
        }
        IImmutableList<T> IImmutableList<T>.Remove(T value) => Remove(value);

        /// <inheritdoc cref="IImmutableList{T}.RemoveAll"/>
        public ImmutableList<T> RemoveAll(Predicate<T> match)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.RemoveAll(match));
            #else
            var result = new List<T>(_impl);
            result.RemoveAll(match);
            return ImmutableList.CreateRange(result);
            #endif
        }
        IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match) => RemoveAll(match);

        #if NETSTANDARD
        /// <inheritdoc cref="IImmutableList{T}.RemoveRange(System.Collections.Generic.IEnumerable{T},System.Collections.Generic.IEqualityComparer{T})"/>
        public ImmutableList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer) 
            => ImmutableList.CreateRange(_impl.RemoveRange(items, equalityComparer));
        IImmutableList<T> IImmutableList<T>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer) 
            => RemoveRange(items, equalityComparer);
        #endif

        /// <inheritdoc cref="IImmutableList{T}.RemoveRange(System.Int32,System.Int32)"/>
        public ImmutableList<T> RemoveRange(int index, int count)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.RemoveRange(index, count));
            #else
            var result = new List<T>(_impl);
            result.RemoveRange(index, count);
            return ImmutableList.CreateRange(result);
            #endif
        }
        IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count) => RemoveRange(index, count);

        /// <inheritdoc cref="IImmutableList{T}.RemoveAt" />
        public ImmutableList<T> RemoveAt(int index)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.RemoveAt(index));
            #else
            var result = new List<T>(_impl);
            result.RemoveAt(index);
            return ImmutableList.CreateRange(result);
            #endif
        }
        IImmutableList<T> IImmutableList<T>.RemoveAt(int index) => RemoveAt(index);
        
        /// <inheritdoc cref="IImmutableList{T}.Reverse" />
        public ImmutableList<T> Reverse()
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.Reverse());
            #else
            return ImmutableList.CreateRange(Enumerable.Reverse(_impl));
            #endif
        }
        IImmutableList<T> IImmutableList<T>.Reverse() => Reverse();

        /// <inheritdoc cref="IImmutableList{T}.SetItem" />
        public ImmutableList<T> SetItem(int index, T value)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.SetItem(index, value));
            #else
            return ImmutableList.CreateRange(new List<T>(_impl) {[index] = value});
            #endif
        }
        IImmutableList<T> IImmutableList<T>.SetItem(int index, T value) => SetItem(index, value);

        #if NETSTANDARD
        /// <inheritdoc cref="IImmutableList{T}.Replace"/>
        public ImmutableList<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer) 
            => ImmutableList.CreateRange(_impl.Replace(oldValue, newValue, equalityComparer));
        IImmutableList<T> IImmutableList<T>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer) 
            => Replace(oldValue, newValue, equalityComparer);
        #endif

        #if NETSTANDARD
        /// <inheritdoc />
        #endif
        public int Count => _impl.Count;

        /// <summary>Gets the element at the specified index in the read-only list.</summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The element at the specified index in the read-only list.</returns>
        public T this[int index] => _impl[index];
    }
}