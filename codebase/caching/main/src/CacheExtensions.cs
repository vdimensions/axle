using System;
using Axle.Verification;

namespace Axle.Caching
{
    /// <summary>
    /// A static class containing extension methods for the <see cref="ICacheManager"/> interface.
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// A generic version of the <see cref="ICache.GetOrAdd(object,object)"/>
        /// </summary>
        /// <param name="cache">
        /// The <see cref="ICache"/> to add values to.
        /// </param>
        /// <param name="key">
        /// The key to store a value for.
        /// </param>
        /// <param name="valueToAdd">
        /// The <typeparamref name="TValue"/> value object to store under <paramref name="key"/>.
        /// </param>
        /// <typeparam name="TKey">
        /// The type of the key object.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the value object.
        /// </typeparam>
        /// <returns>
        /// A <typeparamref name="TValue"/> object representing the stored value.
        /// </returns>
        /// <seealso cref="ICache.GetOrAdd(object,object)"/>
        public static TValue GetOrAdd<TKey, TValue>(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this 
            #endif
            ICache cache, TKey key, TValue valueToAdd)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(cache, nameof(cache)));
            Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key)));
            return (TValue) cache.GetOrAdd(key, valueToAdd);
        }
        /// <summary>
        /// A generic version of the <see cref="ICache.GetOrAdd(object,Func{object, object})"/>
        /// </summary>
        /// <param name="cache">
        /// The <see cref="ICache"/> to add values to.
        /// </param>
        /// <param name="key">
        /// The key to store a value for.
        /// </param>
        /// <param name="valueFactory">
        /// A delegate producing a <typeparamref name="TValue"/> instance to be stored in the current
        /// <see cref="ICache"/> instance, in case the cache did not already contain a value under
        /// <paramref name="key"/>.
        /// </param>
        /// <typeparam name="TKey">
        /// The type of the key object.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of the value object.
        /// </typeparam>
        /// <returns>
        /// A <typeparamref name="TValue"/> object representing the stored value.
        /// </returns>
        /// <seealso cref="ICache.GetOrAdd(object,Func{object, object})"/>
        public static TValue GetOrAdd<TKey, TValue>(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this 
            #endif
            ICache cache, TKey key, Func<TKey, TValue> valueFactory)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(cache, nameof(cache)));
            Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key)));
            Verifier.IsNotNull(Verifier.VerifyArgument(valueFactory, nameof(valueFactory)));
            return (TValue) cache.GetOrAdd(key, (k) => valueFactory((TKey) k));
        }
    }
}