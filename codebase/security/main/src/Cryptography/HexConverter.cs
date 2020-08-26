using System.IO;
using System.Linq;
using System.Text;
using Axle.Conversion;

namespace Axle.Security.Cryptography
{
    /// <summary>
    /// A class that converts a <see cref="byte">byte</see> array to and from its <see cref="string">string</see>
    /// representation.
    /// Each single byte is represented as a hexadecimal string.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class HexConverter : AbstractTwoWayConverter<byte[], string>
    {
        private readonly string _format;

        /// <summary>
        /// Initializes a new instance of the <see cref="HexConverter"/> class.
        /// </summary>
        public HexConverter() : this(false) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="HexConverter"/> class.
        /// </summary>
        /// <param name="useUpperCaseOutput">
        /// A boolean value that determines if the produced hexadecimal representations should use upper-case
        /// letters. 
        /// </param>
        public HexConverter(bool useUpperCaseOutput)
        {
            _format = useUpperCaseOutput ? "{0:X2}" : "{0:x2}";
        }
        
        protected override string DoConvert(byte[] source)
        {
            return source.Aggregate(new StringBuilder(2*source.Length), (sb, b) => sb.AppendFormat(_format, b)).ToString();
        }

        protected override byte[] DoConvertBack(string source)
        {
            var bytesCount = source.Length/2;
            var bytes = new byte[bytesCount];
            using (var sr = new StringReader(source))
            {
                for (var i = 0; i < bytesCount; i++)
                {
                    bytes[i] = System.Convert.ToByte(new string(new[] { (char) sr.Read(), (char) sr.Read() }), 16);
                }
            }
            return bytes;
        }
    }
}
