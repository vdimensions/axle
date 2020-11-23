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
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="action">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
        /// </param>
        /// <typeparam name="T">
        /// The type of the argument of the partial application.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Action"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action Apply<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Action<T> action, T arg)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return () => action(arg);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="action">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
        /// </param>
        /// <typeparam name="T1">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Action{T2}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T2> Apply<T1, T2>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Action<T1, T2> action, T1 arg)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return arg2 => action(arg, arg2);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="action">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Action{T2, T3}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T2, T3> Apply<T1, T2, T3>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Action<T1, T2, T3> action, T1 arg)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg2, arg3) => action(arg, arg2, arg3);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="action">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Action{T2, T3, T4}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T2, T3, T4> Apply<T1, T2, T3, T4>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Action<T1, T2, T3, T4> action, T1 arg)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg2, arg3, arg4) => action(arg, arg2, arg3, arg4);
        }
        #if NETSTANDARD || NET40_OR_NEWER
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="action">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Action{T2, T3, T4, T5}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T2, T3, T4, T5> Apply<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action, T1 arg)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg2, arg3, arg4, arg5) => action(arg, arg2, arg3, arg4, arg5);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="action">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Action{T2, T3, T4, T5, T6}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T2, T3, T4, T5, T6> Apply<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> action, T1 arg)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg2, arg3, arg4, arg5, arg6) => action(arg, arg2, arg3, arg4, arg5, arg6);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="action">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Action{T2, T3, T4, T5, T6, T7}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T2, T3, T4, T5, T6, T7> Apply<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> action, T1 arg)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg2, arg3, arg4, arg5, arg6, arg7) => action(arg, arg2, arg3, arg4, arg5, arg6, arg7);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="action">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Action{T2, T3, T4, T5, T6, T7, T8}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T2, T3, T4, T5, T6, T7, T8> Apply<T1, T2, T3, T4, T5, T6, T7, T8>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 arg)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            return (arg2, arg3, arg4, arg5, arg6, arg7, arg8) => action(arg, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
        #endif
        
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="func">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
        /// </param>
        /// <typeparam name="T">
        /// The type of the first argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<TResult> Apply<T, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Func<T, TResult> func, T arg)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return () => func(arg);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="func">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Func{T2, TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T2, TResult> Apply<T1, T2, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Func<T1, T2, TResult> func, T1 arg) => arg2 =>
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return func(arg, arg2);
        };
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="func">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Func{T2, T3, TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T2, T3, TResult> Apply<T1, T2, T3, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Func<T1, T2, T3, TResult> func, T1 arg) => (arg2, arg3) =>
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return func(arg, arg2, arg3);
        };
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="func">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Func{T2, T3, T4, TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Func<T1, T2, T3, T4, TResult> func, T1 arg)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return (arg2, arg3, arg4) => func(arg, arg2, arg3, arg4);
        }
        #if NETSTANDARD || NET35_OR_NEWER
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="func">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Func{T2, T3, T4, T5, TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T2, T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func, T1 arg)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return (arg2, arg3, arg4, arg5) => func(arg, arg2, arg3, arg4, arg5);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="func">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Func{T2, T3, T4, T5, T6, TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T2, T3, T4, T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 arg)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return (arg2, arg3, arg4, arg5, arg6) => func(arg, arg2, arg3, arg4, arg5, arg6);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="func">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T2, T3, T4, T5, T6, T7, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 arg)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return (arg2, arg3, arg4, arg5, arg6, arg7) => func(arg, arg2, arg3, arg4, arg5, arg6, arg7);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="func">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Func{T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T2, T3, T4, T5, T6, T7, T8, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T1 arg)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            return (arg2, arg3, arg4, arg5, arg6, arg7, arg8) => func(arg, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
        #endif
        
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="predicate">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
        /// </param>
        /// <typeparam name="T">
        /// The type of the argument of the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Func{TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="predicate"/> is <c>null</c>
        /// </exception>
        public static Func<bool> Apply<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Predicate<T> predicate, T arg)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            return () => predicate(arg);
        }

        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="attempt">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
        /// </param>
        /// <typeparam name="T">
        /// The type of the argument of the original delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result returned by the original delegate.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Attempt{TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attempt"/> is <c>null</c>
        /// </exception>
        public static Attempt<TResult> Apply<T, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Attempt<T, TResult> attempt, T arg)
        {
            if (attempt == null)
            {
                throw new ArgumentNullException(nameof(attempt));
            }
            return (out TResult result) => attempt(arg, out result);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="attempt">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Attempt{T2, TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attempt"/> is <c>null</c>
        /// </exception>
        public static Attempt<T2, TResult> Apply<T1, T2, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Attempt<T1, T2, TResult> attempt, T1 arg)
        {
            if (attempt == null)
            {
                throw new ArgumentNullException(nameof(attempt));
            }
            return (T2 arg2, out TResult result) => attempt(arg, arg2, out result);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="attempt">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Attempt{T2, T3, TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attempt"/> is <c>null</c>
        /// </exception>
        public static Attempt<T2, T3, TResult> Apply<T1, T2, T3, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Attempt<T1, T2, T3, TResult> attempt, T1 arg)
        {
            if (attempt == null)
            {
                throw new ArgumentNullException(nameof(attempt));
            }
            return (T2 arg2, T3 arg3, out TResult result) => attempt(arg, arg2, arg3, out result);
        }
        /// <summary>
        /// Applies a value to the first (leftmost) argument of the provided delegate, and returns a a new
        /// delegate representing the partially-applied (curried) version of the original.
        /// </summary>
        /// <param name="attempt">
        /// The delegate to perform partial application on.
        /// </param>
        /// <param name="arg">
        /// The value for the first argument that will become baked-in the resulting partially-applied delegate.
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
        /// A <see cref="Attempt{T2, T3, T4, TResult}"/> delegate representing the result of the partial application.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attempt"/> is <c>null</c>
        /// </exception>
        public static Attempt<T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Attempt<T1, T2, T3, T4, TResult> attempt, T1 arg)
        {
            if (attempt == null)
            {
                throw new ArgumentNullException(nameof(attempt));
            }
            return (T2 arg2, T3 arg3, T4 arg4, out TResult result) => attempt(arg, arg2, arg3, arg4, out result);
        }
        // public static Attempt<T2, T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(this Attempt<T1, T2, T3, T4, T5, TResult> action, T1 arg) => (arg2, arg3, arg4, arg5) => action(arg, arg2, arg3, arg4, arg5);
        // public static Attempt<T2, T3, T4, T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(this Attempt<T1, T2, T3, T4, T5, T6, TResult> action, T1 arg) => (arg2, arg3, arg4, arg5, arg6) => action(arg, arg2, arg3, arg4, arg5, arg6);
        // public static Attempt<T2, T3, T4, T5, T6, T7, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(this Attempt<T1, T2, T3, T4, T5, T6, T7, TResult> action, T1 arg) => (arg2, arg3, arg4, arg5, arg6, arg7) => action(arg, arg2, arg3, arg4, arg5, arg6, arg7);
        // public static Attempt<T2, T3, T4, T5, T6, T7, T8, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Attempt<T1, T2, T3, T4, T5, T6, T7, T8, TResult> action, T1 arg) => (arg2, arg3, arg4, arg5, arg6, arg7, arg8) => action(arg, arg2, arg3, arg4, arg5, arg6, arg7, arg8);        
    }
}