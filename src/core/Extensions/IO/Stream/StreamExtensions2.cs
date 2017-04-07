using System;
using System.IO;

using Axle.Verification;


namespace Axle.Extensions.IO.Stream
{
    /// <summary>
    /// A static class containing common extension methods to <see cref="Stream"/> instances.
    /// </summary>
    public static class StreamExtensions2
    {
        private const int DefaultBufferSize = 4096;

        public static long SeekToBeginning(this System.IO.Stream @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }
            var currentPosition = @this.Position;
            @this.Seek(0, SeekOrigin.Begin);
            return currentPosition;
        }

        public static long SeekToEnd(this System.IO.Stream @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }
            var currentPosition = @this.Position;
            @this.Seek(0, SeekOrigin.End);
            return currentPosition;
        }

        public static SubStream[] Split(this System.IO.Stream current, params long[] positions)
        {
            var position = current.Position;
            SubStream[] result;
            var streamLength = current.Length;
            if (positions.Length == 0)
            {
                result = new[] { new SubStream(current, 0, current.Length, true) };
                current.Seek(position, SeekOrigin.Begin);
                return result;
            }
            if (positions[0] > 0)
            {
                var newPositions = new long[positions.Length + 1];
                positions.CopyTo(newPositions, 1);
                newPositions[0] = 0;
                positions = newPositions;
            }
            result = new SubStream[positions.Length];
            for (var i = 1; i < positions.Length; i++)
            {
                if (positions[i-1] >= positions[i])
                {
                    throw new ArgumentException(
                        "The provided positions array must consist of values sorted in ascending order.",
                        "positions");
                }
                if (positions[i] > streamLength)
                {
                    throw new ArgumentException(
                        "The provided positions array contains values greater than the length of the target to split.",
                        "positions");
                }

                result[i-1] = new SubStream(current, positions[i-1], positions[i] - positions[i-1], true);
            }
            var lastOrigin = positions[positions.Length-1];
            result[positions.Length-1] = new SubStream(current, lastOrigin, streamLength - lastOrigin, true);

            return result;
        }
        public static SubStream[] Split(this Mixin.Mixin<System.IO.Stream> @this, params long[] positions) { return Split(@this.Value, positions); }

        public static byte[] ToByteArray(this System.IO.Stream @this, int bufferSize, bool leaveOpen)
        {
            @this.VerifyArgument("this").IsNotNull();

            var result = new byte[@this.Length];
            var position = @this.Position;
            try
            {
                @this.Seek(0, SeekOrigin.Begin);
                var loc = 0;
                do
                {
                    var available = result.Length - loc;
                    var count = available > bufferSize ? bufferSize : available;
                    loc += @this.Read(result, loc, count);
                }
                while (loc < result.Length);
            }
            finally
            {
                if (leaveOpen)
                {
                    @this.Seek(position, SeekOrigin.Begin);
                }
                else
                {
                    @this.Close();
                }
            }
            return result;
        }
        public static byte[] ToByteArray(this System.IO.Stream @this, bool leaveOpen) { return ToByteArray(@this, DefaultBufferSize, leaveOpen); }
        public static byte[] ToByteArray(this System.IO.Stream @this, int bufferSize) { return ToByteArray(@this, bufferSize, false); }
        public static byte[] ToByteArray(this System.IO.Stream @this) { return ToByteArray(@this, DefaultBufferSize, false); }
        public static byte[] ToByteArray(this Mixin.Mixin<System.IO.Stream> @this, int bufferSize, bool leaveOpen)
        {
            return ToByteArray(@this.Value, bufferSize, leaveOpen);
        }
        public static byte[] ToByteArray(this Mixin.Mixin<System.IO.Stream> @this, bool leaveOpen) { return ToByteArray(@this.Value, DefaultBufferSize, leaveOpen); }
        public static byte[] ToByteArray(this Mixin.Mixin<System.IO.Stream> @this, int bufferSize) { return ToByteArray(@this.Value, bufferSize, false); }
        public static byte[] ToByteArray(this Mixin.Mixin<System.IO.Stream> @this) { return ToByteArray(@this.Value, DefaultBufferSize, false); }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static long WriteToUnchecked(System.IO.Stream current, System.IO.Stream target, byte[] buffer)
        {
            var i = current.Read(buffer, 0, buffer.Length);
            long size = i;
            do
            {
                target.Write(buffer, 0, i);
                i = current.Read(buffer, 0, buffer.Length);
                size += i;
            }
            while (i > 0);
            return size;
        }

        /// <summary>
        /// Writes data to an output target using the specified input target and buffer size.
        /// <remarks>
        /// The data is being read from the input target's current position.
        /// </remarks>
        /// </summary>
        /// <param name="current">The target to read data from.</param>
        /// <param name="target">The target to write data to.</param>
        /// <param name="bufferSize">The size of the buffer (byte array) that will be used for writing.</param>
        /// <exception cref="NotSupportedException">
        /// The current input target does not support reading.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// The specified output target does not support writing.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The output target and the input target are the same instance.
        /// </exception>
        public static long WriteTo(this System.IO.Stream current, System.IO.Stream target, int bufferSize)
        {
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException("bufferSize", bufferSize, "Invalid buffer size");
            }
            var buffer = new byte[bufferSize];
            return WriteTo(current, target, buffer);
        }
        public static long WriteTo(this System.IO.Stream current, System.IO.Stream target, byte[] buffer)
        {
            if (!current.CanRead)
            {
                throw new NotSupportedException("The specified input stream does not support reading.");
            }
            if (!target.CanWrite)
            {
                throw new NotSupportedException("The specified output stream does not support writing.");
            }
            if (ReferenceEquals(target, current))
            {
                throw new InvalidOperationException(
                    "The output stream cannot be the same instance as the specified input stream.");
            }
            return WriteToUnchecked(current, target, buffer);
        }
    }
}
