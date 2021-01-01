#if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Axle.Verification;

namespace Axle.Threading.Extensions.AggregateException
{
    using AggregateException = System.AggregateException;

    /// <summary>
    /// A static class containing extension methods for the <see cref="AggregateException"/> class.
    /// </summary>
    public static class AggregateExceptionExtensions
    {
        private static IEnumerable<Exception> Resolve(AggregateException e, ICollection<Exception> result)
        {
            for (var i = 0; i < e.InnerExceptions.Count; i++)
            {
                var innerException = e.InnerExceptions[i];
                switch (innerException)
                {
                    case TaskCanceledException _:
                        continue;
                    case AggregateException aggEx:
                        foreach (var exception in Resolve(aggEx, result))
                        {
                            result.Add(exception);
                        }

                        break;
                    default:
                        result.Add(innerException);
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Expands the list of exception causes for an <see cref="AggregateException"/>.
        /// </summary>
        /// <param name="e">
        /// The <see cref="AggregateException"/> instance to expand.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Exception"/> instances, that are collectively responsible for the throwing of
        /// the <see cref="AggregateException"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="e"/> is <c>null</c>
        /// </exception>
        public static IEnumerable<Exception> Resolve(this AggregateException e)
        {
            e.VerifyArgument(nameof(e)).IsNotNull();
            return Resolve(e, new LinkedList<Exception>());
        }

        /// <summary>
        /// Expands the list of exception causes for an <see cref="AggregateException"/> and returns the first
        /// exception.
        /// </summary>
        /// <param name="e">
        /// The <see cref="AggregateException"/> instance to resolve.
        /// </param>
        /// <returns>
        /// The first <see cref="Exception"/> that was in the cause list of the provided
        /// <see cref="AggregateException"/> <paramref name="e"/>, or <c>null</c> if no exception causes were found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="e"/> is <c>null</c>
        /// </exception>
        public static Exception ResolveFirst(this AggregateException e)
        {
            e.VerifyArgument(nameof(e)).IsNotNull();
            var exceptions = new List<Exception>();
            Resolve(e, exceptions);
            return exceptions.Count > 0 ? exceptions[0] : null;
        }
    }
}
#endif