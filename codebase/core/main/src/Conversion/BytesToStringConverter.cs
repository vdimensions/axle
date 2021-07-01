using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class BytesToStringConverter : AbstractTwoWayConverter<byte[], string>
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        /// <summary>
        /// Gets a reference to a shared <see cref="BytesToStringConverter"/> instance that uses the
        /// <see cref="System.Text.Encoding.Default">default encoding</see>.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly BytesToStringConverter Default = new BytesToStringConverter();
        /// <summary>
        /// Gets a reference to a shared <see cref="BytesToStringConverter"/> instance that uses the
        /// <see cref="System.Text.Encoding.ASCII">ASCII encoding</see>.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly BytesToStringConverter ASCII = new BytesToStringConverter(Encoding.ASCII);
        #endif
        
        /// <summary>
        /// Gets a reference to a shared <see cref="BytesToStringConverter"/> instance that uses the
        /// <see cref="System.Text.Encoding.UTF8">UTF8 encoding</see>.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public static readonly BytesToStringConverter UTF8 = new BytesToStringConverter(Encoding.UTF8);
        #else
        public static readonly BytesToStringConverter UTF8 = new BytesToStringConverter();
        #endif
        
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
