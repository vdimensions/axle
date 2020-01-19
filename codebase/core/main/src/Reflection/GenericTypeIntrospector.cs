#if NETSTANDARD || NET35_OR_NEWER
#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;

namespace Axle.Reflection
{
    internal sealed class GenericTypeIntrospector : AbstractGenericTypeIntrospector
    {
        public GenericTypeIntrospector(Type genericDefinitionType, params Type[] genericTypeArguments) 
            : base(genericDefinitionType, genericTypeArguments) { }
        
        public override ITypeIntrospector GetIntrospector()
        {
            var size = GenericTypeConstraints.Length;
            return size == GenericTypeArguments.Length 
                ? new TypeIntrospector(GenericDefinitionType.MakeGenericType(GenericTypeArguments)) 
                : null;
        }

        public override IGenericTypeIntrospector MakeGenericType(params Type[] typeParameters)
        {
            var concatenatedTypes = new Type[typeParameters.Length + GenericTypeArguments.Length];
            GenericTypeArguments.CopyTo(concatenatedTypes, 0);
            typeParameters.CopyTo(concatenatedTypes,GenericTypeArguments.Length);
            if (concatenatedTypes.Length > GenericTypeConstraints.Length)
            {
                throw new InvalidOperationException("You have provided more types than the supported number of generic type arguments.");
            }
            return new GenericTypeIntrospector(GenericDefinitionType, concatenatedTypes);
        }

        private Type[] GenericTypeConstraints
        {
            get
            {
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                return  GenericDefinitionType.GetGenericParameterConstraints();
                #else
                return System.Reflection.IntrospectionExtensions.GetTypeInfo(GenericDefinitionType).GetGenericParameterConstraints();
                #endif
            }
        }
    }
}
#endif
#endif