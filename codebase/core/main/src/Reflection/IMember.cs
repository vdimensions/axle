using System;


namespace Axle.Reflection
{
    /// <summary>
    /// Represents a reflected member of a <c>struct</c>, <c>class</c>, or <c>interface</c>.
    /// This interface acts as a high-level abstraction over concrete member types, such as a constructor, method, 
    /// field, property and etc.
    /// </summary>
    /// <seealso cref="IConstructor"/>
    /// <seealso cref="IMethod"/>
    /// <seealso cref="IField"/>
    /// <seealso cref="IProperty"/>
    /// <seealso cref="IEvent"/>
    /// <seealso cref="IReadableMember"/>
    /// <seealso cref="IWriteableMember"/>
    public interface IMember : IAttributeTarget
    {
        /// <summary>
        /// The <see cref="Type">type</see> that declares the reflected <see cref="IMember">member</see>.
        /// </summary>
        Type DeclaringType { get; }

        /// <summary>
        /// The <see cref="Type">type</see> of the reflected <see cref="IMember">member</see> itself.
        /// </summary>
        Type MemberType { get; }

        /// <summary>
        /// The name of the current <see cref="IMember">member</see>.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the declaration type for the current <see cref="IMember">member</see>.
        /// </summary>
        DeclarationType Declaration { get; }

        /// <summary>
        /// Gets the access modifier of the current <see cref="IMember">member</see>.
        /// </summary>
        AccessModifier AccessModifier { get; }
    }
}