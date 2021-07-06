using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Extensions
{
    /// <summary>
    /// A static class that provides useful utilities for working with delegates.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static partial class Delegate
    {
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="action">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Action"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Action"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action Resolve(Action action) => action ?? throw new ArgumentNullException(nameof(action));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="action">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Action{T}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Action{T}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T> Resolve<T>(Action<T> action) => action ?? throw new ArgumentNullException(nameof(action));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="action">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Action{T1, T2}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Action{T1, T2}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T1, T2> Resolve<T1, T2>(Action<T1, T2> action) => action ?? throw new ArgumentNullException(nameof(action));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="action">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Action{T1, T2, T3}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Action{T1, T2, T3}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T1, T2, T3> Resolve<T1, T2, T3>(Action<T1, T2, T3> action) => action ?? throw new ArgumentNullException(nameof(action));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="action">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Action{T1, T2, T3, T4}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Action{T1, T2, T3, T4}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T1, T2, T3, T4> Resolve<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) => action ?? throw new ArgumentNullException(nameof(action));
        #if NETSTANDARD || NET40_OR_NEWER
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="action">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Action{T1, T2, T3, T4, T5}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Action{T1, T2, T3, T4, T5}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T1, T2, T3, T4, T5> Resolve<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action) => action ?? throw new ArgumentNullException(nameof(action));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="action">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Action{T1, T2, T3, T4, T5, T6}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Action{T1, T2, T3, T4, T5, T6}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T1, T2, T3, T4, T5, T6> Resolve<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action) => action ?? throw new ArgumentNullException(nameof(action));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="action">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Action{T1, T2, T3, T4, T5, T6, T7}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Action{T1, T2, T3, T4, T5, T6, T7}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T1, T2, T3, T4, T5, T6, T7> Resolve<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action) => action ?? throw new ArgumentNullException(nameof(action));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="action">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is <c>null</c>
        /// </exception>
        public static Action<T1, T2, T3, T4, T5, T6, T7, T8> Resolve<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action) => action ?? throw new ArgumentNullException(nameof(action));
        #endif
        
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="func">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Func{TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Func{TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<TResult> Resolve<TResult>(Func<TResult> func) => func ?? throw new ArgumentNullException(nameof(func));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="func">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Func{T, TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Func{T, TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T, TResult> Resolve<T, TResult>(Func<T, TResult> func) => func ?? throw new ArgumentNullException(nameof(func));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="func">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Func{T1, T2, TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Func{T1, T2, TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T1, T2, TResult> Resolve<T1, T2, TResult>(Func<T1, T2, TResult> func) => func ?? throw new ArgumentNullException(nameof(func));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="func">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Func{T1, T2, T3, TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Func{T1, T2, T3, TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T1, T2, T3, TResult> Resolve<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func) => func ?? throw new ArgumentNullException(nameof(func));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="func">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Func{T1, T2, T3, T4, TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Func{T1, T2, T3, T4, TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T1, T2, T3, T4, TResult> Resolve<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func) => func ?? throw new ArgumentNullException(nameof(func));
        #if NETSTANDARD || NET35_OR_NEWER
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="func">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T1, T2, T3, T4, T5, TResult> Resolve<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func) => func ?? throw new ArgumentNullException(nameof(func));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="func">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T1, T2, T3, T4, T5, T6, TResult> Resolve<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func) => func ?? throw new ArgumentNullException(nameof(func));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="func">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> Resolve<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func) => func ?? throw new ArgumentNullException(nameof(func));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="func">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func"/> is <c>null</c>
        /// </exception>
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Resolve<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func) => func ?? throw new ArgumentNullException(nameof(func));
        #endif
        
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="predicate">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Predicate{T}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Predicate{T}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="predicate"/> is <c>null</c>
        /// </exception>
        public static Predicate<T> Resolve<T>(Predicate<T> predicate) => predicate ?? throw new ArgumentNullException(nameof(predicate));
        
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="attempt">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Attempt{TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Attempt{TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attempt"/> is <c>null</c>
        /// </exception>
        public static Attempt<TResult> Resolve<TResult>(Attempt<TResult> attempt) => attempt ?? throw new ArgumentNullException(nameof(attempt));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="attempt">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Attempt{T, TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Attempt{T, TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attempt"/> is <c>null</c>
        /// </exception>
        public static Attempt<T, TResult> Resolve<T, TResult>(Attempt<T, TResult> attempt) => attempt ?? throw new ArgumentNullException(nameof(attempt));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="attempt">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Attempt{T1, T2, TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Attempt{T1, T2, TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attempt"/> is <c>null</c>
        /// </exception>
        public static Attempt<T1, T2, TResult> Resolve<T1, T2, TResult>(Attempt<T1, T2, TResult> attempt) => attempt ?? throw new ArgumentNullException(nameof(attempt));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="attempt">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Attempt{T1, T2, T3, TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attempt"/> is <c>null</c>
        /// </exception>
        public static Attempt<T1, T2, T3, TResult> Resolve<T1, T2, T3, TResult>(Attempt<T1, T2, T3, TResult> attempt) => attempt ?? throw new ArgumentNullException(nameof(attempt));
        /// <summary>
        /// Resolves the identity of the provided method group/delegate
        /// </summary>
        /// <param name="attempt">
        /// A method group or a multi-cast delegate instance matching the signature of the
        /// <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </param>
        /// <returns>
        /// An <see cref="Attempt{T1, T2, T3, T4, TResult}"/> representing the provided multi-cast delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="attempt"/> is <c>null</c>
        /// </exception>
        public static Attempt<T1, T2, T3, T4, TResult> Resolve<T1, T2, T3, T4, TResult>(Attempt<T1, T2, T3, T4, TResult> attempt) => attempt ?? throw new ArgumentNullException(nameof(attempt));
        // public static Attempt<T1, T2, T3, T4, T5, TResult> From<T1, T2, T3, T4, T5, TResult>(Attempt<T1, T2, T3, T4, T5, TResult> attempt) => attempt ?? throw new ArgumentNullException(nameof(attempt));
        // public static Attempt<T1, T2, T3, T4, T5, T6, TResult> From<T1, T2, T3, T4, T5, T6, TResult>(Attempt<T1, T2, T3, T4, T5, T6, TResult> attempt) => attempt ?? throw new ArgumentNullException(nameof(attempt));
        // public static Attempt<T1, T2, T3, T4, T5, T6, T7, TResult> From<T1, T2, T3, T4, T5, T6, T7, TResult>(Attempt<T1, T2, T3, T4, T5, T6, T7, TResult> attempt) => attempt ?? throw new ArgumentNullException(nameof(attempt));
        // public static Attempt<T1, T2, T3, T4, T5, T6, T7, T8, TResult> From<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Attempt<T1, T2, T3, T4, T5, T6, T7, T8, TResult> attempt) => attempt ?? throw new ArgumentNullException(nameof(attempt));
    }
}