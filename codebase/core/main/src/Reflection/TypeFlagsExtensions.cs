using System;
using System.Collections;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif

namespace Axle.Reflection
{
    /// <summary>
    /// A static class containing extension methods for the <see cref="TypeFlags"/> enum.
    /// </summary>
    public static class TypeFlagsExtensions
    {
        internal static TypeFlags Determine(Type type)
        {
            var result = TypeFlags.Unknown;
            
            #if NETSTANDARD || NET45_OR_NEWER
            var ti = type.GetTypeInfo();
            #else
            var ti = type;
            #endif

            if (ti.IsGenericType)
            {
                result |= TypeFlags.Generic;
            }

            if (ti.IsGenericTypeDefinition)
            {
                result |= TypeFlags.GenericDefinition;
            }

            if (ti.IsValueType)
            {
                result |= TypeFlags.ValueType;

                if (ti.IsEnum)
                {
                    result |= TypeFlags.Enum;
                }
                
                if (ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    result |= TypeFlags.NullableValueType;
                }
            }
            else
            {
                result |= TypeFlags.ReferenceType;
                
                if (ti.IsAbstract)
                {
                    result |= TypeFlags.Abstract;
                }

                if (ti.IsSealed)
                {
                    result |= TypeFlags.Sealed;
                }

                if (ti.IsInterface)
                {
                    result |= TypeFlags.Interface;
                }
                else if (ti.BaseType != null)
                {
                    if (typeof(Attribute)
                        #if NETSTANDARD || NET45_OR_NEWER
                        .GetTypeInfo()
                        .IsAssignableFrom(ti.BaseType.GetTypeInfo())
                        #else
                        .IsAssignableFrom(ti.BaseType)
                        #endif
                        )
                    {
                        result |= TypeFlags.Attribute;
                    }
                    else if (typeof(Delegate)
                        #if NETSTANDARD || NET45_OR_NEWER
                        .GetTypeInfo()
                        .IsAssignableFrom(ti.BaseType.GetTypeInfo())
                        #else
                        .IsAssignableFrom(ti.BaseType)
                        #endif
                        )
                    {
                        result |= TypeFlags.Delegate;
                    }
                }
            }

            if (ti.IsArray)
            {
                result |= TypeFlags.Array;
            }
            else if (typeof(IEnumerable)
                #if NETSTANDARD || NET45_OR_NEWER
                .GetTypeInfo()
                #endif
                .IsAssignableFrom(ti)
                )
            {
                result |= TypeFlags.Enumerable;
            }

            if (typeof(IDisposable)
                #if NETSTANDARD || NET45_OR_NEWER
                .GetTypeInfo()
                #endif
                .IsAssignableFrom(ti)
                )
            {
                result |= TypeFlags.Disposable;
            }

            if (ti.IsGenericParameter)
            {
                result |= TypeFlags.GenericParameter;
            }

            if (ti.IsNested)
            {
                result |= TypeFlags.Nested;
            }

            return result;
        }
        
        /// Checks if a particular <paramref name="flag"/> is set.
        public static bool HasFlag(this TypeFlags value, TypeFlags flag) => (value & flag) == flag;
        
        /// Checks if the <see cref="TypeFlags.Abstract"/> flag is set.
        public static bool IsAbstract(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.Abstract);
        
        /// Checks if the <see cref="TypeFlags.Array"/> flag is set.
        public static bool IsArray(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.Array);
        
        /// Checks if the <see cref="TypeFlags.Attribute"/> flag is set.
        public static bool IsAttribute(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.Attribute);
        
        /// Checks if the <see cref="TypeFlags.Delegate"/> flag is set.
        public static bool IsDelegate(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.Delegate);

        /// Checks if the <see cref="TypeFlags.Disposable"/> flag is set.
        public static bool IsDisposable(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.Disposable);

        /// Checks if the <see cref="TypeFlags.Enum"/> flag is set.
        public static bool IsEnum(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.Enum);

        /// Checks if the <see cref="TypeFlags.Enumerable"/> flag is set.
        public static bool IsEnumerable(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.Enumerable);

        /// Checks if the <see cref="TypeFlags.Generic"/> flag is set.
        public static bool IsGeneric(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.Generic);
        
        /// Checks if the <see cref="TypeFlags.GenericDefinition"/> flag is set.
        public static bool IsGenericDefinition(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.GenericDefinition);

        /// Checks if the <see cref="TypeFlags.GenericParameter"/> flag is set.
        public static bool IsGenericParameter(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.GenericParameter);

        /// Checks if the <see cref="TypeFlags.Interface"/> flag is set.
        public static bool IsInterface(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.Interface);

        /// Checks if the <see cref="TypeFlags.Nested"/> flag is set.
        public static bool IsNested(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.Nested);

        /// Checks if the <see cref="TypeFlags.NullableValueType"/> flag is set.
        public static bool IsNullableValueType(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.NullableValueType);
        
        /// Checks if the <see cref="TypeFlags.ReferenceType"/> flag is set.
        public static bool IsReferenceType(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.ReferenceType);
        
        /// Checks if the <see cref="TypeFlags.Sealed"/> flag is set.
        public static bool IsSealed(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.Sealed);
        
        /// Checks if the <see cref="TypeFlags.Static"/> flag is set.
        public static bool IsStatic(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.Static);
        
        /// Checks if the <see cref="TypeFlags.ValueType"/> flag is set.
        public static bool IsValueType(this TypeFlags typeFlags) => HasFlag(typeFlags, TypeFlags.ValueType);
    }
}