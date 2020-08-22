using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Axle.Text
{
    /// <summary>
    /// Represents a <see cref="CharSequence"/> implementation, that is backed by a <see cref="char">character</see>
    /// array.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    [StructLayout((LayoutKind.Sequential))]
    public sealed class EnumerableCharSequence : CharSequence
    {
        private readonly char[] _value;

        internal EnumerableCharSequence(IEnumerable<char> value)
        {
            _value = value is char[] c ? c : Enumerable.ToArray(value);
        }
        internal EnumerableCharSequence(char[] value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override string ToString() => new string(_value);

        /// <inheritdoc />
        public override IEnumerator<char> GetEnumerator() => ((IEnumerable<char>) _value).GetEnumerator();

        /// <inheritdoc />
        public override int Length => _value.Length;

        /// <inheritdoc />
        public override char this[int index] => _value[index];
    }
}