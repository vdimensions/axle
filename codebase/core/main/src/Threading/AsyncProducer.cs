#if NETSTANDARD1_3_OR_NEWER || NET35_OR_NEWER || (UNITY_2018_1_OR_NEWER && (UNITY_EDITOR || !UNITY_WEBGL))
using Axle.Threading.Extensions;
using Axle.Verification;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Axle.Threading
{
    /// <summary>
    /// A producer-consumer queue implementation that allows externalization of the consumer logic.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object representing the data being consumed.
    /// </typeparam>
    public class AsyncProducer<T> : IDisposable
    {
        private readonly BlockingCollection<T> _dataItems = new BlockingCollection<T>(1);
        private readonly Task _workerTask;
        private readonly IAsyncConsumer<T> _consumer;

        /// <summary>
        /// Creates a new instance of the <see cref="AsyncProducer{T}"/> class using the specified
        /// <paramref name="consumer"/>.
        /// </summary>
        /// <param name="consumer">
        /// A <see cref="IAsyncConsumer{T}"/> object to process
        /// the data provided by the current <see cref="AsyncProducer{T}"/> instance.
        /// </param>
        public AsyncProducer(IAsyncConsumer<T> consumer)
        {
            _workerTask = StartWorker();
            _consumer = consumer.VerifyArgument(nameof(consumer)).IsNotNull().Value;
        }

        /// <summary>
        /// Finalizes the current <see cref="AsyncProducer{T}"/> instance.
        /// </summary>
        ~AsyncProducer() => Dispose(false);

        private void DoWork()
        {
            while (!_dataItems.IsCompleted)
            {
                try
                {
                    var data = _dataItems.Take();
                    try
                    {
                        _consumer.Consume(data);
                    }
                    catch (Exception e)
                    {
                        _consumer.HandleError(e, data);
                    }
                }
                catch (InvalidOperationException)
                {
                    // IOE means that Take() was called on a completed collection.
                    // Some other thread can call CompleteAdding after we pass the
                    // IsCompleted check but before we call Take. 
                    // Here, we can simply catch the exception since the 
                    // loop will break on the next iteration.
                }
            }
            _consumer.Complete();
        }

        private Task StartWorker() => new Action(DoWork).InvokeAsync();

        /// <summary>
        /// Adds an <paramref name="item"/> to the queue to be processed by the consumer.
        /// </summary>
        /// <param name="item">
        /// The item to be added for processing.
        /// </param>
        public void Push(T item) => _dataItems.Add(item);

        #region IDisposable Support
        private bool _wasDisposed;

        /// <summary>
        /// Disposes of the current <see cref="AsyncProducer{T}"/> instance.
        /// </summary>
        /// <param name="disposing">
        /// A <see cref="bool"/> value indicating whether the dispose call is being sent by a finalizer, or a manual
        /// dispose call.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_wasDisposed)
            {
                return;
            }
            _wasDisposed = true;
            if (disposing)
            {
                GC.SuppressFinalize(this);
                _dataItems.CompleteAdding();
                _workerTask.Wait();
                (_workerTask as IDisposable)?.Dispose();
            }
        }

        void IDisposable.Dispose() => Dispose(true);
        #endregion
    }
}
#endif