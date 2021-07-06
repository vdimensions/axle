#if NETSTANDARD || NET35_OR_NEWER
using System.IO;

using Axle.Verification;


namespace Axle.IO.Extensions.StreamReader
{
    using StreamReader = System.IO.StreamReader;

    /// <summary>
    /// A static class providing extension methods to <see cref="StreamReader"/> instances.
    /// </summary>
    public static class StreamReaderExtensions
    {
        /// <summary>
        /// Sets the position within the <see cref="StreamReader.BaseStream">underlying stream</see> of the
        /// <see cref="StreamReader" />.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="StreamReader" /> instance that owns the stream. 
        /// </param>
        /// <param name="offset">
        /// A byte offset relative to the <paramref name="origin"/> parameter. 
        /// </param>
        /// <param name="origin">
        /// A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position. 
        /// </param>
        /// <returns>
        /// The new position within the <see cref="StreamReader.BaseStream">underlying stream</see> of the
        /// <see cref="StreamReader" />.
        /// </returns>
        /// <exception cref="IOException">
        /// An I/O error occurs. 
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// The <see cref="StreamReader.BaseStream">underlying stream</see> of the <see cref="StreamReader" /> does not
        /// support seeking, 
        /// such as if the stream is constructed from a pipe or console output. 
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">
        /// Methods were called after the <see cref="StreamReader" /> or its
        /// <see cref="StreamReader.BaseStream">underlying stream</see> were closed. 
        /// </exception>
        /// <seealso cref="StreamReader"/>
        /// <seealso cref="StreamReader.BaseStream"/>
        /// <seealso cref="SeekOrigin"/>
        public static long Seek(this StreamReader reader, long offset, SeekOrigin origin)
        {
            return reader.VerifyArgument(nameof(reader)).IsNotNull().Value.BaseStream.Seek(offset, origin);
        }
    }
}
#endif