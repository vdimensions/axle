#if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System;

namespace Axle.Reflection
{
    internal sealed class GenericTypeIntrospector : AbstractGenericTypeIntrospector
    {
        public GenericTypeIntrospector(
                Type genericDefinitionType, 
                IGenericTypeIntrospector rawTypeIntrospector, 
                params Type[] genericTypeArguments) 
            : base(genericDefinitionType, rawTypeIntrospector, genericTypeArguments) { }
        
        public override ITypeIntrospector Introspect()
        {
            var size = GenericArguments.Length;
            return size == GenericTypeArguments.Length 
                ? new TypeIntrospector(GenericDefinitionType.MakeGenericType(GenericTypeArguments)) 
                : null;
        }

        public override IGenericTypeIntrospector MakeGenericType(params Type[] typeParameters)
        {
            if (typeParameters.Length == 0)
            {
                return this;
            }

            var concatenatedTypes = new Type[typeParameters.Length + GenericTypeArguments.Length];
            GenericTypeArguments.CopyTo(concatenatedTypes, 0);
            typeParameters.CopyTo(concatenatedTypes,GenericTypeArguments.Length);
            if (concatenatedTypes.Length > GenericArguments.Length)
            {
                throw new InvalidOperationException("You have provided more types than the supported number of generic type arguments.");
            }
            return new GenericTypeIntrospector(GenericDefinitionType, GenericDefinitionIntrospector, concatenatedTypes);
        }

        private Type[] GenericArguments
        {
            get
            {
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
                return GenericDefinitionType.GetGenericArguments();
                #else
                return System.Reflection.IntrospectionExtensions.GetTypeInfo(GenericDefinitionType).GetGenericArguments();
                #endif
            }
        }
    }
}
#endif
#endif