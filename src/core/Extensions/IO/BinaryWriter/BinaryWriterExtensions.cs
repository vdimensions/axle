using System.IO;

using Axle.Verification;


namespace Axle.Extensions.IO.BinaryWriter
{
    using BinaryWriter = System.IO.BinaryWriter;

    /// <summary>
    /// A static class providing extension methods to <see cref="BinaryWriter"/> instances.
    /// </summary>
    public static class BinaryWriterExtensions
    {
        /// <summary>
        /// Sets the position within the <see cref="BinaryWriter.BaseStream">underlying stream</see> of the <see cref="BinaryWriter" />.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="System.IO.BinaryWriter" /> instance that owns the stream. 
        /// </param>
        /// <param name="offset">
        /// A byte offset relative to the <paramref name="origin"/> parameter. 
        /// </param>
        /// <param name="origin">
        /// A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position. 
        /// </param>
        /// <returns>
        /// The new position within the <see cref="BinaryWriter.BaseStream">underlying stream</see> of the <see cref="BinaryWriter" />.
        /// </returns>
        /// <exception cref="IOException">
        /// An I/O error occurs. 
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// The <see cref="BinaryWriter.BaseStream">underlying stream</see> of the <see cref="BinaryWriter" /> does not support seeking, 
        /// such as if the stream is constructed from a pipe or console output. 
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">
        /// Methods were called after the <see cref="BinaryWriter" /> or its <see cref="BinaryWriter.BaseStream">underlying stream</see> were closed. 
        /// </exception>
        /// <seealso cref="BinaryWriter"/>
        /// <seealso cref="BinaryWriter.BaseStream"/>
        /// <seealso cref="SeekOrigin"/>
        public static long Seek(this BinaryWriter writer, long offset, SeekOrigin origin)
        {
            return writer.VerifyArgument("writer").IsNotNull().Value.BaseStream.Seek(offset, origin);
        }
    }
}
