using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Extensions
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static partial class Delegate
    {
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="action"/> that accepts the same formal arguments
        /// but in reverse order.
        /// </summary>
        /// <param name="action">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T2, T1> FlipArgs<T1, T2>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Action<T1, T2> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg2, arg1) => action(arg1, arg2);
        } 
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="action"/> that accepts the same formal arguments
        /// but in reverse order.
        /// </summary>
        /// <param name="action">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T3, T2, T1> FlipArgs<T1, T2, T3>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Action<T1, T2, T3> action) 
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg3, arg2, arg1) => action(arg1, arg2, arg3);
        }
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="action"/> that accepts the same formal arguments
        /// but in reverse order.
        /// </summary>
        /// <param name="action">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T4, T3, T2, T1> FlipArgs<T1, T2, T3, T4>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Action<T1, T2, T3, T4> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg4, arg3, arg2, arg1) => action(arg1, arg2, arg3, arg4);
        }
        #if NETSTANDARD || NET40_OR_NEWER
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="action"/> that accepts the same formal arguments
        /// but in reverse order.
        /// </summary>
        /// <param name="action">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth argument of the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T5, T4, T3, T2, T1> FlipArgs<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg5, arg4, arg3, arg2, arg1) => action(arg1, arg2, arg3, arg4, arg5);
        }
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="action"/> that accepts the same formal arguments
        /// but in reverse order.
        /// </summary>
        /// <param name="action">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T6">
        /// The type of the sixth argument of the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T6, T5, T4, T3, T2, T1> FlipArgs<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg6, arg5, arg4, arg3, arg2, arg1) => action(arg1, arg2, arg3, arg4, arg5, arg6);
        }
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="action"/> that accepts the same formal arguments
        /// but in reverse order.
        /// </summary>
        /// <param name="action">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T6">
        /// The type of the sixth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T7">
        /// The type of the seventh argument of the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T7, T6, T5, T4, T3, T2, T1> FlipArgs<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg7, arg6, arg5, arg4, arg3, arg2, arg1) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="action"/> that accepts the same formal arguments
        /// but in reverse order.
        /// </summary>
        /// <param name="action">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T6">
        /// The type of the sixth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T7">
        /// The type of the seventh argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T8">
        /// The type of the eighth argument of the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T8, T7, T6, T5, T4, T3, T2, T1> FlipArgs<T1, T2, T3, T4, T5, T6, T7, T8>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg8, arg7, arg6, arg5, arg4, arg3, arg2, arg1) =>
                action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
        #endif
        
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="func"/> that accepts the same formal arguments but
        /// in reverse order.
        /// </summary>
        /// <param name="func">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T2, T1, TResult> FlipArgs<T1, T2, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Func<T1, T2, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return (arg2, arg1) => func(arg1, arg2);
        }
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="func"/> that accepts the same formal arguments but
        /// in reverse order.
        /// </summary>
        /// <param name="func">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T3, T2, T1, TResult> FlipArgs<T1, T2, T3, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Func<T1, T2, T3, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return (arg3, arg2, arg1) => func(arg1, arg2, arg3);
        }

        /// <summary>
        /// Creates a new delegate from the provided <paramref name="func"/> that accepts the same formal arguments but
        /// in reverse order.
        /// </summary>
        /// <param name="func">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T4, T3, T2, T1, TResult> FlipArgs<T1, T2, T3, T4, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Func<T1, T2, T3, T4, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return (arg4, arg3, arg2, arg1) => func(arg1, arg2, arg3, arg4);
        }
        #if NETSTANDARD || NET35_OR_NEWER
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="func"/> that accepts the same formal arguments but
        /// in reverse order.
        /// </summary>
        /// <param name="func">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T5, T4, T3, T2, T1, TResult> FlipArgs<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return (arg5, arg4, arg3, arg2, arg1) => func(arg1, arg2, arg3, arg4, arg5);
        }
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="func"/> that accepts the same formal arguments but
        /// in reverse order.
        /// </summary>
        /// <param name="func">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T6">
        /// The type of the sixth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T6, T5, T4, T3, T2, T1, TResult> FlipArgs<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return (arg6, arg5, arg4, arg3, arg2, arg1) => func(arg1, arg2, arg3, arg4, arg5, arg6);
        }
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="func"/> that accepts the same formal arguments but
        /// in reverse order.
        /// </summary>
        /// <param name="func">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T6">
        /// The type of the sixth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T7">
        /// The type of the seventh argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T7, T6, T5, T4, T3, T2, T1, TResult> FlipArgs<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return (arg7, arg6, arg5, arg4, arg3, arg2, arg1) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="func"/> that accepts the same formal arguments but
        /// in reverse order.
        /// </summary>
        /// <param name="func">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T6">
        /// The type of the sixth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T7">
        /// The type of the seventh argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T8">
        /// The type of the eighth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T8, T7, T6, T5, T4, T3, T2, T1, TResult> FlipArgs<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return (arg8, arg7, arg6, arg5, arg4, arg3, arg2, arg1) =>
                func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
        #endif
        
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="attempt"/> that accepts the same formal arguments
        /// but in reverse order.
        /// </summary>
        /// <param name="attempt">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attempt"/> is <c>null</c>
        /// </exception>
        public static Attempt<T2, T1, TResult> FlipArgs<T1, T2, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Attempt<T1, T2, TResult> attempt) 
        { 
            if (attempt == null)
            {
                throw new ArgumentNullException(nameof(attempt));
            }
            return (T2 arg2, T1 arg1, out TResult result) => attempt(arg1, arg2, out result); 
        }
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="attempt"/> that accepts the same formal arguments
        /// but in reverse order.
        /// </summary>
        /// <param name="attempt">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attempt"/> is <c>null</c>
        /// </exception>
        public static Attempt<T3, T2, T1, TResult> FlipArgs<T1, T2, T3, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Attempt<T1, T2, T3, TResult> attempt) 
        { 
            if (attempt == null)
            {
                throw new ArgumentNullException(nameof(attempt));
            }
            return (T3 arg3, T2 arg2, T1 arg1, out TResult result) => attempt(arg1, arg2, arg3, out result);
        }
        /// <summary>
        /// Creates a new delegate from the provided <paramref name="attempt"/> that accepts the same formal arguments
        /// but in reverse order.
        /// </summary>
        /// <param name="attempt">
        /// The delegate to flip arguments of.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial
        /// application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attempt"/> is <c>null</c>
        /// </exception>
        public static Attempt<T4, T3, T2, T1, TResult> FlipArgs<T1, T2, T3, T4, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Attempt<T1, T2, T3, T4, TResult> attempt) 
        {
            if (attempt == null)
            {
                throw new ArgumentNullException(nameof(attempt));
            }
            return (T4 arg4, T3 arg3, T2 arg2, T1 arg1, out TResult result) => attempt(arg1, arg2, arg3, arg4, out result);
        }
        // public static Attempt<T5, T4, T3, T2, T1, TResult> FlipArgs<T1, T2, T3, T4, T5, TResult>(this Attempt<T1, T2, T3, T4, T5, TResult> attempt) => (T5 arg5, T4 arg4, T3 arg3, T2 arg2, T1 arg1, out TResult result) => attempt(arg1, arg2, arg3, arg4, arg5, out result);
        // public static Attempt<T6, T5, T4, T3, T2, T1, TResult> FlipArgs<T1, T2, T3, T4, T5, T6, TResult>(this Attempt<T1, T2, T3, T4, T5, T6, TResult> attempt) => (T6 arg6, T5 arg5, T4 arg4, T3 arg3, T2 arg2, T1 arg1, out TResult result) => attempt(arg1, arg2, arg3, arg4, arg5, arg6, out result);
        // public static Attempt<T7, T6, T5, T4, T3, T2, T1, TResult> FlipArgs<T1, T2, T3, T4, T5, T6, T7, TResult>(this Attempt<T1, T2, T3, T4, T5, T6, T7, TResult> attempt) => (T7 arg7, T6 arg6, T5 arg5, T4 arg4, T3 arg3, T2 arg2, T1 arg1, out TResult result) => attempt(arg1, arg2, arg3, arg4, arg5, arg6, arg7, out result);
        // public static Attempt<T8, T7, T6, T5, T4, T3, T2, T1, TResult> FlipArgs<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Attempt<T1, T2, T3, T4, T5, T6, T7, T8, TResult> attempt) => (T8 arg8, T7 arg7, T6 arg6, T5 arg5, T4 arg4, T3 arg3, T2 arg2, T1 arg1, out TResult result) => attempt(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, out result);
    }
}