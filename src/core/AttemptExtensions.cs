using System.Collections.Generic;

using Axle.Verification;

using Enumerable = System.Linq.Enumerable;


namespace Axle
{
    public static class AttemptExtensions
    {
        public static bool Any<TResult>(this IEnumerable<Attempt<TResult>> attempts, out TResult result)
        {
            var res = result = default(TResult);
            if (Enumerable.Any<Attempt<TResult>>(attempts.VerifyArgument(nameof(attempts)).IsNotNull().Value, x => x(out res)))
            {
                result = res;
                return true;
            }
            return false;
        }
        public static bool Any<T, TResult>(this IEnumerable<Attempt<T, TResult>> attempts, T arg, out TResult result)
        {
            var res = result = default(TResult);
            if (Enumerable.Any<Attempt<T, TResult>>(attempts.VerifyArgument(nameof(attempts)).IsNotNull().Value, x => x(arg, out res)))
            {
                result = res;
                return true;
            }
            return false;
        }
        public static bool Any<T1, T2, TResult>(this IEnumerable<Attempt<T1, T2, TResult>> attempts, T1 arg1, T2 arg2, out TResult result)
        {
            var res = result = default(TResult);
            if (Enumerable.Any<Attempt<T1, T2, TResult>>(attempts.VerifyArgument(nameof(attempts)).IsNotNull().Value, x => x(arg1, arg2, out res)))
            {
                result = res;
                return true;
            }
            return false;
        }
        public static bool Any<T1, T2, T3, TResult>(this IEnumerable<Attempt<T1, T2, T3, TResult>> attempts, T1 arg1, T2 arg2, T3 arg3, out TResult result)
        {
            var res = result = default(TResult);
            if (Enumerable.Any<Attempt<T1, T2, T3, TResult>>(attempts.VerifyArgument(nameof(attempts)).IsNotNull().Value, x => x(arg1, arg2, arg3, out res)))
            {
                result = res;
                return true;
            }
            return false;
        }
        public static bool Any<T1, T2, T3, T4, TResult>(this IEnumerable<Attempt<T1, T2, T3, T4, TResult>> attempts, T1 arg1, T2 arg2, T3 arg3, T4 arg4, out TResult result)
        {
            var res = result = default(TResult);
            if (Enumerable.Any<Attempt<T1, T2, T3, T4, TResult>>(attempts.VerifyArgument(nameof(attempts)).IsNotNull().Value, x => x(arg1, arg2, arg3, arg4, out res)))
            {
                result = res;
                return true;
            }
            return false;
        }

        public static TResult Invoke<TResult>(this Attempt<TResult> attempt, TResult defaultValue)
        {
            TResult result;
            return attempt.VerifyArgument(nameof(attempt)).IsNotNull ().Value.Invoke (out result) ? result : defaultValue;
        }
        public static TResult Invoke<T, TResult>(this Attempt<T, TResult> attempt, T arg, TResult defaultValue)
        {
            TResult result;
            return attempt.VerifyArgument(nameof(attempt)).IsNotNull ().Value.Invoke (arg, out result) ? result : defaultValue;
        }
        public static TResult Invoke<T1, T2, TResult>(this Attempt<T1, T2, TResult> attempt, T1 arg1, T2 arg2, TResult defaultValue)
        {
            TResult result;
            return attempt.VerifyArgument(nameof(attempt)).IsNotNull().Value.Invoke(arg1, arg2, out result) ? result : defaultValue;
        }
        public static TResult Invoke<T1, T2, T3, TResult>(this Attempt<T1, T2, T3, TResult> attempt, T1 arg1, T2 arg2, T3 arg3, TResult defaultValue)
        {
            TResult result;
            return attempt.VerifyArgument(nameof(attempt)).IsNotNull().Value.Invoke(arg1, arg2, arg3, out result) ? result : defaultValue;
        }
        public static TResult Invoke<T1, T2, T3, T4, TResult>(this Attempt<T1, T2, T3, T4, TResult> attempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, TResult defaultValue)
        {
            TResult result;
            return attempt.VerifyArgument(nameof(attempt)).IsNotNull().Value.Invoke(arg1, arg2, arg3, arg4, out result) ? result : defaultValue;
        }

