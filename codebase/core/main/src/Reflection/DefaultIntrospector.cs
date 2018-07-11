#if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
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
    public class DefaultIntrospector : IIntrospector
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _introspectedType;

        public DefaultIntrospector(Type introspectedType)
        {
            _introspectedType = introspectedType.VerifyArgument(nameof(introspectedType)).IsNotNull();
        }
        #if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
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

            if ((options & ScanOptions.Public) != ScanOptions.Public && member.AccessModifier == AccessModifier.Public)
            {
                return false;
            }
            if ((options & ScanOptions.NonPublic) != ScanOptions.NonPublic && (member.AccessModifier == AccessModifier.Internal || member.AccessModifier == AccessModifier.Private || member.AccessModifier == AccessModifier.Protected || member.AccessModifier == AccessModifier.ProtectedInternal))
            {
                return false;
            }

            return true;
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
            #if !NETSTANDARD
            var type = member.DeclaringType;
            if (type != member.ReflectedType && !(
                type.IsSubclassOf(member.ReflectedType) || member.ReflectedType.IsAssignableFrom(type)))
            {
				throw new ArgumentException(string.Format("Expression '{0}' refers to a property that is not from type {1}.", expression, type), nameof(expression));
            }
            #endif

            return member;
        }

        /// <inheritdoc />
        public IAttributeInfo[] GetAttributes()
        {
            #if NETSTANDARD || NET45_OR_NEWER
            return IntrospectedType.GetTypeInfo().GetEffectiveAttributes();
            #else
            return IntrospectedType.GetEffectiveAttributes();
            #endif
        }
        public IAttributeInfo[] GetAttributes(Type attributeType)
        {
            #if NETSTANDARD || NET45_OR_NEWER
            return IntrospectedType.GetTypeInfo().GetEffectiveAttributes(attributeType);
            #else
            return IntrospectedType.GetEffectiveAttributes(attributeType);
            #endif
        }

        /// <inheritdoc />
        public IConstructor GetConstructor(ScanOptions scanOptions, params Type[] argumentTypes)
        {
            #if !NETSTANDARD
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
            #elif !NETSTANDARD
            var constructors = _introspectedType.GetConstructors(bindingFlags);
            #endif
            return constructors.Select<ConstructorInfo, IConstructor>(x => new ConstructorToken(x)).ToArray();
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
            return property != null ? new PropertyToken(property) : null;
        }

        /// <inheritdoc />
        public IProperty GetProperty(Expression<Func<object>> expression)
        {
            if (ExtractMember(expression) is PropertyInfo prop)
            {
                return new PropertyToken(prop);
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
            return properties.Select(x => new PropertyToken(x)).Cast<IProperty>().ToArray();
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
        public IField GetField(Expression<Func<object>> expression)
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
            return fields.Select<FieldInfo, IField>(x => new FieldToken(x)).ToArray();
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
        public IEvent GetEvent(Expression<Func<object>> expression)
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
        public Type IntrospectedType => _introspectedType;
    }

    public sealed class DefaultIntrospector<T> : DefaultIntrospector, IIntrospector<T>
    {
        public DefaultIntrospector() : base(typeof(T)) { }

        public IProperty GetProperty(Expression<Func<T, object>> expression)
        {
            if (ExtractMember(expression) is PropertyInfo prop)
            {
                return new PropertyToken(prop);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        public IField GetField(Expression<Func<T, object>> expression)
        {
            if (ExtractMember(expression) is FieldInfo field)
            {
                return new FieldToken(field);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid field expression.", expression), nameof(expression));
        }

        public IEvent GetEvent(Expression<Func<T, object>> expression)
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
