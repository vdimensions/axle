#if NETSTANDARD || NET35_OR_NEWER
#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

using Axle.Verification;


namespace Axle.Reflection
{
    public abstract class AbstractTypeIntrospector : ITypeIntrospector
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _introspectedType;
        
        protected AbstractTypeIntrospector(Type type)
        {
            _introspectedType = Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
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
            if (IsGenericTypeDefinition)
            {
                return new GenericTypeIntrospector(_introspectedType);
            }
            if (IsGenericType)
            {
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                return new GenericTypeIntrospector(_introspectedType.GetGenericTypeDefinition());
                #else
                return new GenericTypeIntrospector(_introspectedType.GetTypeInfo().GetGenericTypeDefinition());
                #endif
            }
            return null;

        }

        /// <inheritdoc />
        public TypeCode TypeCode => Type.GetTypeCode(_introspectedType);

        /// <inheritdoc />
        public Type IntrospectedType => _introspectedType;
        
        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        /// <inheritdoc />
        public bool IsDelegate
        {
            get
            {
                return typeof(MulticastDelegate)
                    #if NETSTANDARD || NET45_OR_NEWER
                    .GetTypeInfo()
                    .IsAssignableFrom(_introspectedType.GetTypeInfo().BaseType.GetTypeInfo())
                    #else
                    .IsAssignableFrom(_introspectedType.BaseType)
                    #endif
                    ;
            }
        }
        #endif
        

        #if NETSTANDARD || NET35_OR_NEWER
        /// <inheritdoc />
        public bool IsGenericType
        {
            get
            {
                #if NETSTANDARD || NET45_OR_NEWER
                var ti = _introspectedType.GetTypeInfo();
                return ti.IsGenericType;
                #else
                return _introspectedType.IsGenericType;
                #endif
            }
        }
        #endif

        #if NETSTANDARD || NET35_OR_NEWER
        /// <inheritdoc />
        public bool IsGenericTypeDefinition
        {
            get
            {
                #if NETSTANDARD || NET45_OR_NEWER
                var ti = _introspectedType.GetTypeInfo();
                return ti.IsGenericTypeDefinition;
                #else
                return _introspectedType.IsGenericTypeDefinition;
                #endif
            }
        }
        #endif

        #if NETSTANDARD || NET35_OR_NEWER
        /// <inheritdoc />
        public bool IsNullableType
        {
            get
            {
                #if NETSTANDARD || NET45_OR_NEWER
                var ti = _introspectedType.GetTypeInfo();
                return ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(Nullable<>);
                #else
                return _introspectedType.IsGenericType && _introspectedType.GetGenericTypeDefinition() == typeof (Nullable<>);
                #endif
            }
        }
        #endif

        #if NETSTANDARD2_0 || NETFRAMEWORK
        public bool IsEnum => _introspectedType.IsEnum;
        #else
        public bool IsEnum => _introspectedType.GetTypeInfo().IsEnum;
        #endif

        /// <inheritdoc />
        #if NETSTANDARD2_0 || NETFRAMEWORK
        public bool IsAbstract => _introspectedType.IsAbstract;
        #else
        public bool IsAbstract => _introspectedType.GetTypeInfo().IsAbstract;
        #endif
    }
}
#endif
#endif