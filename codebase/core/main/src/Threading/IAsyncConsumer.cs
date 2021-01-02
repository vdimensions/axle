#if NETSTANDARD1_3_OR_NEWER || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
using System;

namespace Axle.Threading
{
    /// <summary>
    /// An interface representing an async consumer. An async consumer is 
    /// used in conjunction with an instance of <see cref="AsyncProducer{T}"/>
    /// where the consumer's role is to process the data which is pushed to the
    /// <see cref="AsyncProducer{T}"/> asynchronously.
    /// <remarks>
    /// The consumer itself works in synchronous context.
    /// </remarks>
    /// </summary>
    /// <typeparam name="T">
    /// The type of the consumed data.
    /// </typeparam>
    public interface IAsyncConsumer<in T>
    {
        /// <summary>
        /// A method invoked when data supplied by an <see cref="AsyncProducer{T}"/> is available.
        /// </summary>
        /// <param name="data">
        /// An instance of <typeparamref name="T"/> provided by an <see cref="AsyncProducer{T}"/>
        /// </param>
        void Consume(T data);

        /// <summary>
        /// A method invoked when an exception is being thrown while consuming.
        /// </summary>
        /// <param name="e">
        /// The exception which occurred while processing data.
        /// </param>
        /// <param name="data">
        /// The data item that was being processed while the exception was thrown.
        /// </param>
        /// <seealso cref="Consume(T)"/>
        void HandleError(Exception e, T data);

        /// <summary>
        /// A method invoked when the <see cref="AsyncProducer{T}"/> is no longer
        /// receiving data.
        /// </summary>
        void Complete();
    }
}
#endif