#if NETSTANDARD || NET20_OR_NEWER
using System;
using Axle.Verification;

namespace Axle.Collections.Extensions.Array
{
    using Array = System.Array;

    /// <summary>
    /// A static class to contain common extension methods for working with arrays.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Creates an array of specific type out of an object array.
        /// </summary>
        /// <param name="array">
        /// The source array to be converted.
        /// </param>
        /// <param name="type">
        /// The destination type of the resulting array.
        /// </param>
        /// <returns>
        /// A new array of the provided <paramref name="type"/>.
        /// </returns>
        /// <exception cref="InvalidCastException">
        /// There is an element inside the <paramref name="array"/> which cannot be cast to the destination
        /// <paramref name="type"/>.
        /// </exception>
        public static Array MakeGeneric(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            object[] array, Type type)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(array, nameof(array)));
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));

            var destinationArray = Array.CreateInstance(type, array.Length);
            Array.Copy(array, destinationArray, array.Length);
            return destinationArray;
        }
    }
}
#endif