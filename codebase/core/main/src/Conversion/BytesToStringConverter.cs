using System.Diagnostics;
using System.Text;

using Axle.Verification;

#if NETSTANDARD1_0
using Axle.Text.Extensions.Encoding;
#endif


namespace Axle.Conversion
{
    /// <summary>
    /// A converter class that can turn a byte sequence to a <see cref="string">string</see> representation, using a
    /// specified <see cref="System.Text.Encoding"/>
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    public sealed class BytesToStringConverter : AbstractTwoWayConverter<byte[], string>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Encoding _encoding;

        /// <summary>
        /// Creates a new <see cref="BytesToStringConverter" /> instance using the specified
        /// <paramref name="encoding" /> parameter.
        /// </summary>
        /// <param name="encoding">
        /// The <see cref="System.Text.Encoding">encoding</see> that is used for the conversion.
        /// </param>
        public BytesToStringConverter(Encoding encoding)
        {
            _encoding = Verifier.IsNotNull(Verifier.VerifyArgument(encoding, nameof(encoding)));
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        /// <summary>
        /// Creates a new <see cref="BytesToStringConverter" /> instance using the
        /// <see cref="System.Text.Encoding.Default">default encoding</see>
        /// </summary>
        public BytesToStringConverter() : this(Encoding.Default) { }
        #else
        /// <summary>
        /// Creates a new <see cref="BytesToStringConverter" /> instance using the
        /// <see cref="System.Text.Encoding.UTF8">UTF8 encoding</see>
        /// </summary>
        public BytesToStringConverter() : this(Encoding.UTF8) { }
        #endif

        /// <inheritdoc />
        protected override string DoConvert(byte[] source) => 
            _encoding.GetString(Verifier.IsNotNull(Verifier.VerifyArgument(source, nameof(source))));

        /// <inheritdoc />
        protected override byte[] DoConvertBack(string source) => 
            _encoding.GetBytes(Verifier.IsNotNull(Verifier.VerifyArgument(source, nameof(source))));

        /// <summary>
        /// Gets the <see cref="System.Text.Encoding" /> instance used to convert string instances to bytes and
        /// vice-versa.
        /// </summary>
        public Encoding Encoding => _encoding;
    }
}
