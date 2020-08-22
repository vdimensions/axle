using System;
using Axle.Verification;

namespace Axle.Caching
{
    public static class CacheExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this ICache cache, TKey key, TValue valueToAdd)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(cache, nameof(cache)));
            Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key)));
            return (TValue) cache.GetOrAdd(key, valueToAdd);
        }
        public static TValue GetOrAdd<TKey, TValue>(this ICache cache, TKey key, Func<TKey, TValue> valueFactory)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(cache, nameof(cache)));
            Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key)));
            Verifier.IsNotNull(Verifier.VerifyArgument(valueFactory, nameof(valueFactory)));
            return (TValue) cache.GetOrAdd(key, (k) => valueFactory((TKey) k));
        }
    }
}