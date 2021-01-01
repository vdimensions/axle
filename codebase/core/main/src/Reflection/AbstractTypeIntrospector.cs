#if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

using Axle.Verification;

namespace Axle.Reflection
{
    /// <summary>
    /// An abstract class that serves as a base for implementing the <see cref="ITypeIntrospector"/> interface.
    /// </summary>
    public abstract class AbstractTypeIntrospector : ITypeIntrospector
    {
        #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        private static AccessModifier GetAccessModifier(Type type)
        {
            #if NETSTANDARD1_6_OR_NEWER || NET45_OR_NEWER
            var t = type.GetTypeInfo();
            #else
            var t = type;
            #endif
            var isPublic = t.IsPublic || t.IsNestedPublic;
            var isAssembly = t.IsNested ? t.IsNestedAssembly : t.IsNotPublic;
            var isFamily = t.IsNested && t.IsNestedFamily;
            var isPrivate = t.IsNested && t.IsNestedPrivate;
            return AccessModifierExtensions.GetAccessModifier(isPublic, isAssembly, isFamily, isPrivate);
        }
        #endif

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _introspectedType;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly TypeFlags _flags;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractTypeIntrospector"/> class using the provided
        /// <paramref name="type"/>.
        /// </summary>
        /// <param name="type">
        /// The type to be introspected by the current <see cref="AbstractTypeIntrospector"/> implementation.
        /// </param>
        protected AbstractTypeIntrospector(Type type)
        {
            _introspectedType = Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            _flags = TypeFlagsExtensions.Determine(type);
            #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
            AccessModifier = GetAccessModifier(type);
            #endif
        }

        /// <inheritdoc />
        public abstract IAttributeInfo[] GetAttributes();
        
        /// <inheritdoc />
        public abstract IAttributeInfo[] GetAttributes(Type attributeType);
        
        /// <inheritdoc />
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
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
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

        /// <summary>
        /// Determines whether a given attribute type is defined for the introspected by the
        /// current <see cref="ITypeIntrospector{T}"/> implementation.
        /// </summary>
        /// <param name="attributeType">
        /// The type of the attributed to look for.
        /// </param>
        /// <param name="inherit">
        /// A <see cref="bool">boolean</see> flag indicating whether to include attributes defined
        /// on a supertype of the introspected type.
        /// </param>
        /// <returns>
        /// <c>true</c> if an attribute of the specified type and matching inheritance criteria is found for the
        /// introspected type; <c>false</c> otherwise.
        /// </returns>
        public abstract bool IsAttributeDefined(Type attributeType, bool inherit);
        
        /// <inheritdoc />
        public IGenericTypeIntrospector GetGenericTypeDefinition()
        {
            if (_flags.IsGenericDefinition())
            {
                return new GenericTypeIntrospector(_introspectedType, null);
            }
            if (_flags.IsGeneric())
            {
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
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
        public object CreateInstance(params object[] args)
        {
            if (TypeFlags.IsAbstract())
            {
                throw new InvalidOperationException("Instances of abstract types cannot be instantiated");
            }
            if (TypeFlags.IsGenericDefinition())
            {
                throw new InvalidOperationException("Unable to instantiate a generic definition type. You need to supply type arguments first.");
            }
            return Activator.CreateInstance(IntrospectedType, args);
        }
        
        /// <inheritdoc />
        public ITypeIntrospector[] GetInterfaces()
        {
            return IntrospectedType
                #if NETSTANDARD
                .GetTypeInfo()
                #endif
                .GetInterfaces()
                .Select(i => new TypeIntrospector(i))
                .ToArray();
        }

        /// <inheritdoc />
        public TypeCode TypeCode => Type.GetTypeCode(_introspectedType);

        /// <inheritdoc />
        public Type IntrospectedType => _introspectedType;

        /// <inheritdoc />
        public TypeFlags TypeFlags => _flags;


        #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <inheritdoc />
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public AccessModifier AccessModifier { get; }
        #endif



        
        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <inheritdoc />
        public bool IsDelegate => _flags.IsDelegate();
        #endif
        
        #if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
        /// <inheritdoc />
        public bool IsGenericType => _flags.IsGeneric();
        #endif

        #if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
        /// <inheritdoc />
        public bool IsGenericTypeDefinition => _flags.IsGenericDefinition();
        #endif

        #if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
        /// <inheritdoc />
        public bool IsNullableType => _flags.IsNullableValueType();
        #endif

        /// <inheritdoc />
        public bool IsEnum => _flags.IsEnum();

        /// <inheritdoc />
        public bool IsAbstract => _flags.IsAbstract();
    }
}
#endif
#endif