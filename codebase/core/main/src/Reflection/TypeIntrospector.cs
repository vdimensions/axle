#if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System;
using System.Linq;
using System.Reflection;

using Axle.Reflection.Extensions;

namespace Axle.Reflection
{
    /// <summary>
    /// The default implementation of the <see cref="ITypeIntrospector"/> interface.
    /// </summary>
    public class TypeIntrospector : AbstractTypeIntrospector
    {
        private static bool MatchesSignature(MethodBase mb, Type[] types)
        {
            var parameters = mb.GetParameters();
            if (types.Length != parameters.Length)
            {
                return false;
            }

            for (var i = 0; i < types.Length; ++i)
            {
                if (parameters[i].ParameterType != types[i])
                {
                    return false;
                }
            }

            return true;
        }
        
        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static BindingFlags MemberScanOptionsToBindingFlags(ScanOptions scanOptions)
        {
            var flags = BindingFlags.Default;

            if ((scanOptions & ScanOptions.Static) == ScanOptions.Static)
            {
                flags |= BindingFlags.Static;
            }
            if ((scanOptions & ScanOptions.Instance) == ScanOptions.Instance)
            {
                flags |= BindingFlags.Instance;
            }
            if ((scanOptions & ScanOptions.Public) == ScanOptions.Public)
            {
                flags |= BindingFlags.Public;
            }
            if ((scanOptions & ScanOptions.NonPublic) == ScanOptions.NonPublic)
            {
                flags |= BindingFlags.NonPublic;
            }

            return flags;
        }
        #endif

        #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
        private static bool MatchesScanOptions(IMember member, ScanOptions options)
        {
            if ((options & ScanOptions.Static) != ScanOptions.Static && member.Declaration == DeclarationType.Static)
            {
                return false;
            }
            if ((options & ScanOptions.Instance) != ScanOptions.Instance && member.Declaration == DeclarationType.Instance)
            {
                return false;
            }
            
            switch (member.AccessModifier)
            {
                case AccessModifier.Internal:
                case AccessModifier.Protected:
                case AccessModifier.ProtectedInternal:
                case AccessModifier.Private:
                case AccessModifier.PrivateProtected:
                    return (options & ScanOptions.NonPublic) == ScanOptions.NonPublic;
                case AccessModifier.Public:
                    return (options & ScanOptions.Public) == ScanOptions.Public;
                default: 
                    return false;
            }
        }
        #endif

        /// <summary>
        /// Creates a new instance of the <see cref="TypeIntrospector" /> class.
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type"/> to provide reflected information for by the current <see cref="TypeIntrospector"/>
        /// instance.
        /// </param>
        public TypeIntrospector(Type type) : base(type) { }

        /// <inheritdoc cref="GetAttributes()"/>
        public sealed override IAttributeInfo[] GetAttributes()
        {
            #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
            return IntrospectedType.GetTypeInfo().GetEffectiveAttributes();
            #else
            return CustomAttributeProviderExtensions.GetEffectiveAttributes(IntrospectedType);
            #endif
        }
        /// <inheritdoc />
        public sealed override IAttributeInfo[] GetAttributes(Type attributeType)
        {
            #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
            return IntrospectedType.GetTypeInfo().GetEffectiveAttributes(attributeType);
            #else
            return CustomAttributeProviderExtensions.GetEffectiveAttributes(IntrospectedType, attributeType);
            #endif
        }
        /// <inheritdoc />
        public sealed override IAttributeInfo[] GetAttributes(Type attributeType, bool inherit)
        {
            #if NETSTANDARD || NET45_OR_NEWER
            var attrs = IntrospectedType.GetTypeInfo().GetCustomAttributes(attributeType, inherit).Cast<Attribute>();
            #else
            var attrs = IntrospectedType.GetCustomAttributes(attributeType, inherit).Cast<Attribute>();
            #endif
            var empty = new Attribute[0];
            return inherit
                ? CustomAttributeProviderExtensions.FilterAttributes(empty, attrs) 
                : CustomAttributeProviderExtensions.FilterAttributes(attrs, empty);
        }

        /// <inheritdoc />
        public sealed override IConstructor GetConstructor(ScanOptions scanOptions, params Type[] argumentTypes)
        {
            var introspectedType = IntrospectedType;
            #if NETFRAMEWORK
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var constructor = introspectedType.GetConstructor(argumentTypes)
                           ?? introspectedType.GetConstructor(bindingFlags, null, argumentTypes, new ParameterModifier[0]);
            return constructor != null ? new ConstructorToken(constructor) : null;
            #elif NETSTANDARD1_5_OR_NEWER || UNITY_2018_1_OR_NEWER
            var constructor = introspectedType.GetTypeInfo().GetConstructor(argumentTypes);
            var result = new ConstructorToken(constructor);
            return MatchesScanOptions(result, scanOptions) ? result : null;
            #elif NETSTANDARD1_3_OR_NEWER 
            var result = introspectedType.GetTypeInfo().DeclaredConstructors
                .Where(x => MatchesSignature(x, argumentTypes))
                .Select(x => new ConstructorToken(x))
                .SingleOrDefault();
            return result != null && MatchesScanOptions(result, scanOptions) ? result : null;
            #endif
        }

