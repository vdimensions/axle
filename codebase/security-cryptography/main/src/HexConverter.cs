using System.IO;
using System.Linq;
using System.Text;

using Axle.Conversion;


namespace Axle.Security.Cryptography
{
    /// <summary>
    /// A class that converts a <see cref="byte">byte</see> array to and from its <see cref="string">string</see> representation.
    /// Each single byte is represented as a hexadecimal string.
    /// </summary>
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class HexConverter : AbstractTwoWayConverter<byte[], string>
    {
        protected override string DoConvert(byte[] source)
        {
            return source.Aggregate(new StringBuilder(2*source.Length), (sb, b) => sb.AppendFormat("{0:X2}", b)).ToString();
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
