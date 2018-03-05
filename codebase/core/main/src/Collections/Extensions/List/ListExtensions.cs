using System;
using System.Collections;
using System.Collections.Generic;

using Axle.Collections.Generic;
using Axle.Verification;


namespace Axle.Collections.Extensions.List
{
    /// <summary>
    /// A static class to hold extension methods for list collections.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Converts a given <see cref="IList"/> instance to its generic <see cref="IList{T}"/> equivalent.
        /// </summary>
        /// <param name="list">
        /// The <see cref="IList"/> instance to be covnerted.
        /// </param>
        /// <param name="type">
        /// The type to be used as generic type argument for the generic list.
        /// </param>
        /// <returns>
        /// A new <see cref="IList"/> instance that represens a generic <see cref="IList{T}"/> with the generic type being the one
        /// provided by the <paramref name="type"/> parameter.
        /// </returns>
        public static IList MakeGeneric(this IList list, Type type)
        {
            list.VerifyArgument(nameof(list)).IsNotNull();
            type.VerifyArgument(nameof(type)).IsNotNull();

            if (type == typeof(object))
            {
                /*
                 * If the given type is object, we do not actually need to copy the list,
                 * we can just wrap it around a generic decorator.
                 */
                return list is IList<object> ? list : new GenericList<object>(list);
            }
            
            var result = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
            foreach (var element in list)
            {
                result.Add(element);
            }
            return result;
        }
    }
}
