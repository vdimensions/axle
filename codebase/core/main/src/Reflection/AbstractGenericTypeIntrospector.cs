#if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System;
using Axle.Verification;

namespace Axle.Reflection
{
    /// <summary>
    /// An abstract class aiding the implementation of the <see cref="IGenericTypeIntrospector"/> interface.
    /// </summary>
    public abstract class AbstractGenericTypeIntrospector : IGenericTypeIntrospector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractGenericTypeIntrospector"/> class.
        /// </summary>
        /// <param name="genericDefinitionType">
        /// The type representing the generic type definition being introspected. 
        /// </param>
        /// <param name="genericDefinitionIntrospector">
        /// An instance of <see cref="IGenericTypeIntrospector"/> that is used to handle generic type substitution.
        /// </param>
        /// <param name="genericTypeArguments">
        /// A <see cref="Type"/> array representing the current type substitutions of this
        /// <see cref="IGenericTypeIntrospector"/> implementation, or an empty array if the current instance represents
        /// a naked generic type.
        /// </param>
        protected AbstractGenericTypeIntrospector(
                Type genericDefinitionType, 
                IGenericTypeIntrospector genericDefinitionIntrospector, 
                Type[] genericTypeArguments)
        {
            Verifier.IsTrue(
                Verifier.IsNotNull(Verifier.VerifyArgument(genericDefinitionType, nameof(genericDefinitionType))), 
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
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