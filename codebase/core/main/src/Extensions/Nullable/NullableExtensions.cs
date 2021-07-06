using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Extensions.Nullable
{
    /// <summary>
    /// A static class containing common extension methods for nullable types.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class NullableExtensions
    {
        /// <summary>
        /// If the current <paramref name="nullable"/> object has a valid value, apply the provided
        /// <paramref name="mappingFn">mapping function</paramref> to it, and if the result is non-null, return a
        /// <see cref="System.Nullable{TResult}"/> describing the result.
        /// Otherwise return an empty <see cref="System.Nullable{TResult}"/>.
        /// </summary>
        /// <param name="nullable">
        /// The <see cref="System.Nullable{T}"/> object to apply mapping on.
        /// </param>
        /// <param name="mappingFn">
        /// A <see cref="Func{T,TResult}"/> that maps a value of type <typeparamref name="T"/> to a value of type
        /// <typeparamref name="TResult"/>.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable{TResult}"/> value representing either a valid result of the
        /// <paramref name="mappingFn">mapping function</paramref>, or an empty <see cref="System.Nullable{TResult}"/>. 
        /// </returns>
        /// <typeparam name="T">
        /// The underlying type of the provided <paramref name="nullable"/> value.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The result type of the <paramref name="mappingFn">mapping function</paramref>. 
        /// </typeparam>
        public static TResult? Map<T, TResult>(this T? nullable, Func<T, TResult> mappingFn)
            where T: struct
            where TResult: struct
        {
            return nullable.HasValue ? (TResult?) mappingFn(nullable.Value) : null;
        }
        /// <summary>
        /// If the current <paramref name="nullable"/> object has a valid value, apply the provided
        /// <paramref name="mappingFn">mapping function</paramref> to it, and return a
        /// <see cref="System.Nullable{TResult}"/> describing the result.
        /// </summary>
        /// <param name="nullable">
        /// The <see cref="System.Nullable{T}"/> object to apply mapping on.
        /// </param>
        /// <param name="mappingFn">
        /// A <see cref="Func{T,TResult}"/> that maps a value of type <typeparamref name="T"/> to a value of type
        /// <see cref="System.Nullable{TResult}"/>.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable{TResult}"/> value representing either a valid result of the
        /// <paramref name="mappingFn">mapping function</paramref>, or an empty <see cref="System.Nullable{TResult}"/>. 
        /// </returns>
        /// <typeparam name="T">
        /// The underlying type of the provided <paramref name="nullable"/> value.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The result type of the <paramref name="mappingFn">mapping function</paramref>. 
        /// </typeparam>
        public static TResult? FlatMap<T, TResult>(this T? nullable, Func<T, TResult?> mappingFn)
            where T: struct
            where TResult: struct
        {
            return nullable.HasValue ? mappingFn(nullable.Value) : null;
        }
    }
}