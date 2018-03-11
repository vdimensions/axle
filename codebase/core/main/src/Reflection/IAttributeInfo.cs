using System;


namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected attribute. Allows querying of reflected metadata.
    /// </summary>
    public interface IAttributeInfo
    {
        /// <summary>
        /// Gets a reference to the reflected <see cref="Attribute">attribute</see> instance. 
        /// </summary>
        Attribute Attribute { get; }

        /// <summary>
        /// Gets a set of values identifying which program elements the indicated attribute can be applied to.
        /// </summary>
        /// <seealso cref="AttributeUsageAttribute.ValidOn"/>
        AttributeTargets AttributeTargets { get; }

        /// <summary>
        /// Gets or sets a <see cref="bool"/> value indicating whether more than one instance of the indicated attribute can be specified for a single program element.
        /// </summary>
        /// <seealso cref="AttributeUsageAttribute.AllowMultiple"/>
        bool AllowMultiple { get; }

        /// <summary>
        /// Gets or sets a <see cref="bool"/> value indicating whether the indicated attribute can be inherited by derived classes and overriding members.
        /// </summary>
        /// <seealso cref="AttributeUsageAttribute.Inherited"/>
        bool Inherited { get; }
    }
}