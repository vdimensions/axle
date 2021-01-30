#if NETSTANDARD || NET35_OR_NEWER
#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using Axle.Verification;

namespace Axle.Reflection
{
    /// <summary>
    /// An abstract class aiding the implementation of the <see cref="IGenericTypeIntrospector"/> interface.
    /// </summary>
    public abstract class AbstractGenericTypeIntrospector : IGenericTypeIntrospector
    {
        protected AbstractGenericTypeIntrospector(
                Type genericDefinitionType, 
                IGenericTypeIntrospector genericDefinitionIntrospector, 
                Type[] genericTypeArguments)
        {
            Verifier.IsTrue(
                Verifier.IsNotNull(Verifier.VerifyArgument(genericDefinitionType, nameof(genericDefinitionType))), 
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                x => x.IsGenericTypeDefinition, 
                #else
                x => System.Reflection.IntrospectionExtensions.GetTypeInfo(x).IsGenericTypeDefinition,
                #endif
                "The provided type must be a generic type definition.");
            GenericDefinitionType = genericDefinitionType;
            GenericTypeArguments = genericTypeArguments;
            GenericDefinitionIntrospector = genericDefinitionIntrospector;
        }

        /// <inheritdoc />
        public abstract ITypeIntrospector Introspect();

        /// <inheritdoc />
        public abstract IGenericTypeIntrospector MakeGenericType(params Type[] typeParameters);

        /// <inheritdoc />
        public Type GenericDefinitionType { get; }

        /// <inheritdoc />
        public Type[] GenericTypeArguments { get; }

        /// <inheritdoc />
        public IGenericTypeIntrospector GenericDefinitionIntrospector { get; }
    }
}
#endif
#endif