#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Axle.Verification;

namespace Axle.Threading.Extensions
{
    /// <summary>
    /// A static class providing extension methods enabling delegates to be executed asynchronously in a task.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class AsyncExtensions
    {
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="action">
        /// The work to execute asynchronously.
        /// </param>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>.
        /// </exception>
        public static Task InvokeAsync(this Action action)
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
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work.
        /// </param>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>.
        /// </exception>
        public static Task InvokeAsync(this Action action, CancellationToken cancellationToken)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(action, nameof(action)));
            #if NET35 || NET40
            return Task.Factory.StartNew(action, cancellationToken);
            #else
            return Task.Run(action, cancellationToken);
            #endif
        }
        
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="action">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="arg">
        /// The argument to pass to the <paramref name="action"/>.
        /// </param>
        /// <typeparam name="T">
        /// The type of the argument to pass to the action.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>.
        /// </exception>
        public static Task InvokeAsync<T>(this Action<T> action, T arg)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(action, nameof(action)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                () => action(arg)
            );
        }
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="action">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="arg">
        /// The argument to pass to the <paramref name="action"/>.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work.
        /// </param>
        /// <typeparam name="T">
        /// The type of the argument to pass to the action.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>.
        /// </exception>
        public static Task InvokeAsync<T>(this Action<T> action, CancellationToken cancellationToken, T arg)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(action, nameof(action)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                () => action(arg),
                cancellationToken
            );
        }
        
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="action">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="arg1">
        /// The first argument to pass to the <paramref name="action"/>.
        /// </param>
        /// <param name="arg2">
        /// The second argument to pass to the <paramref name="action"/>.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument to pass to the action.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to pass to the action.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>.
        /// </exception>
        public static Task InvokeAsync<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(action, nameof(action)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                () => action(arg1, arg2)
            );
        }
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="action">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="arg1">
        /// The first argument to pass to the <paramref name="action"/>.
        /// </param>
        /// <param name="arg2">
        /// The second argument to pass to the <paramref name="action"/>.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument to pass to the action.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to pass to the action.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>.
        /// </exception>
        public static Task InvokeAsync<T1, T2>(this Action<T1, T2> action, CancellationToken cancellationToken, T1 arg1, T2 arg2)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(action, nameof(action)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                () => action(arg1, arg2),
                cancellationToken
            );
        }
        
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="action">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="arg1">
        /// The first argument to pass to the <paramref name="action"/>.
        /// </param>
        /// <param name="arg2">
        /// The second argument to pass to the <paramref name="action"/>.
        /// </param>
        /// <param name="arg3">
        /// The third argument to pass to the <paramref name="action"/>.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument to pass to the action.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to pass to the action.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument to pass to the action.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>.
        /// </exception>
        public static Task InvokeAsync<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(action, nameof(action)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                () => action(arg1, arg2, arg3)
            );
        }
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="action">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="arg1">
        /// The first argument to pass to the <paramref name="action"/>.
        /// </param>
        /// <param name="arg2">
        /// The second argument to pass to the <paramref name="action"/>.
        /// </param>
        /// <param name="arg3">
        /// The third argument to pass to the <paramref name="action"/>.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument to pass to the action.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to pass to the action.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument to pass to the action.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>.
        /// </exception>
        public static Task InvokeAsync<T1, T2, T3>(this Action<T1, T2, T3> action, CancellationToken cancellationToken, T1 arg1, T2 arg2, T3 arg3)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(action, nameof(action)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                () => action(arg1, arg2, arg3),
                cancellationToken
            );
        }
        
        
        
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="func">
        /// The work to execute asynchronously.
        /// </param>
        /// <typeparam name="TResult">
        /// The result produced from the queued work.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>.
        /// </exception>
        public static Task<TResult> InvokeAsync<TResult>(this Func<TResult> func)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(func, nameof(func)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                func
            );
        }
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="func">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work.
        /// </param>
        /// <typeparam name="TResult">
        /// The result produced from the queued work.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>.
        /// </exception>
        public static Task<TResult> InvokeAsync<TResult>(this Func<TResult> func, CancellationToken cancellationToken)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(func, nameof(func)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                func, 
                cancellationToken
            );
        }
        
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="func">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="arg">
        /// The argument to pass to the <paramref name="func"/>.
        /// </param>
        /// <typeparam name="T">
        /// The type of the argument to pass to the <paramref name="func"/>.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The result produced from the queued work.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>.
        /// </exception>
        public static Task<TResult> InvokeAsync<T, TResult>(this Func<T, TResult> func, T arg)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(func, nameof(func)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                () => func(arg)
            );
        }
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="func">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="arg">
        /// The argument to pass to the <paramref name="func"/>.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work.
        /// </param>
        /// <typeparam name="T">
        /// The type of the argument to pass to the <paramref name="func"/>.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The result produced from the queued work.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>.
        /// </exception>
        public static Task<TResult> InvokeAsync<T, TResult>(this Func<T, TResult> func, CancellationToken cancellationToken, T arg)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(func, nameof(func)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                () => func(arg), 
                cancellationToken
            );
        }
        
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="func">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="arg1">
        /// The first argument to pass to the <paramref name="func"/>.
        /// </param>
        /// <param name="arg2">
        /// The second argument to pass to the <paramref name="func"/>.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument to pass to the <paramref name="func"/>.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to pass to the <paramref name="func"/>.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The result produced from the queued work.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>.
        /// </exception>
        public static Task<TResult> InvokeAsync<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(func, nameof(func)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                () => func(arg1, arg2)
            );
        }
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="func">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="arg1">
        /// The first argument to pass to the <paramref name="func"/>.
        /// </param>
        /// <param name="arg2">
        /// The second argument to pass to the <paramref name="func"/>.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument to pass to the <paramref name="func"/>.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to pass to the <paramref name="func"/>.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The result produced from the queued work.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>.
        /// </exception>
        public static Task<TResult> InvokeAsync<T1, T2, TResult>(this Func<T1, T2, TResult> func, CancellationToken cancellationToken, T1 arg1, T2 arg2)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(func, nameof(func)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                () => func(arg1, arg2), 
                cancellationToken
            );
        }
        
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="func">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="arg1">
        /// The first argument to pass to the <paramref name="func"/>.
        /// </param>
        /// <param name="arg2">
        /// The second argument to pass to the <paramref name="func"/>.
        /// </param>
        /// <param name="arg3">
        /// The third argument to pass to the <paramref name="func"/>.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument to pass to the <paramref name="func"/>.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to pass to the <paramref name="func"/>.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument to pass to the <paramref name="func"/>.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The result produced from the queued work.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>.
        /// </exception>
        public static Task<TResult> InvokeAsync<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(func, nameof(func)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                () => func(arg1, arg2, arg3)
            );
        }
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a <see cref="Task"/>
        /// object that represents that work.
        /// </summary>
        /// <param name="func">
        /// The work to execute asynchronously.
        /// </param>
        /// <param name="arg1">
        /// The first argument to pass to the <paramref name="func"/>.
        /// </param>
        /// <param name="arg2">
        /// The second argument to pass to the <paramref name="func"/>.
        /// </param>
        /// <param name="arg3">
        /// The third argument to pass to the <paramref name="func"/>.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument to pass to the <paramref name="func"/>.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to pass to the <paramref name="func"/>.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument to pass to the <paramref name="func"/>.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The result produced from the queued work.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task">task</see> that represents the work queued to execute in the ThreadPool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>.
        /// </exception>
        public static Task<TResult> InvokeAsync<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, CancellationToken cancellationToken, T1 arg1, T2 arg2, T3 arg3)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(func, nameof(func)));
            #if NET35 || NET40
            return Task.Factory.StartNew(
            #else
            return Task.Run(
            #endif
                () => func(arg1, arg2, arg3), 
                cancellationToken
            );
        }
    }
}
#endif