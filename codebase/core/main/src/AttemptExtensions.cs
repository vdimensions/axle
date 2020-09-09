#if NETSTANDARD || NET20_OR_NEWER
using System.Collections.Generic;
using Axle.Verification;


namespace Axle
{
    using Enumerable = System.Linq.Enumerable;

    /// <summary>
    /// A <see langword="static"/> class containing extension methods for 
    /// working with attempt delegates.
    /// </summary>
    public static class AttemptExtensions
    {
        /// <summary>
        /// Executes the collection of <see cref="Attempt{TResult}"/> in the provided by 
        /// collection order until any of the delegates returns <c>true</c>.
        /// </summary>
        /// <typeparam name="TResult">
        /// The type of the result of the <see cref="Attempt{TResult}"/> delegate.
        /// </typeparam>
        /// <param name="attempts">
        /// The collection of <see cref="Attempt{TResult}"/> to invoke.
        /// </param>
        /// <param name="result">
        /// An output parameter containing the result of the first delegate to return <c>true</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if any of the provided by the <paramref name="attempts"/> 
        /// delegate succeeds (returns <c>true</c> itself); 
        /// <c>false</c> otherwise.
        /// </returns>
        public static bool Any<TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IEnumerable<Attempt<TResult>> attempts, out TResult result)
        {
            var res = result = default(TResult);
            if (Enumerable.Any(Verifier.IsNotNull(Verifier.VerifyArgument(attempts, nameof(attempts))).Value, x => x(out res)))
            {
                result = res;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Executes the collection of <see cref="Attempt{T, TResult}"/> in the provided by 
        /// the collection order until any of the delegates returns <c>true</c>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the argument to the <see cref="Attempt{T, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the <see cref="Attempt{T, TResult}"/> delegate.
        /// </typeparam>
        /// <param name="attempts">
        /// The collection of <see cref="Attempt{T, TResult}"/> to invoke.
        /// </param>
        /// <param name="arg">
        /// The argument to the <see cref="Attempt{T, TResult}"/> delegate.
        /// </param>
        /// <param name="result">
        /// An output parameter containing the result of the first delegate to return <c>true</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if any of the provided by the 
        /// <paramref name="attempts"/> delegate succeeds (returns <c>true</c> itself); 
        /// <c>false</c> otherwise.
        /// </returns>
        public static bool Any<T, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IEnumerable<Attempt<T, TResult>> attempts, T arg, out TResult result)
        {
            var res = result = default(TResult);
            if (Enumerable.Any(Verifier.IsNotNull(Verifier.VerifyArgument(attempts, nameof(attempts))).Value, x => x(arg, out res)))
            {
                result = res;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Executes the collection of <see cref="Attempt{T1, T2, TResult}"/> in the provided 
        /// by the collection order until any of the delegates returns <c>true</c>.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument to the <see cref="Attempt{T1, T2, TResult}"/> 
        /// delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to the <see cref="Attempt{T1, T2, TResult}"/> 
        /// delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the <see cref="Attempt{T1, T2, TResult}"/> 
        /// delegate.
        /// </typeparam>
        /// <param name="attempts">
        /// The collection of <see cref="Attempt{T1, T2, TResult}"/> to invoke.
        /// </param>
        /// <param name="arg1">
        /// The first argument to the <see cref="Attempt{T1, T2, TResult}"/> delegate.
        /// </param>
        /// <param name="arg2">
        /// The second argument to the <see cref="Attempt{T1, T2, TResult}"/> delegate.
        /// </param>
        /// <param name="result">
        /// An output parameter containing the result of the first delegate to return 
        /// <c>true</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if any of the provided by the <paramref name="attempts"/> 
        /// delegates succeeds (returns <c>true</c> itself); 
        /// <c>false</c> otherwise.
        /// </returns>
        public static bool Any<T1, T2, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IEnumerable<Attempt<T1, T2, TResult>> attempts, T1 arg1, T2 arg2, out TResult result)
        {
            var res = result = default(TResult);
            if (Enumerable.Any(Verifier.IsNotNull(Verifier.VerifyArgument(attempts, nameof(attempts))).Value, x => x(arg1, arg2, out res)))
            {
                result = res;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Executes the collection of <see cref="Attempt{T1, T2, T3, TResult}"/> in the provided by 
        /// the collection order until any of the delegates returns <c>true</c>.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument to the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument to the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </typeparam>
        /// <param name="attempts">
        /// The collection of <see cref="Attempt{T1, T2, T3, TResult}"/> to invoke.
        /// </param>
        /// <param name="arg1">
        /// The first argument to the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </param>
        /// <param name="arg2">
        /// The second argument to the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </param>
        /// <param name="arg3">
        /// The third argument to the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </param>
        /// <param name="result">
        /// An output parameter containing the result of the first delegate to return 
        /// <c>true</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if any of the provided by the <paramref name="attempts"/> 
        /// delegate succeeds (returns <c>true</c> itself); 
        /// <c>false</c> otherwise.
        /// </returns>
        public static bool Any<T1, T2, T3, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IEnumerable<Attempt<T1, T2, T3, TResult>> attempts, T1 arg1, T2 arg2, T3 arg3, out TResult result)
        {
            var res = result = default(TResult);
            if (Enumerable.Any(Verifier.IsNotNull(Verifier.VerifyArgument(attempts, nameof(attempts))).Value, x => x(arg1, arg2, arg3, out res)))
            {
                result = res;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Executes the collection of <see cref="Attempt{T1, T2, T3, T4, TResult}"/> in the provided 
        /// by the collection order until any of the delegates returns <c>true</c>.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </typeparam>
        /// <param name="attempts">
        /// The collection of <see cref="Attempt{T1, T2, T3, T4, TResult}"/> to invoke.
        /// </param>
        /// <param name="arg1">
        /// The first argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </param>
        /// <param name="arg2">
        /// The second argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </param>
        /// <param name="arg3">
        /// The third argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </param>
        /// <param name="arg4">
        /// The fourth argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </param>
        /// <param name="result">
        /// An output parameter containing the result of the first delegate to return <c>true</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if any of the provided by the <paramref name="attempts"/> 
        /// delegate succeeds (returns <c>true</c> itself);
        /// <c>false</c> otherwise.
        /// </returns>
        public static bool Any<T1, T2, T3, T4, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IEnumerable<Attempt<T1, T2, T3, T4, TResult>> attempts, T1 arg1, T2 arg2, T3 arg3, T4 arg4, out TResult result)
        {
            var res = result = default(TResult);
            if (Enumerable.Any(Verifier.IsNotNull(Verifier.VerifyArgument(attempts, nameof(attempts))).Value, x => x(arg1, arg2, arg3, arg4, out res)))
            {
                result = res;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Invokes the current <see cref="Attempt{TResult}"/> and returns either the delegate's result,
        /// or the provided by the <paramref name="defaultValue"/> parameter value.
        /// </summary>
        /// <typeparam name="TResult">
        /// The type of the result of the <see cref="Attempt{TResult}"/> delegate.
        /// </typeparam>
        /// <param name="attempt">
        /// The delegate to invoke.
        /// </param>
        /// <param name="defaultValue">
        /// A default value to use as a fallback in case the provided delegate does not produce a value of its own.
        /// </param>
        /// <returns>
        /// Either the delegate's result, or the provided by the <paramref name="defaultValue"/> 
        /// parameter value.
        /// </returns>
        public static TResult InvokeOrDefault<TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Attempt<TResult> attempt, TResult defaultValue)
        {
            return Verifier.IsNotNull(Verifier.VerifyArgument(attempt, nameof(attempt))).Value.Invoke(out var result) ? result : defaultValue;
        }
        /// <summary>
        /// Invokes the current <see cref="Attempt{T, TResult}"/> and returns either the delegate's 
        /// result, or the provided by the <paramref name="defaultValue"/> parameter value.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the first argument to the <see cref="Attempt{T, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the <see cref="Attempt{T, TResult}"/> delegate.
        /// </typeparam>
        /// <param name="attempt">
        /// The delegate to invoke.
        /// </param>
        /// <param name="arg">
        /// The argument to the <see cref="Attempt{T, TResult}"/> delegate.
        /// </param>
        /// <param name="defaultValue">
        /// A default value to use as a fallback in case the provided delegate does not produce a value of its own.
        /// </param>
        /// <returns>
        /// Either the delegate's result, or the provided by the <paramref name="defaultValue"/> parameter value.
        /// </returns>
        public static TResult InvokeOrDefault<T, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Attempt<T, TResult> attempt, T arg, TResult defaultValue)
        {
            return Verifier.IsNotNull(Verifier.VerifyArgument(attempt, nameof(attempt))).Value.Invoke(arg, out var result) ? result : defaultValue;
        }
        /// <summary>
        /// Invokes the current <see cref="Attempt{T1, T2, TResult}"/> and returns either the delegate's result,
        /// or the provided by the <paramref name="defaultValue"/> parameter value.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument to the <see cref="Attempt{T1, T2, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to the <see cref="Attempt{T1, T2, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the <see cref="Attempt{T1, T2, TResult}"/> delegate.
        /// </typeparam>
        /// <param name="attempt">
        /// The delegate to invoke.
        /// </param>
        /// <param name="arg1">
        /// The first argument to the <see cref="Attempt{T1, T2, TResult}"/> delegate.
        /// </param>
        /// <param name="arg2">
        /// The second argument to the <see cref="Attempt{T1, T2, TResult}"/> delegate.
        /// </param>
        /// <param name="defaultValue">
        /// A default value to use as a fallback in case the provided delegate does not produce a value of its own.
        /// </param>
        /// <returns>
        /// Either the delegate's result, or the provided by the <paramref name="defaultValue"/> parameter value.
        /// </returns>
        public static TResult InvokeOrDefault<T1, T2, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Attempt<T1, T2, TResult> attempt, T1 arg1, T2 arg2, TResult defaultValue)
        {
            return Verifier.IsNotNull(Verifier.VerifyArgument(attempt, nameof(attempt))).Value.Invoke(arg1, arg2, out var result) ? result : defaultValue;
        }
        /// <summary>
        /// Invokes the current <see cref="Attempt{T1, T2, T3, TResult}"/> and returns either the delegate's result,
        /// or the provided by the <paramref name="defaultValue"/> parameter value.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument to the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument to the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </typeparam>
        /// <param name="attempt">
        /// The delegate to invoke.
        /// </param>
        /// <param name="arg1">
        /// The first argument to the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </param>
        /// <param name="arg2">
        /// The second argument to the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </param>
        /// <param name="arg3">
        /// The third argument to the <see cref="Attempt{T1, T2, T3, TResult}"/> delegate.
        /// </param>
        /// <param name="defaultValue">
        /// A default value to use as a fallback in case the provided delegate does not produce a value of its own.
        /// </param>
        /// <returns>
        /// Either the delegate's result, or the provided by the <paramref name="defaultValue"/> parameter value.
        /// </returns>
        public static TResult InvokeOrDefault<T1, T2, T3, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Attempt<T1, T2, T3, TResult> attempt, T1 arg1, T2 arg2, T3 arg3, TResult defaultValue)
        {
            return Verifier.IsNotNull(Verifier.VerifyArgument(attempt, nameof(attempt))).Value.Invoke(arg1, arg2, arg3, out var result) ? result : defaultValue;
        }
        /// <summary>
        /// Invokes the current <see cref="Attempt{T1, T2, T3, T4, TResult}"/> and returns either the delegate's result,
        /// or the provided by the <paramref name="defaultValue"/> parameter value.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </typeparam>
        /// <param name="attempt">
        /// The delegate to invoke.
        /// </param>
        /// <param name="arg1">
        /// The first argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </param>
        /// <param name="arg2">
        /// The second argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </param>
        /// <param name="arg3">
        /// The third argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </param>
        /// <param name="arg4">
        /// The fourth argument to the <see cref="Attempt{T1, T2, T3, T4, TResult}"/> delegate.
        /// </param>
        /// <param name="defaultValue">
        /// A default value to use as a fallback in case the provided delegate does not produce a value of its own.
        /// </param>
        /// <returns>
        /// Either the delegate's result, or the provided by the <paramref name="defaultValue"/> parameter value.
        /// </returns>
        public static TResult InvokeOrDefault<T1, T2, T3, T4, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            Attempt<T1, T2, T3, T4, TResult> attempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, TResult defaultValue)
        {
            return Verifier.IsNotNull(Verifier.VerifyArgument(attempt, nameof(attempt))).Value.Invoke(arg1, arg2, arg3, arg4, out var result) ? result : defaultValue;
        }
    }
}
#endif