using System;
using System.Collections.Generic;


namespace Axle.Reflection
{
    /// <summary>
    /// Represents a reflected object that may be annotated with attributes.
    /// </summary>
    /// <seealso cref="Attribute"/>
    /// <seealso cref="IAttributeInfo"/>
    public interface IAttributeTarget
    {
        /// <summary>
        /// A collection of zero or more <see cref="IAttributeInfo">attributes</see> that the reflected object has.
        /// </summary>
        IEnumerable<IAttributeInfo> Attributes { get; }
    }
}