using System;

using Axle.Verification;


namespace Axle.Extensions.Type
{
    using Type = System.Type;

    /// <summary>
    /// A static class containing extension methods for instances of the <see cref="System.Type"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        #if !NETSTANDARD || NETSTANDARD1_5_OR_NEWER
        /// <summary>
        /// Gets the underlying <see cref="TypeCode">type code</see> of the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type">type</see> whose underlying <see cref="TypeCode">type code</see> to get.</param>
        /// <returns>
        /// The <see cref="TypeCode"/> value of the underlying type.
        /// </returns>
        public static TypeCode GetTypeCode(this Type type) => Type.GetTypeCode(type.VerifyArgument(nameof(type)).IsNotNull().Value);
        #endif
    }
}
