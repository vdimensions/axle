#if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
using Axle.Verification;


namespace Axle.Text.Extensions.Encoding
{
    /// <summary>
    /// A static class to contain extension methods for the <see cref="System.Text.Encoding"/> type.
    /// </summary>
    public static class EncodingExtensions
    {
        #if NETSTANDARD || UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Decodes a sequence of bytes from the specified byte array into a string.
        /// </summary>
        /// <param name="encoding">
        /// The <see cref="System.Text.Encoding"/> instance this extension method is invoked upon.
        /// </param>
        /// <param name="bytes">
        /// The byte array containing the sequence of bytes to decode. 
        /// </param>
        /// <returns>
        /// A <see cref="string"/> that contains the results of decoding the specified sequence of bytes.
        /// </returns>
        public static string GetString(this System.Text.Encoding encoding, byte[] bytes)
        {
            return encoding.VerifyArgument(nameof(encoding)).IsNotNull().Value
                .GetString(bytes.VerifyArgument(nameof(bytes)).IsNotNull().Value, 0, bytes.Length);
        }
        #endif
    }
}
#endif