using System;


namespace Axle.Threading
{
    namespace ReaderWriterLock
    {
        /// <summary>
        /// A class containing extension methods for instances of the <see cref="IReadWriteLock"/> type.
        /// </summary>
        public static class ReaderWriterLockExtensions
        {
            #region Read(...)
            /// <summary>
            /// Executes the code provided by the <paramref name="func"/> parameter within the confines of a read lock.
            /// </summary>
            /// <typeparam name="T">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a read lock.
            /// </param>
            /// <returns>
            /// The result (of type <typeparamref name="T"/>) from the execution of the provided by the <paramref name="func"/> parameter delegate.
            /// </returns>
            public static T Read<T>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Func<T> func)
            {
                @lock.EnterReadLock();
                try
                {
                    return func();
                }
                finally
                {
                    @lock.ExitReadLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="func"/> parameter within the confines of a read lock.
            /// </summary>
            /// <typeparam name="T">
            /// The type of the parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a read lock.
            /// </param>
            /// <param name="arg">
            /// The parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <returns>
            /// The result (of type <typeparamref name="TResult"/>) from the execution of the provided by the <paramref name="func"/> parameter delegate.
            /// </returns>
            public static TResult Read<T, TResult>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Func<T, TResult> func, T arg)
            {
                @lock.EnterReadLock();
                try
                {
                    return func(arg);
                }
                finally
                {
                    @lock.ExitReadLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="func"/> parameter within the confines of a read lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a read lock.
            /// </param>
            /// <param name="arg1">
            /// The first parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg2">
            /// The second parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <returns>
            /// The result (of type <typeparamref name="TResult"/>) from the execution of the provided by the <paramref name="func"/> parameter delegate.
            /// </returns>
            public static TResult Read<T1, T2, TResult>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
            {
                @lock.EnterReadLock();
                try
                {
                    return func(arg1, arg2);
                }
                finally
                {
                    @lock.ExitReadLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="func"/> parameter within the confines of a read lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T3">
            /// The type of the third parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a read lock.
            /// </param>
            /// <param name="arg1">
            /// The first parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg2">
            /// The second parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg3">
            /// The third parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <returns>
            /// The result (of type <typeparamref name="TResult"/>) from the execution of the provided by the <paramref name="func"/> parameter delegate.
            /// </returns>
            public static TResult Read<T1, T2, T3, TResult>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3)
            {
                @lock.EnterReadLock();
                try
                {
                    return func(arg1, arg2, arg3);
                }
                finally
                {
                    @lock.ExitReadLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="func"/> parameter within the confines of a read lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T3">
            /// The type of the third parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T4">
            /// The type of the fourth parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a read lock.
            /// </param>
            /// <param name="arg1">
            /// The first parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg2">
            /// The second parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg3">
            /// The third parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg4">
            /// The fourth parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <returns>
            /// The result (of type <typeparamref name="TResult"/>) from the execution of the provided by the <paramref name="func"/> parameter delegate.
            /// </returns>
            public static TResult Read<T1, T2, T3, T4, TResult>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                @lock.EnterReadLock();
                try
                {
                    return func(arg1, arg2, arg3, arg4);
                }
                finally
                {
                    @lock.ExitReadLock();
                }
            }
            #endregion Read(...)

            #region Write(...)
            /// <summary>
            /// Executes the code provided by the <paramref name="action"/> parameter within the confines of a write lock.
            /// </summary>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="action">
            /// The action delegate to be executed inside a write lock.
            /// </param>
            public static void Write(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock,  Action action)
            {
                @lock.EnterWriteLock();
                try
                {
                    action();
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="action"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T">
            /// The type of the sole parameter to the <paramref name="action"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="action">
            /// The action delegate to be executed inside a write lock.
            /// </param>
            /// <param name="arg">
            /// The argument to pass to the <paramref name="action"/> delegate upon execution.
            /// </param>
            public static void Write<T>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Action<T> action, T arg)
            {
                @lock.EnterWriteLock();
                try
                {
                    action(arg);
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="action"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="action"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="action"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="action">
            /// The action delegate to be executed inside a write lock.
            /// </param>
            /// <param name="arg1">
            /// The first argument to pass to the <paramref name="action"/> delegate upon execution.
            /// </param>
            /// <param name="arg2">
            /// The second argument to pass to the <paramref name="action"/> delegate upon execution.
            /// </param>
            public static void Write<T1, T2>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Action<T1, T2> action, T1 arg1, T2 arg2)
            {
                @lock.EnterWriteLock();
                try
                {
                    action(arg1, arg2);
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="action"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="action"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="action"/> delegate.
            /// </typeparam>
            /// <typeparam name="T3">
            /// The type of the third parameter to the <paramref name="action"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="action">
            /// The action delegate to be executed inside a write lock.
            /// </param>
            /// <param name="arg1">
            /// The first argument to pass to the <paramref name="action"/> delegate upon execution.
            /// </param>
            /// <param name="arg2">
            /// The second argument to pass to the <paramref name="action"/> delegate upon execution.
            /// </param>
            /// <param name="arg3">
            /// The third argument to pass to the <paramref name="action"/> delegate upon execution.
            /// </param>
            public static void Write<T1, T2, T3>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
            {
                @lock.EnterWriteLock();
                try
                {
                    action(arg1, arg2, arg3);
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="action"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="action"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="action"/> delegate.
            /// </typeparam>
            /// <typeparam name="T3">
            /// The type of the third parameter to the <paramref name="action"/> delegate.
            /// </typeparam>
            /// <typeparam name="T4">
            /// The type of the fourth parameter to the <paramref name="action"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="action">
            /// The action delegate to be executed inside a write lock.
            /// </param>
            /// <param name="arg1">
            /// The first argument to pass to the <paramref name="action"/> delegate upon execution.
            /// </param>
            /// <param name="arg2">
            /// The second argument to pass to the <paramref name="action"/> delegate upon execution.
            /// </param>
            /// <param name="arg3">
            /// The third argument to pass to the <paramref name="action"/> delegate upon execution.
            /// </param>
            /// <param name="arg4">
            /// The fourth argument to pass to the <paramref name="action"/> delegate upon execution.
            /// </param>
            public static void Write<T1, T2, T3, T4>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                @lock.EnterWriteLock();
                try
                {
                    action(arg1, arg2, arg3, arg4);
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="func"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a write lock.
            /// </param>
            /// <returns>
            /// The result (of type <typeparamref name="T"/>) from the execution of the provided by the <paramref name="func"/> parameter delegate.
            /// </returns>
            public static T Write<T>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Func<T> func)
            {
                @lock.EnterWriteLock();
                try
                {
                    return func();
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="func"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T">
            /// The type of the parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a write lock.
            /// </param>
            /// <param name="arg">
            /// The parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <returns>
            /// The result (of type <typeparamref name="TResult"/>) from the execution of the provided by the <paramref name="func"/> parameter delegate.
            /// </returns>
            public static TResult Write<T, TResult>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Func<T, TResult> func, T arg)
            {
                @lock.EnterWriteLock();
                try
                {
                    return func(arg);
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="func"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a write lock.
            /// </param>
            /// <param name="arg1">
            /// The first parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg2">
            /// The second parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <returns>
            /// The result (of type <typeparamref name="TResult"/>) from the execution of the provided by the <paramref name="func"/> parameter delegate.
            /// </returns>
            public static TResult Write<T1, T2, TResult>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
            {
                @lock.EnterWriteLock();
                try
                {
                    return func(arg1, arg2);
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="func"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T3">
            /// The type of the third parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a write lock.
            /// </param>
            /// <param name="arg1">
            /// The first parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg2">
            /// The second parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg3">
            /// The third parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <returns>
            /// The result (of type <typeparamref name="TResult"/>) from the execution of the provided by the <paramref name="func"/> parameter delegate.
            /// </returns>
            public static TResult Write<T1, T2, T3, TResult>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3)
            {
                @lock.EnterWriteLock();
                try
                {
                    return func(arg1, arg2, arg3);
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Executes the code provided by the <paramref name="func"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T3">
            /// The type of the third parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T4">
            /// The type of the fourth parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a write lock.
            /// </param>
            /// <param name="arg1">
            /// The first parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg2">
            /// The second parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg3">
            /// The third parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg4">
            /// The fourth parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <returns>
            /// The result (of type <typeparamref name="TResult"/>) from the execution of the provided by the <paramref name="func"/> parameter delegate.
            /// </returns>
            public static TResult Write<T1, T2, T3, T4, TResult>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                @lock.EnterWriteLock();
                try
                {
                    return func(arg1, arg2, arg3, arg4);
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }
            #endregion Write(...)

            #region TryRead(...)
            /// <summary>
            /// Attempts to execute the code provided by the <paramref name="func"/> parameter within the confines of a read lock.
            /// </summary>
            /// <typeparam name="T">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="millisecondsTimeout">
            /// The number of milliseconds to wait until acquiring the lock, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a read lock.
            /// </param>
            /// <param name="result">
            /// The resulting object from the invocation of the <paramref name="func"/> delegate upon.
            /// </param>
            /// <returns>
            /// <c>true</c> if the lock was successfully acquired within the specified timeout; <c>false</c> otherwise.
            /// </returns>
            public static bool TryRead<T>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, int millisecondsTimeout, Func<T> func, out T result)
            {
                if (!@lock.TryEnterReadLock(millisecondsTimeout))
                {
                    result = default(T);
                    return false;
                }
                try
                {
                    result = func();
                    return true;
                }
                finally
                {
                    @lock.ExitReadLock();
                }
            }

            /// <summary>
            /// Attempts to execute the code provided by the <paramref name="func"/> parameter within the confines of a read lock.
            /// </summary>
            /// <typeparam name="T">
            /// The type of the sole parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="millisecondsTimeout">
            /// The number of milliseconds to wait until acquiring the lock, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a read lock.
            /// </param>
            /// <param name="arg">
            /// The sole parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="result">
            /// The resulting object from the invocation of the <paramref name="func"/> delegate upon.
            /// </param>
            /// <returns>
            /// <c>true</c> if the lock was successfully acquired within the specified timeout; <c>false</c> otherwise.
            /// </returns>
            public static bool TryRead<T, TResult>(this IReadWriteLock @lock, int millisecondsTimeout, Func<T, TResult> func, T arg, out TResult result)
            {
                if (!@lock.TryEnterReadLock(millisecondsTimeout))
                {
                    result = default(TResult);
                    return false;
                }
                try
                {
                    result = func(arg);
                    return true;
                }
                finally
                {
                    @lock.ExitReadLock();
                }
            }

            /// <summary>
            /// Attempts to execute the code provided by the <paramref name="func"/> parameter within the confines of a read lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="millisecondsTimeout">
            /// The number of milliseconds to wait until acquiring the lock, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a read lock.
            /// </param>
            /// <param name="arg1">
            /// The first parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg2">
            /// The second parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="result">
            /// The resulting object from the invocation of the <paramref name="func"/> delegate upon.
            /// </param>
            /// <returns>
            /// <c>true</c> if the lock was successfully acquired within the specified timeout; <c>false</c> otherwise.
            /// </returns>
            public static bool TryRead<T1, T2, TResult>(this IReadWriteLock @lock, int millisecondsTimeout, Func<T1, T2, TResult> func, T1 arg1, T2 arg2, out TResult result)
            {
                if (!@lock.TryEnterReadLock(millisecondsTimeout))
                {
                    result = default(TResult);
                    return false;
                }
                try
                {
                    result = func(arg1, arg2);
                    return true;
                }
                finally
                {
                    @lock.ExitReadLock();
                }
            }

            /// <summary>
            /// Attempts to execute the code provided by the <paramref name="func"/> parameter within the confines of a read lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T3">
            /// The type of the third parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="millisecondsTimeout">
            /// The number of milliseconds to wait until acquiring the lock, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a read lock.
            /// </param>
            /// <param name="arg1">
            /// The first parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg2">
            /// The second parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg3">
            /// The third parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="result">
            /// The resulting object from the invocation of the <paramref name="func"/> delegate upon.
            /// </param>
            /// <returns>
            /// <c>true</c> if the lock was successfully acquired within the specified timeout; <c>false</c> otherwise.
            /// </returns>
            public static bool TryRead<T1, T2, T3, TResult>(
                this IReadWriteLock @lock,
                int millisecondsTimeout,
                Func<T1, T2, T3, TResult> func,
                T1 arg1,
                T2 arg2,
                T3 arg3,
                out TResult result)
            {
                if (!@lock.TryEnterReadLock(millisecondsTimeout))
                {
                    result = default(TResult);
                    return false;
                }
                try
                {
                    result = func(arg1, arg2, arg3);
                    return true;
                }
                finally
                {
                    @lock.ExitReadLock();
                }
            }

            /// <summary>
            /// Attempts to execute the code provided by the <paramref name="func"/> parameter within the confines of a read lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T3">
            /// The type of the third parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T4">
            /// The type of the fourth parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="millisecondsTimeout">
            /// The number of milliseconds to wait until acquiring the lock, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a read lock.
            /// </param>
            /// <param name="arg1">
            /// The first parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg2">
            /// The second parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg3">
            /// The third parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg4">
            /// The fourth parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="result">
            /// The resulting object from the invocation of the <paramref name="func"/> delegate upon.
            /// </param>
            /// <returns>
            /// <c>true</c> if the lock was successfully acquired within the specified timeout; <c>false</c> otherwise.
            /// </returns>
            public static bool TryRead<T1, T2, T3, T4, TResult>(
                this IReadWriteLock @lock,
                int millisecondsTimeout,
                Func<T1, T2, T3, T4, TResult> func,
                T1 arg1,
                T2 arg2,
                T3 arg3,
                T4 arg4,
                out TResult result)
            {
                if (!@lock.TryEnterReadLock(millisecondsTimeout))
                {
                    result = default(TResult);
                    return false;
                }
                try
                {
                    result = func(arg1, arg2, arg3, arg4);
                    return true;
                }
                finally
                {
                    @lock.ExitReadLock();
                }
            }
            #endregion TryRead(...)

            #region TryWrite(...)
            /// <summary>
            /// Attempts to execute the code provided by the <paramref name="func"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="millisecondsTimeout">
            /// The number of milliseconds to wait until acquiring the lock, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a write lock.
            /// </param>
            /// <param name="result">
            /// The resulting object from the invocation of the <paramref name="func"/> delegate upon.
            /// </param>
            /// <returns>
            /// <c>true</c> if the lock was successfully acquired within the specified timeout; <c>false</c> otherwise.
            /// </returns>
            public static bool TryWrite<T>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, int millisecondsTimeout, Func<T> func, out T result)
            {
                if (!@lock.TryEnterWriteLock(millisecondsTimeout))
                {
                    result = default(T);
                    return false;
                }
                try
                {
                    result = func();
                    return true;
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Attempts to execute the code provided by the <paramref name="func"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T">
            /// The type of the sole parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="millisecondsTimeout">
            /// The number of milliseconds to wait until acquiring the lock, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a write lock.
            /// </param>
            /// <param name="arg">
            /// The sole parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="result">
            /// The resulting object from the invocation of the <paramref name="func"/> delegate upon.
            /// </param>
            /// <returns>
            /// <c>true</c> if the lock was successfully acquired within the specified timeout; <c>false</c> otherwise.
            /// </returns>
            public static bool TryWrite<T, TResult>(this IReadWriteLock @lock, int millisecondsTimeout, Func<T, TResult> func, T arg, out TResult result)
            {
                if (!@lock.TryEnterWriteLock(millisecondsTimeout))
                {
                    result = default(TResult);
                    return false;
                }
                try
                {
                    result = func(arg);
                    return true;
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Attempts to execute the code provided by the <paramref name="func"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="millisecondsTimeout">
            /// The number of milliseconds to wait until acquiring the lock, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a write lock.
            /// </param>
            /// <param name="arg1">
            /// The first parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg2">
            /// The second parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="result">
            /// The resulting object from the invocation of the <paramref name="func"/> delegate upon.
            /// </param>
            /// <returns>
            /// <c>true</c> if the lock was successfully acquired within the specified timeout; <c>false</c> otherwise.
            /// </returns>
            public static bool TryWrite<T1, T2, TResult>(this IReadWriteLock @lock, int millisecondsTimeout, Func<T1, T2, TResult> func, T1 arg1, T2 arg2, out TResult result)
            {
                if (!@lock.TryEnterWriteLock(millisecondsTimeout))
                {
                    result = default(TResult);
                    return false;
                }
                try
                {
                    result = func(arg1, arg2);
                    return true;
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Attempts to execute the code provided by the <paramref name="func"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T3">
            /// The type of the third parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="millisecondsTimeout">
            /// The number of milliseconds to wait until acquiring the lock, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a write lock.
            /// </param>
            /// <param name="arg1">
            /// The first parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg2">
            /// The second parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg3">
            /// The third parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="result">
            /// The resulting object from the invocation of the <paramref name="func"/> delegate upon.
            /// </param>
            /// <returns>
            /// <c>true</c> if the lock was successfully acquired within the specified timeout; <c>false</c> otherwise.
            /// </returns>
            public static bool TryWrite<T1, T2, T3, TResult>(
                this IReadWriteLock @lock,
                int millisecondsTimeout,
                Func<T1, T2, T3, TResult> func,
                T1 arg1,
                T2 arg2,
                T3 arg3,
                out TResult result)
            {
                if (!@lock.TryEnterWriteLock(millisecondsTimeout))
                {
                    result = default(TResult);
                    return false;
                }
                try
                {
                    result = func(arg1, arg2, arg3);
                    return true;
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }

            /// <summary>
            /// Attempts to execute the code provided by the <paramref name="func"/> parameter within the confines of a write lock.
            /// </summary>
            /// <typeparam name="T1">
            /// The type of the first parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T2">
            /// The type of the second parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T3">
            /// The type of the third parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="T4">
            /// The type of the fourth parameter to the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The return type of the <paramref name="func"/> delegate.
            /// </typeparam>
            /// <param name="lock">
            /// The <see cref="IReadWriteLock"/> object to provide the synchronization mechanics.
            /// </param>
            /// <param name="millisecondsTimeout">
            /// The number of milliseconds to wait until acquiring the lock, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
            /// </param>
            /// <param name="func">
            /// The function to be executed inside a write lock.
            /// </param>
            /// <param name="arg1">
            /// The first parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg2">
            /// The second parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg3">
            /// The third parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="arg4">
            /// The fourth parameter to pass to the <paramref name="func"/> delegate upon invocation.
            /// </param>
            /// <param name="result">
            /// The resulting object from the invocation of the <paramref name="func"/> delegate upon.
            /// </param>
            /// <returns>
            /// <c>true</c> if the lock was successfully acquired within the specified timeout; <c>false</c> otherwise.
            /// </returns>
            public static bool TryWrite<T1, T2, T3, T4, TResult>(
                this IReadWriteLock @lock,
                int millisecondsTimeout,
                Func<T1, T2, T3, T4, TResult> func,
                T1 arg1,
                T2 arg2,
                T3 arg3,
                T4 arg4,
                out TResult result)
            {
                if (!@lock.TryEnterWriteLock(millisecondsTimeout))
                {
                    result = default(TResult);
                    return false;
                }
                try
                {
                    result = func(arg1, arg2, arg3, arg4);
                    return true;
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }
            #endregion TryWrite(...)

            #region CreateReadLockHandle(...)
            #if NETSTANDARD || NET45_OR_NEWER
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            #endif
            public static ILockHandle CreateReadLockHandle(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock)
            {
                return new ReadLockHandle(@lock);
            }
            #endregion CreateReadLockHandle(...)

            #region CreateUpgradeableReadLockHandle(...)
            #if NETSTANDARD || NET45_OR_NEWER
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            #endif
            public static ILockHandle CreateUpgradeableReadLockHandle(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock) => new UpgradeableReadLockHandle(@lock);
            #endregion CreateUpgradeableReadLockHandle(...)

            #region CreateWriteLockHandle(...)
            #if NETSTANDARD || NET45_OR_NEWER
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            #endif
            public static ILockHandle CreateWriteLockHandle(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock) => new WriteLockHandle(@lock);
            #endregion

            /// <summary>
            /// Invokes an operation withing the confines of a <see cref="IReadWriteLock"/> only if a write lock is needed.
            /// The need for a lock is determined by first executing <paramref name="isLockNeeded"/> function.
            /// </summary>
            /// <param name="lock">
            /// The current <see cref="IReadWriteLock"/> object to provide the locking behavior.
            /// </param>
            /// <param name="isLockNeeded">
            /// A <see cref="Func{TResult}"/> returning <see cref="bool"/> that is used to determine if a write lock is needed.
            /// </param>
            /// <param name="workAction">
            /// An <see cref="Action"/> delegate that will perform an operation within the confines of a write lock.
            /// This action is not invoked in case <paramref name="isLockNeeded"/> returns <c>true</c>.
            /// </param>
            public static void Invoke(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Func<bool> isLockNeeded, Action workAction)
            {
                using (CreateReadLockHandle(@lock))
                {
                    if (!isLockNeeded())
                    {
                        return;
                    }
                }
                using (CreateUpgradeableReadLockHandle(@lock))
                {
                    if (!isLockNeeded())
                    {
                        return;
                    }
                    using (CreateWriteLockHandle(@lock))
                    {
                        workAction();
                    }
                }
            }
            /// <summary>
            /// Invokes an operation withing the confines of a <see cref="IReadWriteLock"/> only if a write lock is needed.
            /// The need for a lock is determined by first executing <paramref name="readFunc"/> function within the confines
            /// of a read lock in an attempt to obtain a result, and then validating that result against a
            /// <paramref name="isLockNeeded"/> function outside of a lock.
            /// </summary>
            /// <typeparam name="T">
            /// The type of result to eventually be produced within the confines of a write lock.
            /// </typeparam>
            /// <param name="lock">
            /// The current <see cref="IReadWriteLock"/> object to provide the locking behavior.
            /// </param>
            /// <param name="readFunc">
            /// A <see cref="Func{T}"/> that is used to return the result without entering a write lock.
            /// </param>
            /// <param name="isLockNeeded">
            /// A <see cref="Func{T, TResult}"/> returning <see cref="bool"/> that is used to determine if a write lock is needed.
            /// </param>
            /// <param name="workFunc">
            /// A <see cref="Func{T}"/> delegate that will produce the result while a write lock is being held.
            /// This function is not called in case <paramref name="isLockNeeded"/> returns <c>true</c>.
            /// </param>
            /// <returns>
            /// Either the output of the <paramref name="readFunc"/> or the <paramref name="workFunc"/>'s result, depending on whether a lock was required.
            /// </returns>
            public static T Invoke<T>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Func<T> readFunc, Func<T, bool> isLockNeeded, Func<T> workFunc)
            {
                using (CreateReadLockHandle(@lock))
                {
                    var result = readFunc();
                    if (!isLockNeeded(result))
                    {
                        return result;
                    }
                }
                using (CreateUpgradeableReadLockHandle(@lock))
                {
                    var result = readFunc();
                    if (!isLockNeeded(result))
                    {
                        return result;
                    }
                    using (CreateWriteLockHandle(@lock))
                    {
                        result = workFunc();
                    }
                    return result;
                }
            }
            /// <summary>
            /// Invokes an operation withing the confines of a <see cref="IReadWriteLock"/> only if a write lock is needed.
            /// The need for a lock is determined by executing <paramref name="readFunc"/> operation within the confines
            /// of a read lock.
            /// </summary>
            /// <typeparam name="T">
            /// The type of result to eventually be produced within the confines of a write lock.
            /// </typeparam>
            /// <param name="lock">
            /// The current <see cref="IReadWriteLock"/> object to provide the locking behavior.
            /// </param>
            /// <param name="readFunc">
            /// A <see cref="Attempt{T}"/> that is used to determine whether the result can be obtained without issuing a write lock.
            /// </param>
            /// <param name="workFunc">
            /// A <see cref="Func{T}"/> delegate that will produce the result while a write lock is being held.
            /// This function is not called in case <paramref name="readFunc"/> returns <c>true</c>.
            /// </param>
            /// <returns>
            /// Either the output of the <paramref name="readFunc"/> or the <paramref name="workFunc"/>'s result, depending on whether a lock was required.
            /// </returns>
            public static T Invoke<T>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, Attempt<T> readFunc, Func<T> workFunc)
            {
                T result;
                using (CreateReadLockHandle(@lock))
                {
                    if (readFunc(out result))
                    {
                        return result;
                    }
                }
                using (CreateUpgradeableReadLockHandle(@lock))
                {
                    if (readFunc(out result))
                    {
                        return result;
                    }
                    using (CreateWriteLockHandle(@lock))
                    {
                        result = workFunc();
                    }
                }
                return result;
            }
            /// <summary>
            /// Invokes an operation withing the confines of a <see cref="IReadWriteLock"/> only if a write lock is needed.
            /// The need for a lock is determined by executing <paramref name="readFunc"/> operation within the confines
            /// of a read lock.
            /// </summary>
            /// <typeparam name="T">
            /// The type of argument to pass to the read and work functions.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The type of result to eventually be produced within the confines of a write lock.
            /// </typeparam>
            /// <param name="lock">
            /// The current <see cref="IReadWriteLock"/> object to provide the locking behavior.
            /// </param>
            /// <param name="arg">
            /// The argument for the <paramref name="readFunc"/> operation and the <paramref name="workFunc"/> operation.
            /// </param>
            /// <param name="readFunc">
            /// A <see cref="Attempt{T, TResult}"/> that is used to determine whether the result can be obtained without issuing a write lock.
            /// </param>
            /// <param name="workFunc">
            /// A <see cref="Func{T,TResult}"/> delegate that will produce the result while a write lock is being held.
            /// This function is not called in case <paramref name="readFunc"/> returns <c>true</c>.
            /// </param>
            /// <returns>
            /// Either the output of the <paramref name="readFunc"/> or the <paramref name="workFunc"/>'s result, depending on whether a lock was required.
            /// </returns>
            public static TResult Invoke<T, TResult>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                IReadWriteLock @lock, T arg, Attempt<T, TResult> readFunc, Func<T, TResult> workFunc)
            {
                TResult result;
                using (CreateReadLockHandle(@lock))
                {
                    if (readFunc(arg, out result))
                    {
                        return result;
                    }
                }
                using (CreateUpgradeableReadLockHandle(@lock))
                {
                    if (readFunc(arg, out result))
                    {
                        return result;
                    }
                    using (CreateWriteLockHandle(@lock))
                    {
                        result = workFunc(arg);
                    }
                }
                return result;
            }
        }
    }
}