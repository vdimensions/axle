using System;


namespace Axle.Threading
{
    namespace ReaderWriterLock
    {
        public static class ReaderWriterLockExtensions
        {
            #region Read
            public static T Read<T>(this IReadWriteLock @this, Func<T> func)
            {
                @this.EnterReadLock();
                try
                {
                    return func();
                }
                finally
                {
                    @this.ExitReadLock();
                }
            }
            public static TResult Read<T, TResult>(this IReadWriteLock @this, Func<T, TResult> func, T arg)
            {
                @this.EnterReadLock();
                try
                {
                    return func(arg);
                }
                finally
                {
                    @this.ExitReadLock();
                }
            }
            public static TResult Read<T1, T2, TResult>(this IReadWriteLock @this, Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
            {
                @this.EnterReadLock();
                try
                {
                    return func(arg1, arg2);
                }
                finally
                {
                    @this.ExitReadLock();
                }
            }
            public static TResult Read<T1, T2, T3, TResult>(this IReadWriteLock @this, Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3)
            {
                @this.EnterReadLock();
                try
                {
                    return func(arg1, arg2, arg3);
                }
                finally
                {
                    @this.ExitReadLock();
                }
            }
            public static TResult Read<T1, T2, T3, T4, TResult>(this IReadWriteLock @this, Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                @this.EnterReadLock();
                try
                {
                    return func(arg1, arg2, arg3, arg4);
                }
                finally
                {
                    @this.ExitReadLock();
                }
            }
            #endregion

            #region Write
            public static void Write(this IReadWriteLock @this,  Action action)
            {
                @this.EnterWriteLock();
                try
                {
                    action();
                }
                finally
                {
                    @this.ExitWriteLock();
                }
            }
            public static void Write<T>(this IReadWriteLock @this, Action<T> action, T arg)
            {
                @this.EnterWriteLock();
                try
                {
                    action(arg);
                }
                finally
                {
                    @this.ExitWriteLock();
                }
            }
            public static void Write<T1, T2>(this IReadWriteLock @this, Action<T1, T2> action, T1 arg1, T2 arg2)
            {
                @this.EnterWriteLock();
                try
                {
                    action(arg1, arg2);
                }
                finally
                {
                    @this.ExitWriteLock();
                }
            }
            public static void Write<T1, T2, T3>(this IReadWriteLock @this, Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
            {
                @this.EnterWriteLock();
                try
                {
                    action(arg1, arg2, arg3);
                }
                finally
                {
                    @this.ExitWriteLock();
                }
            }
            public static void Write<T1, T2, T3, T4>(this IReadWriteLock @this, Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                @this.EnterWriteLock();
                try
                {
                    action(arg1, arg2, arg3, arg4);
                }
                finally
                {
                    @this.ExitWriteLock();
                }
            }
            public static T Write<T>(this IReadWriteLock @this, Func<T> func)
            {
                @this.EnterWriteLock();
                try
                {
                    return func();
                }
                finally
                {
                    @this.ExitWriteLock();
                }
            }
            public static TResult Write<T, TResult>(this IReadWriteLock @this, Func<T, TResult> func, T arg)
            {
                @this.EnterWriteLock();
                try
                {
                    return func(arg);
                }
                finally
                {
                    @this.ExitWriteLock();
                }
            }
            public static TResult Write<T1, T2, TResult>(this IReadWriteLock @this, Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
            {
                @this.EnterWriteLock();
                try
                {
                    return func(arg1, arg2);
                }
                finally
                {
                    @this.ExitWriteLock();
                }
            }
            public static TResult Write<T1, T2, T3, TResult>(this IReadWriteLock @this, Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3)
            {
                @this.EnterWriteLock();
                try
                {
                    return func(arg1, arg2, arg3);
                }
                finally
                {
                    @this.ExitWriteLock();
                }
            }
            public static TResult Write<T1, T2, T3, T4, TResult>(this IReadWriteLock @this, Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                @this.EnterWriteLock();
                try
                {
                    return func(arg1, arg2, arg3, arg4);
                }
                finally
                {
                    @this.ExitWriteLock();
                }
            }
            #endregion

            #region TryRead
            public static bool TryRead<T>(this IReadWriteLock @this, int millisecondsTimeout, Func<T> func, out T result)
            {
                if (!@this.TryEnterReadLock(millisecondsTimeout))
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
                    @this.ExitReadLock();
                }
            }
            public static bool TryRead<T, TResult>(this IReadWriteLock @this, int millisecondsTimeout, Func<T, TResult> func, T arg, out TResult result)
            {
                if (!@this.TryEnterReadLock(millisecondsTimeout))
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
                    @this.ExitReadLock();
                }
            }
            public static bool TryRead<T1, T2, TResult>(this IReadWriteLock @this, int millisecondsTimeout, Func<T1, T2, TResult> func, T1 arg1, T2 arg2, out TResult result)
            {
                if (!@this.TryEnterReadLock(millisecondsTimeout))
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
                    @this.ExitReadLock();
                }
            }
            public static bool TryRead<T1, T2, T3, TResult>(
                this IReadWriteLock @this,
                int millisecondsTimeout,
                Func<T1, T2, T3, TResult> func,
                T1 arg1,
                T2 arg2,
                T3 arg3,
                out TResult result)
            {
                if (!@this.TryEnterReadLock(millisecondsTimeout))
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
                    @this.ExitReadLock();
                }
            }
            public static bool TryRead<T1, T2, T3, T4, TResult>(
                this IReadWriteLock @this,
                int millisecondsTimeout,
                Func<T1, T2, T3, T4, TResult> func,
                T1 arg1,
                T2 arg2,
                T3 arg3,
                T4 arg4,
                out TResult result)
            {
                if (!@this.TryEnterReadLock(millisecondsTimeout))
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
                    @this.ExitReadLock();
                }
            }
            #endregion

