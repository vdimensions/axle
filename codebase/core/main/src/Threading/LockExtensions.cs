using System;
using System.Diagnostics.CodeAnalysis;


namespace Axle.Threading
{
    namespace ReaderWriterLock
    {
        /// <summary>
        /// A class containing extension methods for instances of the <see cref="ILock"/> type.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedType.Global")]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static class LockExtensions
        {
            /// <summary>
            /// Invokes an operation withing the confines of a <see cref="ILock"/> only if a lock is needed.
            /// The need for a lock is determined by first executing <paramref name="isLockNeeded"/> function.
            /// </summary>
            /// <param name="lock">
            /// The current <see cref="ILock"/> object to provide the locking behavior.
            /// </param>
            /// <param name="isLockNeeded">
            /// A <see cref="Func{TResult}"/> returning <see cref="bool"/> that is used to determine if a lock is needed.
            /// </param>
            /// <param name="workAction">
            /// An <see cref="Action"/> delegate that will perform an operation within the confines of a lock.
            /// This action is not invoked in case <paramref name="isLockNeeded"/> returns <c>true</c>.
            /// </param>
            public static void Invoke(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                ILock @lock, Func<bool> isLockNeeded, Action workAction)
            {
                if (!isLockNeeded())
                {
                    return;
                }
                @lock.Enter();
                try
                {
                    if (!isLockNeeded())
                    {
                        return;
                    }
                    workAction();
                }
                finally
                {
                    @lock.Exit();
                }
            }

            /// <summary>
            /// Invokes an operation withing the confines of a <see cref="ILock"/> only if a lock is needed.
            /// The need for a lock is determined by first executing <paramref name="isLockNeeded"/> function.
            /// </summary>
            /// <typeparam name="T">
            /// The type of result to eventually be produced within the confines of a lock.
            /// </typeparam>
            /// <param name="lock">
            /// The current <see cref="ILock"/> object to provide the locking behavior.
            /// </param>
            /// <param name="readFunc">
            /// A <see cref="Func{T}"/> that is used to return the result without entering a lock.
            /// </param>
            /// <param name="isLockNeeded">
            /// A <see cref="Func{T, TResult}"/> returning <see cref="bool"/> that is used to determine if a lock is needed.
            /// </param>
            /// <param name="workFunc">
            /// A <see cref="Func{T}"/> delegate that will produce the result while a lock is being held.
            /// This function is not called in case <paramref name="isLockNeeded"/> returns <c>true</c>.
            /// </param>
            /// <returns>
            /// Either the output of the <paramref name="readFunc"/> or the <paramref name="workFunc"/>'s result, depending on whether a lock was required.
            /// </returns>
            public static T Invoke<T>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                ILock @lock, Func<T> readFunc, Func<T, bool> isLockNeeded, Func<T> workFunc)
            {
                var result = readFunc();
                if (!isLockNeeded(result))
                {
                    return result;
                }

                try
                {
                    @lock.Enter();
                    result = readFunc();
                    if (!isLockNeeded(result))
                    {
                        return result;
                    }

                    result = workFunc();

                    return result;
                }
                finally
                {
                    @lock.Exit();
                }
            }

            /// <summary>
            /// Invokes an operation withing the confines of a <see cref="ILock"/> only if a lock is needed.
            /// The need for a lock is determined by executing <paramref name="readFunc"/> operation within the confines
            /// of a lock.
            /// </summary>
            /// <typeparam name="T">
            /// The type of argument to pass to the read and work functions.
            /// </typeparam>
            /// <typeparam name="TResult">
            /// The type of result to eventually be produced within the confines of a lock.
            /// </typeparam>
            /// <param name="lock">
            /// The current <see cref="ILock"/> object to provide the locking behavior.
            /// </param>
            /// <param name="arg">
            /// The argument for the <paramref name="readFunc"/> operation and the <paramref name="workFunc"/> operation.
            /// </param>
            /// <param name="readFunc">
            /// A <see cref="Attempt{T, TResult}"/> that is used to determine whether the result can be obtained without issuing a lock.
            /// </param>
            /// <param name="workFunc">
            /// A <see cref="Func{T,TResult}"/> delegate that will produce the result while a lock is being held.
            /// This function is not called in case <paramref name="readFunc"/> returns <c>true</c>.
            /// </param>
            /// <returns>
            /// Either the output of the <paramref name="readFunc"/> or the <paramref name="workFunc"/>'s result, depending on whether a lock was required.
            /// </returns>
            public static TResult Invoke<T, TResult>(
                #if NETSTANDARD || NET35_OR_NEWER
                this
                #endif
                ILock @lock, T arg, Attempt<T, TResult> readFunc, Func<T, TResult> workFunc)
            {
                if (readFunc(arg, out var result))
                {
                    return result;
                }

                try
                {
                    @lock.Enter();
                    if (readFunc(arg, out result))
                    {
                        return result;
                    }

                    result = workFunc(arg);
                    return result;
                }
                finally
                {
                    @lock.Exit();
                }
            }
        }
    }
}