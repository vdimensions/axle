using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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

        private static bool MatchesScanOptions(IMember member, ScanOptions options)
        {
            if ((options & ScanOptions.Instance) == ScanOptions.Instance && member.Declaration == DeclarationType.Static)
            {
                return false;
            }
            if ((options & ScanOptions.Static) == ScanOptions.Instance && member.Declaration == DeclarationType.Instance)
            {
                return false;
            }

            if ((options & ScanOptions.Public) == ScanOptions.Public && member.AccessModifier != AccessModifier.Public)
            {
                return false;
            }

            return true;
        }

        protected static MemberInfo ExtractMember<T>(Expression<T> expression)
        {
            var expr = expression.Body as MemberExpression;
            if (expr == null)
            {
                var unary = expression.Body as UnaryExpression;
                if (unary != null)
                {
                    expr = unary.Operand as MemberExpression;
                    if (expr == null)
                    {
                        if (unary.NodeType == ExpressionType.Convert)
                        {
                            unary = (UnaryExpression)unary.Operand;
                        }
                        if (unary.NodeType == ExpressionType.ArrayLength)
                        {
                            var m = unary.Operand.Type.GetTypeInfo().GetMember(nameof(Array.Length), BindingFlags.Instance | BindingFlags.Public);
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
            var type = member.DeclaringType;
            //if (type != member.ReflectedType && !(
            //    type.GetTypeInfo().IsSubclassOf(member.ReflectedType.GetTypeInfo()) || member.ReflectedType.IsAssignableFrom(type)))
            //{
			//	throw new ArgumentException(string.Format("Expresion '{0}' refers to a property that is not from type {1}.", expression, type), nameof(expression));
            //}

            return member;
        }

        /// <inheritdoc />
        public IConstructor GetConstructor(ScanOptions scanOptions, params Type[] argumentTypes)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var constructor = _introspectedType.GetTypeInfo().GetConstructor(argumentTypes);
            var result = new ConstructorToken(constructor);
            return MatchesScanOptions(result, scanOptions) ? result : null;
        }

        /// <inheritdoc />
        public IEnumerable<IConstructor> GetConstructors(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var constructors = _introspectedType.GetTypeInfo().GetConstructors(bindingFlags);
            return constructors.Select<ConstructorInfo, IConstructor>(x => new ConstructorToken(x)).ToArray();
        }

        /// <inheritdoc />
        public IMethod GetMethod(ScanOptions scanOptions, string methodName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var method = _introspectedType.GetTypeInfo().GetMethod(methodName, bindingFlags);
            return method == null ? null : new MethodToken(method);
        }

        /// <inheritdoc />
        public IEnumerable<IMethod> GetMethods(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var methods = _introspectedType.GetTypeInfo().GetMethods(bindingFlags);
            return methods.Select<MethodInfo, IMethod>(x => new MethodToken(x)).ToArray();
        }

        /// <inheritdoc />
        public IProperty GetProperty(ScanOptions scanOptions, string propertyName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var property = _introspectedType.GetTypeInfo().GetProperty(propertyName, bindingFlags);
            return property == null ? null : new PropertyToken(property);
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
        public IEnumerable<IProperty> GetProperties(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var properties = _introspectedType.GetTypeInfo().GetProperties(bindingFlags)
                .GroupBy(x => x.Name)
                .SelectMany(x => x.Count() == 1 ? x : x.Where(y => y.DeclaringType == _introspectedType));
            return properties.Select(x => new PropertyToken(x)).Cast<IProperty>().ToArray();
        }

        /// <inheritdoc />
        public IField GetField(ScanOptions scanOptions, string fieldName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var field = _introspectedType.GetTypeInfo().GetField(fieldName, bindingFlags);
            return field == null ? null : new FieldToken(field);
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
        public IEnumerable<IField> GetFields(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var fields = _introspectedType.GetTypeInfo().GetFields(bindingFlags)
                .GroupBy(x => x.Name)
                .SelectMany(x => x.Count() == 1 ? x : x.Where(y => y.DeclaringType == _introspectedType));
            return fields.Select<FieldInfo, IField>(x => new FieldToken(x)).ToArray();
        }

        /// <inheritdoc />
        public IEvent GetEvent(ScanOptions scanOptions, string eventName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var @event = _introspectedType.GetTypeInfo().GetEvent(eventName, bindingFlags);
            return @event == null ? null : new EventToken(@event);
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
        public IEnumerable<IEvent> GetEvents(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var events = _introspectedType.GetTypeInfo().GetEvents(bindingFlags)
                .GroupBy(x => x.Name)
                .SelectMany(x => x.Count() == 1 ? x : x.Where(y => y.DeclaringType == _introspectedType));
            return events.Select<EventInfo, IEvent>(x => new EventToken(x)).ToArray();
        }

        /// <inheritdoc />
        public IEnumerable<IMember> GetMembers(ScanOptions scanOptions)
        {
            var ctors = GetConstructors(scanOptions).Cast<IMember>();
            var methods = GetMethods(scanOptions).Cast<IMember>();
            var props = GetProperties(scanOptions).Cast<IMember>();
            var fileds = GetFields(scanOptions).Cast<IMember>();
            var events = GetEvents(scanOptions).Cast<IMember>();
            return ctors.Union(methods).Union(props).Union(fileds).Union(events);
        }

        /// <inheritdoc />
        public Type IntrospectedType => _introspectedType;
    }

    public sealed class DefaultIntrospector<T> : DefaultIntrospector, IIntrospector<T>
    {
        public DefaultIntrospector() : base(typeof(T)) { }

        /// <inheritdoc />
        public IProperty GetProperty(Expression<Func<T, object>> expression)
        {
            if (ExtractMember(expression) is PropertyInfo prop)
            {
                return new PropertyToken(prop);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        /// <inheritdoc />
        public IField GetField(Expression<Func<T, object>> expression)
        {
            if (ExtractMember(expression) is FieldInfo field)
            {
                return new FieldToken(field);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        /// <inheritdoc />
        public IEvent GetEvent(Expression<Func<T, object>> expression)
        {
            if (ExtractMember(expression) is EventInfo evt)
            {
                return new EventToken(evt);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }
    }
}
