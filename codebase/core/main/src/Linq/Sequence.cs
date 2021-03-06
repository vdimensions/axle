﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Axle.References;


namespace Axle.Linq
{
    /// <summary>
    /// A struct reperesenting a sequence, that is a collection-like interface that allows executing language-integrated queries upon it.
    /// </summary>
    /// <typeparam name="TE">
    /// The type of the underlying <see cref="IEnumerable{T}"/> represented by this instance.
    /// </typeparam>
    /// <typeparam name="T">
    /// The type of objects in the sequence.
    /// </typeparam>
    public struct Sequence<TE, T> : ISequence<T>, IReference<TE> where TE: class, IEnumerable<T>
    {
        public static readonly Sequence<TE, T> Empty = new Sequence<TE, T>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly TE value;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool isReadOnly;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool isParallel;

        public Sequence(TE value) : this(value, false, false) { }
        public Sequence(TE value, bool isParallel) : this(value, false, isParallel) { }
        public Sequence(TE value, bool isReadOnly, bool isParallel) : this()
        {
            this.value = value;
            this.isReadOnly = isReadOnly;
            this.isParallel = isParallel;
        }

        public void ForEach(Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            foreach (var item in value)
            {
                action(item);
            }
        }

        private IEnumerator<T> DoGetEnumerator() { return (Value ?? new T[0] as IEnumerable<T>).GetEnumerator(); }
        public IEnumerator<T> GetEnumerator() { return DoGetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return DoGetEnumerator(); }

        /// <summary>
        /// Returns an <see cref="Optional{T}">optional</see> containing the first element of the <see cref="Sequence{TE,T}">sequence</see>,
        /// or a reference to <see cref="Optional{T}.Undefined"/> if the sequence has no elements.
        /// </summary>
        /// <returns>
        /// Returns an <see cref="Optional{T}">optional</see> containing the first element of the <see cref="Sequence{TE,T}">sequence</see>,
        /// or a reference to <see cref="Optional{T}.Undefined"/> if the sequence has no elements.
        /// </returns>
        public Optional<T> First()
        {
            foreach (var val in this)
            {
                return Optional.From(val);
            }
            return Optional<T>.Undefined;
        }

        /// <summary>
        /// Returns an <see cref="Optional{T}">optional</see> containing the last element of the <see cref="Sequence{TE,T}">sequence</see>,
        /// or a reference to <see cref="Optional{T}.Undefined"/> if the sequence has no elements.
        /// </summary>
        /// <returns>
        /// Returns an <see cref="Optional{T}">optional</see> containing the last element of the <see cref="Sequence{TE,T}">sequence</see>,
        /// or a reference to <see cref="Optional{T}.Undefined"/> if the sequence has no elements.
        /// </returns>
        public Optional<T> Last() { return Optional.From(this.LastOrDefault()); }

        /// <summary>
        /// Returns an <see cref="Optional{T}">optional</see> containing the only element in the <see cref="Sequence{TE,T}">sequence</see>,
        /// or a reference to <see cref="Optional{T}.Undefined"/> if the sequence has no elements.
        /// </summary>
        /// <returns>
        /// Returns an <see cref="Optional{T}">optional</see> containing the only element in the <see cref="Sequence{TE,T}">sequence</see>,
        /// or a reference to <see cref="Optional{T}.Undefined"/> if the sequence has no elements.
        /// </returns>
        public Optional<T> Single() { return Optional.From(this.SingleOrDefault()); }

        public TE Value => value;
        object IReference.Value => Value;
        public bool IsReadOnly => isReadOnly;
        public bool IsParallel => isParallel;
        public bool IsFixedSize => typeof(TE).IsArray;
        /// <summary>
        /// Gets the number of elements contained in the sequence.
        /// </summary>
        public int Count => value?.Count() ?? 0;

        public static implicit operator TE(Sequence<TE, T> obj) { return obj.Value; }
    }
}