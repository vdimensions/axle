using System;

using Axle.Verification;


namespace Axle.Extensions.Collections.Array
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
        /// The source array to be covnerted. 
        /// </param>
        /// <param name="type">
        /// The destination type of the resulting array.
        /// </param>
        /// <returns>
        /// A new array of the provided <paramref name="type"/>.
        /// </returns>
        /// <exception cref="InvalidCastException">
        /// There is an element inside the <paramref name="array"/> which cannot be cast to the destination <paramref name="type"/>.
        /// </exception>
        public static Array MakeGeneric(this object[] array, Type type)
        {
            array.VerifyArgument(nameof(array)).IsNotNull();
            type.VerifyArgument(nameof(type)).IsNotNull();

            var destinationArray = Array.CreateInstance(type, array.Length);
            Array.Copy(array, destinationArray, array.Length);
            return destinationArray;
        }
    }
}
