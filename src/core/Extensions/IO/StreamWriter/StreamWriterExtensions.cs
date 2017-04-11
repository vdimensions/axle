using System.IO;

using Axle.Verification;


namespace Axle.Extensions.IO.StreamWriter
{
    using StreamWriter = System.IO.StreamWriter;

    /// <summary>
    /// A static class providing extension methods to <see cref="StreamWriter"/> instances.
    /// </summary>
    public static class StreamWriterExtensions
    {
        /// <summary>
        /// Sets the position within the <see cref="StreamWriter.BaseStream">underlying stream</see> of the <see cref="StreamWriter" />.
        /// </summary>
        /// <param name="writer">The <see cref="StreamWriter" /> instance that owns the stream.</param>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter. </param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position. </param>
        /// <returns>
        /// The new position within the <see cref="StreamWriter.BaseStream">underlying stream</see> of the <see cref="StreamWriter" />.
        /// </returns>
        /// <exception cref="IOException">
        /// An I/O error occurs. 
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// The <see cref="StreamWriter.BaseStream">underlying stream</see> of the <see cref="StreamWriter" /> does not support seeking, 
        /// such as if the stream is constructed from a pipe or console output. 
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">
        /// Methods were called after the <see cref="StreamWriter" /> or its <see cref="StreamWriter.BaseStream">underlying stream</see> were closed. 
        /// </exception>
        /// <seealso cref="StreamWriter"/>
        /// <seealso cref="StreamWriter.BaseStream"/>
        /// <seealso cref="SeekOrigin"/>
        public static long Seek(this StreamWriter writer, long offset, SeekOrigin origin)
        {
            return writer.VerifyArgument("writer").IsNotNull().Value.BaseStream.Seek(offset, origin);
        }
    }
}
