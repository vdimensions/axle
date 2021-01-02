using System;
using System.Collections.Generic;
using Axle.Verification;

namespace Axle.Collections.Generic.Extensions.KeyValuePair
{
    /// <summary>
    /// A static class containing extension methods for <see cref="System.Collections.Generic.KeyValuePair{TKey,TValue}"/>
    /// </summary>
    public static class KeyValuePairExtensions
    {
        /// <summary>
        /// Maps the key of a <see cref="KeyValuePair{TKey,TValue}"/> using the provided
        /// <paramref name="mappingFunc"/>.
        /// </summary>
        /// <param name="pair">
        /// The <see cref="KeyValuePair{TKey,TValue}"/> to map the key of.
        /// </param>
        /// <param name="mappingFunc">
        /// A <see cref="Func{TValue,TMappedValue}"/> instance that will be used to map the key of the
        /// <paramref name="pair"/>.
        /// </param>
        /// <typeparam name="TKey">
        /// The original key type of the <see cref="KeyValuePair{TKey,TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TMappedKey">
        /// The mapped key type of the resulting <see cref="KeyValuePair{TKey,TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The value type of the <see cref="KeyValuePair{TKey,TValue}"/>.
        /// </typeparam>
        /// <returns>
        /// A new <see cref="KeyValuePair{TKey,TMappedValue}"/> where the <see cref="KeyValuePair{TKey,TValue}.Key"/>
        /// property is the result of applying the <paramref name="mappingFunc"/> against the
        /// <see cref="KeyValuePair{TKey,TValue}.Key"/> property of the original <paramref name="pair" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="mappingFunc"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The new key produced by invoking the <paramref name="mappingFunc"/> is <c>null</c>.
        /// </exception>
        public static KeyValuePair<TMappedKey, TValue> MapKey<TKey, TMappedKey, TValue>(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            KeyValuePair<TKey, TValue> pair,
            Func<TKey, TMappedKey> mappingFunc)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(mappingFunc, nameof(mappingFunc)));
            var mappedKey = mappingFunc(pair.Key);
            if (mappedKey == null)
            {
                throw new InvalidOperationException("The resulting key cannot be null");
            }
            return new KeyValuePair<TMappedKey, TValue>(mappedKey, pair.Value);
        }
        /// <summary>
        /// Maps the value of a <see cref="KeyValuePair{TKey,TValue}"/> using the provided
        /// <paramref name="mappingFunc"/>.
        /// </summary>
        /// <param name="pair">
        /// The <see cref="KeyValuePair{TKey,TValue}"/> to map the value of.
        /// </param>
        /// <param name="mappingFunc">
        /// A <see cref="Func{TValue,TMappedValue}"/> instance that will be used to map the value of the
        /// <paramref name="pair"/>.
        /// </param>
        /// <typeparam name="TKey">
        /// The key type of the <see cref="KeyValuePair{TKey,TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The original value type of the <see cref="KeyValuePair{TKey,TValue}"/>.
        /// </typeparam>
        /// <typeparam name="TMappedValue">
        /// The mapped value type of the resulting <see cref="KeyValuePair{TKey,TValue}"/>.
        /// </typeparam>
        /// <returns>
        /// A new <see cref="KeyValuePair{TKey,TMappedValue}"/> where the <see cref="KeyValuePair{TKey,TValue}.Value"/>
        /// property is the result of applying the <paramref name="mappingFunc"/> against the
        /// <see cref="KeyValuePair{TKey,TValue}.Value"/> property of the original <paramref name="pair" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="mappingFunc"/> is <c>null</c>.
        /// </exception>
        public static KeyValuePair<TKey, TMappedValue> MapValue<TKey, TValue, TMappedValue>(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            KeyValuePair<TKey, TValue> pair,
            Func<TValue, TMappedValue> mappingFunc)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(mappingFunc, nameof(mappingFunc)));
            return new KeyValuePair<TKey, TMappedValue>(pair.Key, mappingFunc(pair.Value));
        }
    }
}