#if NETSTANDARD || NET35_OR_NEWER
#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

using Axle.Verification;


namespace Axle.Reflection
{
    public abstract class AbstractTypeIntrospector : ITypeIntrospector
    {
        private static TypeCategories GetTypeCategories(Type type)
        {
            var result = TypeCategories.Unknown;

            if (typeof(MulticastDelegate)
                    #if NETSTANDARD || NET45_OR_NEWER
                    .GetTypeInfo()
                    .IsAssignableFrom(type.GetTypeInfo().BaseType.GetTypeInfo())
                    #else
                    .IsAssignableFrom(type.BaseType)
                    #endif
                    )
            {
                result |= TypeCategories.Delegate;
            }

            if (typeof(Attribute)
                    #if NETSTANDARD || NET45_OR_NEWER
                    .GetTypeInfo()
                    .IsAssignableFrom(type.GetTypeInfo().BaseType.GetTypeInfo())
                    #else
                    .IsAssignableFrom(type.BaseType)
                    #endif
                    )
            {
                result |= TypeCategories.Attribute;
            }

            #if NETSTANDARD || NET45_OR_NEWER
            if (type.GetTypeInfo().IsGenericType)
            #else
            if (type.IsGenericType)
            #endif
            {
                result |= TypeCategories.Generic;
            }

            #if NETSTANDARD || NET45_OR_NEWER
            if (type.GetTypeInfo().IsGenericTypeDefinition)
            #else
            if (type.IsGenericTypeDefinition)
            #endif
            {
                result |= TypeCategories.GenericDefinition;
            }

            #if NETSTANDARD2_0 || NETFRAMEWORK
            if (type.IsValueType)
            #else
            if (type.GetTypeInfo().IsValueType)
            #endif
            {
                result |= TypeCategories.ValueType;
            }
            else
            {
                result |= TypeCategories.ReferenceType;
            }

            #if NETSTANDARD || NET45_OR_NEWER
            var ti = type.GetTypeInfo();
            if (ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(Nullable<>))
            #else
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))
            #endif
            {
                result |= TypeCategories.NullableValueType;
            }

            #if NETSTANDARD2_0 || NETFRAMEWORK
            if (type.IsAbstract)
            #else
            if (type.GetTypeInfo().IsAbstract)
            #endif
            {
                result |= TypeCategories.Abstract;
            }

            #if NETSTANDARD2_0 || NETFRAMEWORK
            if (type.IsSealed)
            #else
            if (type.GetTypeInfo().IsSealed)
            #endif
            {
                result |= TypeCategories.Sealed;
            }

            #if NETSTANDARD2_0 || NETFRAMEWORK
            if (type.IsInterface)
            #else
            if (type.GetTypeInfo().IsInterface)
            #endif
            {
                result |= TypeCategories.Interface;
            }

            #if NETSTANDARD2_0 || NETFRAMEWORK
            if (type.IsArray)
            #else
            if (type.GetTypeInfo().IsArray)
            #endif
            {
                result |= TypeCategories.Array;
            }

            #if NETSTANDARD2_0 || NETFRAMEWORK
            if (type.IsEnum)
            #else
            if (type.GetTypeInfo().IsEnum)
            #endif
            {
                result |= TypeCategories.Enum;
            }

            return result;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _introspectedType;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly TypeCategories _categories;

        protected AbstractTypeIntrospector(Type type)
        {
            _introspectedType = Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            _categories = GetTypeCategories(type);
        }

        public abstract IAttributeInfo[] GetAttributes();
        public abstract IAttributeInfo[] GetAttributes(Type attributeType);
        public abstract IAttributeInfo[] GetAttributes(Type attributeType, bool inherit);

        /// <inheritdoc />
        public abstract IConstructor GetConstructor(ScanOptions scanOptions, params Type[] argumentTypes);

        /// <inheritdoc />
        public abstract IConstructor GetConstructor(ConstructorInfo reflectedConstructor);

        /// <inheritdoc />
        public abstract IConstructor[] GetConstructors(ScanOptions scanOptions);

        /// <inheritdoc />
        public abstract IMethod GetMethod(ScanOptions scanOptions, string methodName);
        
        /// <inheritdoc />
        public abstract IMethod GetMethod(MethodInfo reflectedMethod);

