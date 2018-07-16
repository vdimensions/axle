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
        /// Indicates whether one or more attributes of the specified <see cref="Type">type</see> or of its derived
        /// types is applied to this <see cref="IAttributeTarget">attribute target</see> instance.
        /// </summary>
        /// <param name="attributeType">
        /// The <see cref="Type">type</see> of custom <see cref="Attribute">attribute</see> to search for. The search includes derived types.
        /// </param>
        /// <param name="inherit">
        /// <c>true</c> to search this member's inheritance chain to find the attributes; otherwise, <c>false</c>.
        /// <para>
        /// <remarks>
        /// The <paramref name="inherit"/> parameter is ignored for <see cref="IAttributeTarget">attribute target</see>
        /// implementations representing properties and events.
        /// </remarks>
        /// </para>
        /// </param>
        /// <returns>
        /// <c>true</c> if one or more instances of <paramref name="attributeType"/> or any of its derived types
        /// is applied to this <see cref="IAttributeTarget">attribute target</see> instance; otherwise, <c>false</c>.
        /// </returns>
        bool IsDefined(Type attributeType, bool inherit);

        /// <summary>
        /// A collection of zero or more <see cref="IAttributeInfo">attributes</see> that the reflected object has.
        /// </summary>
        IEnumerable<IAttributeInfo> Attributes { get; }
    }
}