        /// <inheritdoc />
        public override IConstructor GetConstructor(ConstructorInfo reflectedConstructor) 
            => new ConstructorToken(reflectedConstructor);

        /// <inheritdoc />
        public sealed override IConstructor[] GetConstructors(ScanOptions scanOptions)
        {
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var constructors = IntrospectedType.GetTypeInfo().GetConstructors(bindingFlags);
            #elif NETFRAMEWORK
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var constructors = IntrospectedType.GetConstructors(bindingFlags);
            #endif
            return Enumerable.ToArray(Enumerable.Select(constructors, GetConstructor));
        }

        /// <inheritdoc />
        public sealed override IMethod GetMethod(ScanOptions scanOptions, string methodName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
            var method = IntrospectedType.GetTypeInfo().GetMethod(methodName, bindingFlags);
            #else
            var method = IntrospectedType.GetMethod(methodName, bindingFlags);
            #endif
            return method != null ? GetMethod(method) : null;
        }
        /// <inheritdoc />
        public override IMethod GetMethod(MethodInfo reflectedMethod) => new MethodToken(reflectedMethod);

        /// <inheritdoc />
        public sealed override IMethod[] GetMethods(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
            var methods = IntrospectedType.GetTypeInfo().GetMethods(bindingFlags);
            #else
            var methods = IntrospectedType.GetMethods(bindingFlags);
            #endif
            return methods.Select(GetMethod).ToArray();
        }

        /// <inheritdoc />
        public sealed override IProperty GetProperty(ScanOptions scanOptions, string propertyName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
            var property = IntrospectedType.GetTypeInfo().GetProperty(propertyName, bindingFlags);
            #else
            var property = IntrospectedType.GetProperty(propertyName, bindingFlags);
            #endif
            return property != null ? GetProperty(property) : null;
        }

        /// <inheritdoc />
        public override IProperty GetProperty(PropertyInfo reflectedProperty) => PropertyToken.Create(reflectedProperty);

        /// <inheritdoc />
        public sealed override IProperty[] GetProperties(ScanOptions scanOptions)
        {
            var introspectedType = IntrospectedType;
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
            var properties = introspectedType.GetTypeInfo()
            #else
            var properties = introspectedType
            #endif
                .GetProperties(bindingFlags)
                .GroupBy(x => x.Name)
                .SelectMany(x => x.Count() == 1 ? x : x.Where(y => y.DeclaringType == introspectedType));
            return properties.Select(GetProperty).ToArray();
        }

        /// <inheritdoc />
        public sealed override IField GetField(ScanOptions scanOptions, string fieldName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
            var field = IntrospectedType.GetTypeInfo().GetField(fieldName, bindingFlags);
            #else
            var field = IntrospectedType.GetField(fieldName, bindingFlags);
            #endif
            return field != null ? GetField(field) : null;
        }

        /// <inheritdoc />
        public override IField GetField(FieldInfo reflectedField) => new FieldToken(reflectedField);

        /// <inheritdoc />
        public sealed override IField[] GetFields(ScanOptions scanOptions)
        {
            var introspectedType = IntrospectedType;
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
            var fields = introspectedType.GetTypeInfo()
            #else
            var fields = introspectedType
            #endif
                .GetFields(bindingFlags)
                .GroupBy(x => x.Name)
                .SelectMany(x => x.Count() == 1 ? x : x.Where(y => y.DeclaringType == introspectedType));
            return Enumerable.ToArray(Enumerable.Select<FieldInfo, IField>(fields, x => new FieldToken(x)));
        }

        /// <inheritdoc />
        public sealed override IEvent GetEvent(ScanOptions scanOptions, string eventName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
            var @event = IntrospectedType.GetTypeInfo().GetEvent(eventName, bindingFlags);
            #else
            var @event = IntrospectedType.GetEvent(eventName, bindingFlags);
            #endif
            return @event != null ? new EventToken(@event) : null;
        }

        /// <inheritdoc />
        public override IEvent GetEvent(EventInfo reflectedEvent) => new EventToken(reflectedEvent);

        /// <inheritdoc />
        public sealed override IEvent[] GetEvents(ScanOptions scanOptions)
        {
            var introspectedType = IntrospectedType;
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var events = introspectedType
                #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
                .GetTypeInfo()
                #endif
                .GetEvents(bindingFlags)
                .GroupBy(x => x.Name)
                .SelectMany(x => x.Count() == 1 ? x : x.Where(y => y.DeclaringType == introspectedType));
            return events.Select<EventInfo, IEvent>(x => new EventToken(x)).ToArray();
        }

        /// <inheritdoc />
        public sealed override bool IsAttributeDefined(Type attributeType, bool inherit)
        {
            #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
            return IntrospectedType.GetTypeInfo().IsDefined(attributeType, inherit);
            #else
            return IntrospectedType.IsDefined(attributeType, inherit);
            #endif
        }
    }

    /// <summary>
    /// The default implementation of the <see cref="ITypeIntrospector{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">
    /// The <see cref="Type"/> to provide reflected information for by the current <see cref="TypeIntrospector{T}"/>
    /// </typeparam>
    public class TypeIntrospector<T> : TypeIntrospector, ITypeIntrospector<T>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TypeIntrospector{T}" /> class.
        /// </summary>
        public TypeIntrospector() : base(typeof(T)) { }
    }
}
#endif
#endif