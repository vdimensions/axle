using System;


namespace Axle.Reflection
{
    /// <summary>
    /// Represents a reflected member of a <c>struct</c>, <c>class</c>, or <c>interface</c>.
    /// </summary>
    //[Maturity(CodeMaturity.Stable)]
    public interface IMember
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
        /// The name of the reflected <see cref="IMember">member</see>.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the declaration type for the current member. 
        /// </summary>
        DeclarationType Declaration { get; }
        
        /// <summary>
        /// Gets the access modifier of the current member. 
        /// </summary>
        AccessModifier AccessModifier { get; }
    }
}