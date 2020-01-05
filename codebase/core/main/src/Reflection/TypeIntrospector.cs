#if NETSTANDARD || NET35_OR_NEWER
#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using Axle.Reflection.Extensions;
using Axle.Verification;


namespace Axle.Reflection
{
    /// <summary>
    /// The default implementation of the <see cref="ITypeIntrospector"/> interface.
    /// </summary>
    public class TypeIntrospector : ITypeIntrospector
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _introspectedType;

        /// <summary>
        /// Creates a new instance of the <see cref="TypeIntrospector" /> class.
        /// </summary>
        /// <param name="introspectedType">
        /// The <see cref="Type"/> to provide reflected information for by the current <see cref="TypeIntrospector"/>
        /// instance.
        /// </param>
        public TypeIntrospector(Type introspectedType)
        {
            _introspectedType = Verifier.IsNotNull(Verifier.VerifyArgument(introspectedType, nameof(introspectedType)));
        }
        
        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        #if NETSTANDARD || NET45_OR_NEWER
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

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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

        protected static MemberInfo ExtractMember<T>(Expression<T> expression)
        {
            var expr = expression.Body as MemberExpression;
            if (expr == null)
            {
                if (expression.Body is UnaryExpression unary)
                {
                    expr = unary.Operand as MemberExpression;
                    if (expr == null)
                    {
                        if (unary.NodeType == ExpressionType.Convert)
                        {
                            unary = (UnaryExpression) unary.Operand;
                        }
                        if (unary.NodeType == ExpressionType.ArrayLength)
                        {
                            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER
                            var m = unary.Operand.Type.GetTypeInfo().GetMember(nameof(Array.Length), BindingFlags.Instance | BindingFlags.Public);
                            #else
                            var m = unary.Operand.Type.GetMember(nameof(Array.Length), BindingFlags.Instance | BindingFlags.Public);
                            #endif
                            return m[0];
                        }
                        expr = unary.Operand as MemberExpression;
                    }
                }
            }

            if (expr == null)
            {
                throw new ArgumentException(string.Format("The provided expression {0} is not a valid member expression.", expression), nameof(expression));
            }

            var member = expr.Member;
            #if NETFRAMEWORK
            var type = member.DeclaringType;
            if (type != null && type != member.ReflectedType && null != member.ReflectedType && !(
                type.IsSubclassOf(member.ReflectedType) || member.ReflectedType.IsAssignableFrom(type)))
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a property that is not from type {1}.", expression, type), nameof(expression));
            }
            #endif

