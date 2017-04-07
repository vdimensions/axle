using System.IO;

using Axle.Verification;


namespace Axle.Extensions.IO.BinaryReader
{
    using BinaryReader = System.IO.BinaryReader;

    /// <summary>
    /// A static class containing common extension methods to <see cref="BinaryReader"/> instances.
    /// </summary>
    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// Sets the position within the <see cref="BinaryReader.BaseStream">underlying stream</see> of the <see cref="BinaryReader" />.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader" /> instance that owns the stream.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter. </param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position. </param>
        /// <returns>
        /// The new position within the <see cref="BinaryReader.BaseStream">underlying stream</see> of the <see cref="BinaryReader" />.
        /// </returns>
        /// <exception cref="IOException">
        /// An I/O error occurs. 
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// The <see cref="BinaryReader.BaseStream">underlying stream</see> of the <see cref="BinaryReader" /> does not support seeking, 
        /// such as if the stream is constructed from a pipe or console output. 
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">
        /// Methods were called after the <see cref="BinaryReader" /> or its <see cref="BinaryReader.BaseStream">underlying stream</see> were closed. 
        /// </exception>
        /// <seealso cref="BinaryReader"/>
        /// <seealso cref="BinaryReader.BaseStream"/>
        /// <seealso cref="SeekOrigin"/>
        public static long Seek(this BinaryReader reader, long offset, SeekOrigin origin)
        {
            return reader.VerifyArgument("reader").IsNotNull().Value.BaseStream.Seek(offset, origin);
        }
    }
}
