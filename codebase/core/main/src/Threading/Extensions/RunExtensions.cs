#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Threading;
using System.Threading.Tasks;
using Axle.Verification;

namespace Axle.Threading.Extensions
{
    /// <summary>
    /// A static class providing extension methods enabling delegates to be executed asynchronously in a task.
    /// </summary>
    public static class RunExtensions
    {
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="action">
        /// The work to execute asynchronously
        /// </param>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>.
        /// </exception>
        public static Task Run(this Action action)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(action, nameof(action)));
            #if NET35 || NET40
            return Task.Factory.StartNew(action);
            #else
            return Task.Run(action);
            #endif
        }
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="action">
        /// The work to execute asynchronously
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work
        /// </param>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>.
        /// </exception>
        public static Task Run(this Action action, CancellationToken cancellationToken)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(action, nameof(action)));
            #if NET35 || NET40
            return Task.Factory.StartNew(action, cancellationToken);
            #else
            return Task.Run(action, cancellationToken);
            #endif
        }
    }
}
#endif