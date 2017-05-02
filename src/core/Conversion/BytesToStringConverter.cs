using System;
using System.Diagnostics;
using System.Text;

using Axle.Conversion.Sdk;
using Axle.Verification;


namespace Axle.Conversion
{
    /// <summary>
    /// A converter class that can turn a byte sequence to a <see cref="string">string</see> representation, using a specified <see cref="Encoding"/>
    /// </summary>
#if !netstandard
    [Serializable]
#endif
    public sealed class BytesToStringConverter : AbstractTwoWayConverter<byte[], string>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Encoding encoding;

        /// <summary>
        /// Creates a new <see cref="BytesToStringConverter" /> instance using the specified <paramref name="encoding">encoding</paramref>
        /// </summary>
        /// <param name="encoding">The encoding that is used for string conversion.</param>
        public BytesToStringConverter(Encoding encoding)
        {
            this.encoding = encoding.VerifyArgument("encoding").IsNotNull();
        }
        /// <summary>
        /// Creates a new <see cref="BytesToStringConverter" /> instance using the <see cref="System.Text.Encoding.Default">default encoding</see>
        /// </summary>
        public BytesToStringConverter() : this(Encoding.Default) { }

        protected override string DoConvert(byte[] source) { return encoding.GetString(source); }

        protected override byte[] DoConvertBack(string source) { return encoding.GetBytes(source); }

        /// <summary>
        /// Gets the <see cref="Encoding" /> instance used to convert string to bytes and vice-versa.
        /// </summary>
        public Encoding Encoding { get { return encoding; } }
    }
}