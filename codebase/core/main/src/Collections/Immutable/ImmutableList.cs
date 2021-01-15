using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        /// <inheritdoc />
        public IImmutableList<T> Clear()
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.Clear());
            #else
            return ImmutableList.Create<T>();
            #endif
        }

        #if NETSTANDARD
        /// <inheritdoc />
        public int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer) 
            => _impl.IndexOf(item, index, count, equalityComparer);

        /// <inheritdoc />
        public int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer) 
            => _impl.LastIndexOf(item, index, count, equalityComparer);
        #endif

        /// <inheritdoc />
        public IImmutableList<T> Add(T value)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.Add(value));
            #else
            return ImmutableList.CreateRange(new List<T>(_impl) {value});
            #endif
        }

        /// <inheritdoc />
        public IImmutableList<T> AddRange(IEnumerable<T> items)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.AddRange(items));
            #else
            var result = new List<T>(_impl);
            result.AddRange(items);
            return ImmutableList.CreateRange(result);
            #endif
        }

        /// <inheritdoc />
        public IImmutableList<T> Insert(int index, T element)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.Insert(index, element));
            #else
            var result = new List<T>(_impl);
            result.Insert(index, element);
            return ImmutableList.CreateRange(result);
            #endif
        }

        /// <inheritdoc />
        public IImmutableList<T> InsertRange(int index, IEnumerable<T> items)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.InsertRange(index, items));
            #else
            var result = new List<T>(_impl);
            result.InsertRange(index, items);
            return ImmutableList.CreateRange(result);
            #endif
        }

        #if NETSTANDARD
        /// <inheritdoc />
        public IImmutableList<T> Remove(T value, IEqualityComparer<T> equalityComparer) 
            => ImmutableList.CreateRange(_impl.Remove(value, equalityComparer));
        #endif
        
        /// <inheritdoc />
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

        /// <inheritdoc />
        public IImmutableList<T> RemoveAll(Predicate<T> match)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.RemoveAll(match));
            #else
            var result = new List<T>(_impl);
            result.RemoveAll(match);
            return ImmutableList.CreateRange(result);
            #endif
        }

        #if NETSTANDARD
        /// <inheritdoc />
        public IImmutableList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer) 
            => ImmutableList.CreateRange(_impl.RemoveRange(items, equalityComparer));
        #endif

        /// <inheritdoc />
        public IImmutableList<T> RemoveRange(int index, int count)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.RemoveRange(index, count));
            #else
            var result = new List<T>(_impl);
            result.RemoveRange(index, count);
            return ImmutableList.CreateRange(result);
            #endif
        }

        /// <inheritdoc />
        public IImmutableList<T> RemoveAt(int index)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.RemoveAt(index));
            #else
            var result = new List<T>(_impl);
            result.RemoveAt(index);
            return ImmutableList.CreateRange(result);
            #endif
        }

        /// <inheritdoc />
        public IImmutableList<T> SetItem(int index, T value)
        {
            #if NETSTANDARD
            return ImmutableList.CreateRange(_impl.SetItem(index, value));
            #else
            return ImmutableList.CreateRange(new List<T>(_impl) {[index] = value});
            #endif
        }

        #if NETSTANDARD
        /// <inheritdoc />
        public IImmutableList<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer) 
            => ImmutableList.CreateRange(_impl.Replace(oldValue, newValue, equalityComparer));
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