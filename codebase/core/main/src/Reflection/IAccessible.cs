using System.Collections.Generic;


namespace Axle.Reflection
{
    /// <summary>
    /// Represents a reflected object which has accessors.
    /// </summary>
    /// <seealso cref="AccessorType"/>
    public interface IAccessible
    {
        /// <summary>
        /// Locates an accessor of a given <paramref name="type"/> belonging to the current 
        /// reflected object.
        /// </summary>
        /// <param name="type">
        /// One of the <see cref="AccessorType"/> enumeration values, representing the accessor type
        /// to look for.
        /// </param>
        /// <returns>
        /// An <see cref="IAccessor"/> matching the provided accessor <paramref name="type"/>,
        /// or <c><see langword="null"/></c> if no matching accessor was found.
        /// </returns>
        IAccessor FindAccessor(AccessorType type);

        /// <summary>
        /// A collection of <see cref="IAccessor"/> objects associated wit the current reflected object.
        /// </summary>
        IEnumerable<IAccessor> Accessors { get; }
    }
}