        /// <inheritdoc />
        public abstract IMethod[] GetMethods(ScanOptions scanOptions);

        /// <inheritdoc />
        public abstract IProperty GetProperty(ScanOptions scanOptions, string propertyName);
        
        /// <inheritdoc />
        public abstract IProperty GetProperty(PropertyInfo reflectedProperty);

        /// <inheritdoc />
        public abstract IProperty[] GetProperties(ScanOptions scanOptions);

        /// <inheritdoc />
        public abstract IField GetField(ScanOptions scanOptions, string fieldName);
       
        /// <inheritdoc />
        public abstract IField GetField(FieldInfo reflectedField);

        /// <inheritdoc />
        public abstract IField[] GetFields(ScanOptions scanOptions);

        /// <inheritdoc />
        public abstract IEvent GetEvent(ScanOptions scanOptions, string eventName);

        /// <inheritdoc />
        public abstract IEvent GetEvent(EventInfo reflectedEvent);        

        /// <inheritdoc />
        public abstract IEvent[] GetEvents(ScanOptions scanOptions);

        /// <inheritdoc />
        public IMember[] GetMembers(ScanOptions scanOptions)
        {
            var constructors = GetConstructors(scanOptions);
            var methods = GetMethods(scanOptions);
            var props = GetProperties(scanOptions);
            var fields = GetFields(scanOptions);
            var events = GetEvents(scanOptions);

            var result = new List<IMember>(constructors.Length + methods.Length + props.Length + fields.Length + events.Length);

            for (var i = 0; i < constructors.Length; i++)
            {
                result.Add(constructors[i]);
            }
            for (var i = 0; i < methods.Length; i++)
            {
                result.Add(methods[i]);
            }
            for (var i = 0; i < props.Length; i++)
            {
                result.Add(props[i]);
            }
            for (var i = 0; i < fields.Length; i++)
            {
                result.Add(fields[i]);
            }
            for (var i = 0; i < events.Length; i++)
            {
                result.Add(events[i]);
            }

            return result.ToArray();
        }

        /// <inheritdoc />
        bool IAttributeTarget.IsDefined(Type attributeType, bool inherit) => IsAttributeDefined(attributeType, inherit);

        /// <inheritdoc />
        public abstract bool IsAttributeDefined(Type attributeType, bool inherit);
        
        /// <inheritdoc />
        public IGenericTypeIntrospector GetGenericTypeDefinition()
        {
            if ((Categories & TypeCategories.GenericDefinition) == TypeCategories.GenericDefinition)
            {
                return new GenericTypeIntrospector(_introspectedType, null);
            }
            if ((Categories & TypeCategories.Generic) == TypeCategories.Generic)
            {
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                var ti = _introspectedType;
                #else
                var ti = _introspectedType.GetTypeInfo();
                #endif
                return new GenericTypeIntrospector(ti.GetGenericTypeDefinition(), null)
                    .MakeGenericType(ti.GetGenericArguments());
            }
            return null;

        }

        /// <inheritdoc />
        public TypeCode TypeCode => Type.GetTypeCode(_introspectedType);

        /// <inheritdoc />
        public Type IntrospectedType => _introspectedType;

        /// <inheritdoc />
        public TypeCategories Categories => _categories;




        
        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        /// <inheritdoc />
        public bool IsDelegate => TypeCategories.Delegate == (_categories & TypeCategories.Delegate);
        #endif
        
        #if NETSTANDARD || NET35_OR_NEWER
        /// <inheritdoc />
        public bool IsGenericType => TypeCategories.Generic == (_categories & TypeCategories.Generic);
        #endif

        #if NETSTANDARD || NET35_OR_NEWER
        /// <inheritdoc />
        public bool IsGenericTypeDefinition 
            => TypeCategories.GenericDefinition == (_categories & TypeCategories.GenericDefinition);
        #endif

        #if NETSTANDARD || NET35_OR_NEWER
        /// <inheritdoc />
        public bool IsNullableType 
            => TypeCategories.NullableValueType == (_categories & TypeCategories.NullableValueType);
        #endif

        public bool IsEnum => TypeCategories.Enum == (_categories & TypeCategories.Enum);

        /// <inheritdoc />
        public bool IsAbstract => TypeCategories.Abstract == (_categories & TypeCategories.Abstract);
    }
}
#endif
#endif