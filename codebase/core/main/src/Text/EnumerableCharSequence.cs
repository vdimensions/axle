using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Axle.Text
{
    /// <summary>
    /// Represents a <see cref="CharSequence"/> implementation, that is backed by a <see cref="char">character</see>
    /// collection.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    [StructLayout((LayoutKind.Sequential))]
    public sealed class EnumerableCharSequence : CharSequence
    {
        private readonly LinkedList<char> _value;

        internal EnumerableCharSequence(IEnumerable<char> value)
        {
            _value = value is LinkedList<char> ls ? ls : new LinkedList<char>(value);
        }
        internal EnumerableCharSequence(LinkedList<char> value)
        {
            _value = value;
        }
        internal EnumerableCharSequence(char[] value) : this(new LinkedList<char>(value)) { }

        /// <inheritdoc />
        public override string ToString() => new string(_value.ToArray());

        /// <inheritdoc />
        public override IEnumerator<char> GetEnumerator() => _value.GetEnumerator();

        /// <inheritdoc />
        public override int Length => _value.Count;

        /// <inheritdoc />
        public override char this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return _value.Skip(index).Take(1).Single();
            }
        }
    }
}