            #region TryWrite
            public static bool TryWrite<T>(this IReadWriteLock @this, int millisecondsTimeout, Func<T> func, out T result)
            {
                if (!@this.TryEnterWriteLock(millisecondsTimeout))
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
                    @this.ExitWriteLock();
                }
            }
            public static bool TryWrite<T, TResult>(this IReadWriteLock @this, int millisecondsTimeout, Func<T, TResult> func, T arg, out TResult result)
            {
                if (!@this.TryEnterWriteLock(millisecondsTimeout))
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
                    @this.ExitWriteLock();
                }
            }
            public static bool TryWrite<T1, T2, TResult>(this IReadWriteLock @this, int millisecondsTimeout, Func<T1, T2, TResult> func, T1 arg1, T2 arg2, out TResult result)
            {
                if (!@this.TryEnterWriteLock(millisecondsTimeout))
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
                    @this.ExitWriteLock();
                }
            }
            public static bool TryWrite<T1, T2, T3, TResult>(
                this IReadWriteLock @this,
                int millisecondsTimeout,
                Func<T1, T2, T3, TResult> func,
                T1 arg1,
                T2 arg2,
                T3 arg3,
                out TResult result)
            {
                if (!@this.TryEnterWriteLock(millisecondsTimeout))
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
                    @this.ExitWriteLock();
                }
            }
            public static bool TryWrite<T1, T2, T3, T4, TResult>(
                this IReadWriteLock @this,
                int millisecondsTimeout,
                Func<T1, T2, T3, T4, TResult> func,
                T1 arg1,
                T2 arg2,
                T3 arg3,
                T4 arg4,
                out TResult result)
            {
                if (!@this.TryEnterWriteLock(millisecondsTimeout))
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
                    @this.ExitWriteLock();
                }
            }
            #endregion

            #region CreateReadLockHandle(...)
            public static ILockHandle CreateReadLockHandle(this IReadWriteLock @this)
            {
                return new ReadLockHandle(@this);
            }
            #endregion

            #region CreateUpgradeableReadLockHandle(...)
            public static ILockHandle CreateUpgradeableReadLockHandle(this IReadWriteLock @this)
            {
                return new UpgradeableReadLockHandle(@this);
            }
            #endregion

            #region CreateWriteLockHandle(...)
            public static ILockHandle CreateWriteLockHandle(this IReadWriteLock @this)
            {
                return new WriteLockHandle(@this);
            }
            #endregion

            public static void Invoke(this IReadWriteLock @this, Func<bool> isLockNeeded, Action writeAction)
            {
                using (CreateReadLockHandle(@this))
                {
                    if (!isLockNeeded())
                    {
                        return;
                    }
                }
                using (CreateUpgradeableReadLockHandle(@this))
                {
                    if (!isLockNeeded())
                    {
                        return;
                    }
                    using (CreateWriteLockHandle(@this))
                    {
                        writeAction();
                    }
                }
            }
            public static T Invoke<T>(this IReadWriteLock @this, Func<T> readFunc, Func<T, bool> isLockNeeded, Func<T> workFunc)
            {
                using (CreateReadLockHandle(@this))
                {
                    var result = readFunc();
                    if (!isLockNeeded(result))
                    {
                        return result;
                    }
                }
                using (CreateUpgradeableReadLockHandle(@this))
                {
                    var result = readFunc();
                    if (!isLockNeeded(result))
                    {
                        return result;
                    }
                    using (CreateWriteLockHandle(@this))
                    {
                        result = workFunc();
                    }
                    return result;
                }
            }
            public static T Invoke<T>(this IReadWriteLock @this, Attempt<T> readFunc, Func<T> workFunc)
            {
                T result;
                using (CreateReadLockHandle(@this))
                {
                    if (readFunc(out result))
                    {
                        return result;
                    }
                }
                using (CreateUpgradeableReadLockHandle(@this))
                {
                    if (readFunc(out result))
                    {
                        return result;
                    }
                    using (CreateWriteLockHandle(@this))
                    {
                        result = workFunc();
                    }
                    return result;
                }
            }
            public static TResult Invoke<T, TResult>(this IReadWriteLock @this, T arg, Attempt<T, TResult> readFunc, Func<T, TResult> workFunc)
            {
                TResult result;
                using (CreateReadLockHandle(@this))
                {
                    if (readFunc(arg, out result))
                    {
                        return result;
                    }
                }
                using (CreateUpgradeableReadLockHandle(@this))
                {
                    if (readFunc(arg, out result))
                    {
                        return result;
                    }
                    using (CreateWriteLockHandle(@this))
                    {
                        result = workFunc(arg);
                    }
                    return result;
                }
            }
        }
    }
}