            return member;
        }

        /// <inheritdoc cref="GetAttributes()"/>
        public IAttributeInfo[] GetAttributes()
        {
            #if NETSTANDARD || NET45_OR_NEWER
            return IntrospectedType.GetTypeInfo().GetEffectiveAttributes();
            #else
            return IntrospectedType.GetEffectiveAttributes();
            #endif
        }
        /// <inheritdoc />
        public IAttributeInfo[] GetAttributes(Type attributeType)
        {
            #if NETSTANDARD || NET45_OR_NEWER
            return IntrospectedType.GetTypeInfo().GetEffectiveAttributes(attributeType);
            #else
            return IntrospectedType.GetEffectiveAttributes(attributeType);
            #endif
        }
        /// <inheritdoc />
        public IAttributeInfo[] GetAttributes(Type attributeType, bool inherit)
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
        public IConstructor GetConstructor(ScanOptions scanOptions, params Type[] argumentTypes)
        {
            #if NETFRAMEWORK
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var constructor = _introspectedType.GetConstructor(argumentTypes)
                           ?? _introspectedType.GetConstructor(bindingFlags, null, argumentTypes, new ParameterModifier[0]);
            return constructor != null ? new ConstructorToken(constructor) : null;
            #elif NETSTANDARD1_5_OR_NEWER
            var constructor = _introspectedType.GetTypeInfo().GetConstructor(argumentTypes);
            var result = new ConstructorToken(constructor);
            return MatchesScanOptions(result, scanOptions) ? result : null;
            #endif
        }

        /// <inheritdoc />
        public IConstructor[] GetConstructors(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER
            var constructors = _introspectedType.GetTypeInfo().GetConstructors(bindingFlags);
            #elif NETFRAMEWORK
            var constructors = _introspectedType.GetConstructors(bindingFlags);
            #endif
            return Enumerable.ToArray(Enumerable.Select<ConstructorInfo, IConstructor>(constructors, x => new ConstructorToken(x)));
        }

        /// <inheritdoc />
        public IMethod GetMethod(ScanOptions scanOptions, string methodName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER
            var method = _introspectedType.GetTypeInfo().GetMethod(methodName, bindingFlags);
            #else
            var method = _introspectedType.GetMethod(methodName, bindingFlags);
            #endif
            return method != null ? new MethodToken(method) : null;
        }

        /// <inheritdoc />
        public IMethod[] GetMethods(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER
            var methods = _introspectedType.GetTypeInfo().GetMethods(bindingFlags);
            #else
            var methods = _introspectedType.GetMethods(bindingFlags);
            #endif
            return methods.Select<MethodInfo, IMethod>(x => new MethodToken(x)).ToArray();
        }

        /// <inheritdoc />
        public IProperty GetProperty(ScanOptions scanOptions, string propertyName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER
            var property = _introspectedType.GetTypeInfo().GetProperty(propertyName, bindingFlags);
            #else
            var property = _introspectedType.GetProperty(propertyName, bindingFlags);
            #endif
            return property != null ? PropertyToken.Create(property) : null;
        }

        /// <inheritdoc />
        public IProperty GetProperty<TResult>(Expression<Func<TResult>> expression)
        {
            if (ExtractMember(expression) is PropertyInfo prop)
            {
                return PropertyToken.Create(prop);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        /// <inheritdoc />
        public IProperty[] GetProperties(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER
            var properties = _introspectedType.GetTypeInfo()
            #else
            var properties = _introspectedType
            #endif
                .GetProperties(bindingFlags)
                .GroupBy(x => x.Name)
                .SelectMany(x => x.Count() == 1 ? x : x.Where(y => y.DeclaringType == _introspectedType));
            return properties.Select(PropertyToken.Create).Cast<IProperty>().ToArray();
        }

        /// <inheritdoc />
        public IField GetField(ScanOptions scanOptions, string fieldName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER
            var field = _introspectedType.GetTypeInfo().GetField(fieldName, bindingFlags);
            #else
            var field = _introspectedType.GetField(fieldName, bindingFlags);
            #endif
            return field != null ? new FieldToken(field) : null;
        }

        /// <inheritdoc />
        public IField GetField<TResult>(Expression<Func<TResult>> expression)
        {
            if (ExtractMember(expression) is FieldInfo field)
            {
                return new FieldToken(field);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        /// <inheritdoc />
        public IField[] GetFields(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER
            var fields = _introspectedType.GetTypeInfo()
            #else
            var fields = _introspectedType
            #endif
                .GetFields(bindingFlags)
                .GroupBy(x => x.Name)
                .SelectMany(x => x.Count() == 1 ? x : x.Where(y => y.DeclaringType == _introspectedType));
            return Enumerable.ToArray(Enumerable.Select<FieldInfo, IField>(fields, x => new FieldToken(x)));
        }

        /// <inheritdoc />
        public IEvent GetEvent(ScanOptions scanOptions, string eventName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER
            var @event = _introspectedType.GetTypeInfo().GetEvent(eventName, bindingFlags);
            #else
            var @event = _introspectedType.GetEvent(eventName, bindingFlags);
            #endif
            return @event != null ? new EventToken(@event) : null;
        }
        /// <inheritdoc />
        public IEvent GetEvent<TResult>(Expression<Func<TResult>> expression)
        {
            if (ExtractMember(expression) is EventInfo evt)
            {
                return new EventToken(evt);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        /// <inheritdoc />
        public IEvent[] GetEvents(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER
            var events = _introspectedType.GetTypeInfo()
            #else
            var events = _introspectedType
            #endif
                .GetEvents(bindingFlags)
                .GroupBy(x => x.Name)
                .SelectMany(x => x.Count() == 1 ? x : x.Where(y => y.DeclaringType == _introspectedType));
            return events.Select<EventInfo, IEvent>(x => new EventToken(x)).ToArray();
        }

        /// <inheritdoc />
        public IMember[] GetMembers(ScanOptions scanOptions)
        {
            var ctors = GetConstructors(scanOptions);
            var methods = GetMethods(scanOptions);
            var props = GetProperties(scanOptions);
            var fields = GetFields(scanOptions);
            var events = GetEvents(scanOptions);

            var result = new List<IMember>(ctors.Length + methods.Length + props.Length + fields.Length + events.Length);

            for (var i = 0; i < ctors.Length; i++)
            {
                result.Add(ctors[i]);
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
        public bool IsAttributeDefined(Type attributeType, bool inherit)
        {
            #if NETSTANDARD || NET45_OR_NEWER
            return IntrospectedType.GetTypeInfo().IsDefined(attributeType, inherit);
            #else
            return IntrospectedType.IsDefined(attributeType, inherit);
            #endif
        }

        /// <inheritdoc />
        public Type IntrospectedType => _introspectedType;
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

        /// <inheritdoc />
        public IProperty GetProperty<TResult>(Expression<Func<T, TResult>> expression)
        {
            if (ExtractMember(expression) is PropertyInfo prop)
            {
                return PropertyToken.Create(prop);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        /// <inheritdoc />
        public IField GetField<TResult>(Expression<Func<T, TResult>> expression)
        {
            if (ExtractMember(expression) is FieldInfo field)
            {
                return new FieldToken(field);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid field expression.", expression), nameof(expression));
        }

        /// <inheritdoc />
        public IEvent GetEvent<TResult>(Expression<Func<T, TResult>> expression)
        {
            if (ExtractMember(expression) is EventInfo evt)
            {
                return new EventToken(evt);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid event expression.", expression), nameof(expression));
        }
    }
}
#endif
#endif