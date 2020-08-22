using System.Collections.Generic;
using System.Runtime.InteropServices;

#if !NETSTANDARD2_0_OR_NEWER && !NETFRAMEWORK
using System.Linq;
#endif

namespace Axle.Text
{
    /// <summary>
    /// Represents a <see cref="CharSequence"/> implementation that is backed by a <see cref="string"/> instance.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    [StructLayout((LayoutKind.Sequential))]
    public sealed class StringCharSequence : CharSequence
    {
        private readonly string _value;

        internal StringCharSequence(string value)
        {
            _value = value;
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        /// <inheritdoc />
        public override IEnumerator<char> GetEnumerator() => _value.GetEnumerator();
        #else
        /// <inheritdoc />
        public override IEnumerator<char> GetEnumerator() => _value.OfType<char>().GetEnumerator();
        #endif

        /// <inheritdoc />
        public override string ToString() => _value;

        /// <inheritdoc />
        public override int Length => _value.Length;

        /// <inheritdoc />
        public override char this[int index] => _value[index];
    }
}