        public static Optional<TResult> Invoke<TResult>(this Attempt<TResult> attempt)
        {
            TResult result;
            return attempt.VerifyArgument(nameof(attempt)).IsNotNull().Value.Invoke(out result) ? Optional.From(result) : Optional<TResult>.Undefined;
        }
        public static Optional<TResult> Invoke<T, TResult>(this Attempt<T, TResult> attempt, T arg)
        {
            TResult result;
            return attempt.VerifyArgument(nameof(attempt)).IsNotNull().Value.Invoke(arg, out result) ? Optional.From(result) : Optional<TResult>.Undefined;
        }
        public static Optional<TResult> Invoke<T1, T2, TResult>(this Attempt<T1, T2, TResult> attempt, T1 arg1, T2 arg2)
        {
            TResult result;
            return attempt.VerifyArgument(nameof(attempt)).IsNotNull().Value.Invoke(arg1, arg2, out result) ? Optional.From(result) : Optional<TResult>.Undefined;
        }
        public static Optional<TResult> Invoke<T1, T2, T3, TResult>(this Attempt<T1, T2, T3, TResult> attempt, T1 arg1, T2 arg2, T3 arg3)
        {
            TResult result;
            return attempt.VerifyArgument(nameof(attempt)).IsNotNull().Value.Invoke(arg1, arg2, arg3, out result) ? Optional.From(result) : Optional<TResult>.Undefined;
        }
        public static Optional<TResult> Invoke<T1, T2, T3, T4, TResult>(this Attempt<T1, T2, T3, T4, TResult> attempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            TResult result;
            return attempt.VerifyArgument(nameof(attempt)).IsNotNull().Value.Invoke(arg1, arg2, arg3, arg4, out result) ? Optional.From(result) : Optional<TResult>.Undefined;
        }

        public static Optional<TResult> Invoke<TResult>(this IEnumerable<Attempt<TResult>> attempts)
        {
            var res = default(TResult);
            if (Enumerable.Any<Attempt<TResult>>(attempts.VerifyArgument(nameof(attempts)).IsNotNull().Value, x => x(out res)))
            {
                return Optional.From(res);
            }
            return Optional<TResult>.Undefined;
        }
        public static Optional<TResult> Invoke<T, TResult>(this IEnumerable<Attempt<T, TResult>> attempts, T arg)
        {
            var res = default(TResult);
            if (Enumerable.Any<Attempt<T, TResult>>(attempts.VerifyArgument(nameof(attempts)).IsNotNull().Value, x => x(arg, out res)))
            {
                return Optional.From(res);
            }
            return Optional<TResult>.Undefined;
        }
        public static Optional<TResult> Invoke<T1, T2, TResult>(this IEnumerable<Attempt<T1, T2, TResult>> attempts, T1 arg1, T2 arg2)
        {
            var res = default(TResult);
            if (Enumerable.Any<Attempt<T1, T2, TResult>>(attempts.VerifyArgument(nameof(attempts)).IsNotNull().Value, x => x(arg1, arg2, out res)))
            {
                return Optional.From(res);
            }
            return Optional<TResult>.Undefined;
        }
        public static Optional<TResult> Invoke<T1, T2, T3, TResult>(this IEnumerable<Attempt<T1, T2, T3, TResult>> attempts, T1 arg1, T2 arg2, T3 arg3)
        {
            var res = default(TResult);
            if (Enumerable.Any<Attempt<T1, T2, T3, TResult>>(attempts.VerifyArgument(nameof(attempts)).IsNotNull().Value, x => x(arg1, arg2, arg3, out res)))
            {
                return Optional.From(res);
            }
            return Optional<TResult>.Undefined;
        }
        public static Optional<TResult> Invoke<T1, T2, T3, T4, TResult>(this IEnumerable<Attempt<T1, T2, T3, T4, TResult>> attempts, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var res = default(TResult);
            if (Enumerable.Any<Attempt<T1, T2, T3, T4, TResult>>(attempts.VerifyArgument(nameof(attempts)).IsNotNull().Value, x => x(arg1, arg2, arg3, arg4, out res)))
            {
                return Optional.From(res);
            }
            return Optional<TResult>.Undefined;
        }
    }
}