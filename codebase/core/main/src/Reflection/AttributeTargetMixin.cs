using System;
using Axle.Verification;

namespace Axle.Reflection
{
    /// <summary>
    /// A static class providing extension methods to implementations of the <see cref="IAttributeTarget"/> interface.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public static class AttributeTargetMixin
    {
        /// <summary>
        /// Gets a collection of zero or more attributes of the provided by the generic type parameter
        /// <typeparamref name="T"/>, that the <see cref="IAttributeTarget"/> instance has defined.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the attributes to look for. Must inherit <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="attributeTarget">
        /// The <see cref="IAttributeTarget"/> instance this extension method is invoked on.
        /// </param>
        /// <returns>
        /// A collection of zero or more attributes of the provided by the generic type parameter
        /// <typeparamref name="T"/>, that the <see cref="IAttributeTarget"/> instance has defined.
        /// </returns>
        public static IAttributeInfo[] GetAttributes<T>(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this 
            #endif
            IAttributeTarget attributeTarget) where T: Attribute
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(attributeTarget, nameof(attributeTarget)));
            return attributeTarget.GetAttributes(typeof(T));
        }

        /// <summary>
        /// Gets a collection of zero or more attributes of the provided by the generic type parameter
        /// <typeparamref name="T"/>, that the <see cref="IAttributeTarget"/> instance has defined, and
        /// including/excluding attributes from base types depending on the value of the <paramref name="inherit"/>
        /// parameter.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the attributes to look for. Must inherit <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="attributeTarget">
        /// The <see cref="IAttributeTarget"/> instance this extension method is invoked on.
        /// </param>
        /// <param name="inherit">
        /// A <see cref="bool">boolean</see> value indicating whether to include attributes defined
        /// on a supertype of the type represented by the <see cref="IAttributeTarget"/>.
        /// </param>
        /// <returns>
        /// A collection of zero or more attributes of the provided by the generic type parameter
        /// <typeparamref name="T"/>, that the <see cref="IAttributeTarget"/> instance has defined, and
        /// including/excluding attributes from base types depending on the value of the <paramref name="inherit"/>
        /// parameter.
        /// </returns>
        public static IAttributeInfo[] GetAttributes<T>(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this 
            #endif
            IAttributeTarget attributeTarget, bool inherit) where T: Attribute
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(attributeTarget, nameof(attributeTarget)));
            return attributeTarget.GetAttributes(typeof(T), inherit);
        }

        /// <summary>
        /// Determines whether a given attribute type is defined for the introspected by the
        /// current <see cref="IAttributeTarget"/> implementation.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the attribute to look for. Must inherit <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="attributeTarget">
        /// The <see cref="IAttributeTarget"/> instance this extension method is invoked on.
        /// </param>
        /// <param name="inherit">
        /// A <see cref="bool">boolean</see> value indicating whether to include attributes defined
        /// on a supertype of the type represented by the <see cref="IAttributeTarget"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if an attribute of the specified type and matching inheritance criteria is found for the
        /// introspected type; <c>false</c> otherwise.
        /// </returns>
        public static bool IsDefined<T>(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this 
            #endif
            IAttributeTarget attributeTarget, bool inherit) where T : Attribute
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(attributeTarget, nameof(attributeTarget)));
            return attributeTarget.IsDefined(typeof(T), inherit);
        }
    }
}