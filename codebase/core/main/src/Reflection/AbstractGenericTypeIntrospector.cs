#if NETSTANDARD || NET35_OR_NEWER
#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using Axle.Verification;

namespace Axle.Reflection
{
    public abstract class AbstractGenericTypeIntrospector : IGenericTypeIntrospector
    {
        private readonly IGenericTypeIntrospector _rawTypeIntrospector;

        protected AbstractGenericTypeIntrospector(
                Type genericDefinitionType, 
                IGenericTypeIntrospector rawTypeIntrospector, 
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
        }

        public abstract ITypeIntrospector GetIntrospector();
        public abstract IGenericTypeIntrospector MakeGenericType(params Type[] typeParameters);

        public Type[] GenericTypeArguments { get; }
        public Type GenericDefinitionType { get; }
        public IGenericTypeIntrospector RawTypeIntrospector { get; }
    }
}
#endif
#endif