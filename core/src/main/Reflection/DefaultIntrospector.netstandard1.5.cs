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
                            var m = unary.Operand.Type.GetMember("Length", BindingFlags.Instance | BindingFlags.Public);
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
            if (type != member.ReflectedType && !(
                type.IsSubclassOf(member.ReflectedType) || member.ReflectedType.IsAssignableFrom(type)))
            {
				throw new ArgumentException(string.Format("Expresion '{0}' refers to a property that is not from type {1}.", expression, type), nameof(expression));
            }

            return member;
        }

        public IConstructor GetConstructor(ScanOptions scanOptions, params Type[] argumentTypes)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var constructor = _introspectedType.GetConstructor(bindingFlags, null, argumentTypes, new ParameterModifier[0]);
            return new ConstructorToken(constructor);
        }

        public IEnumerable<IConstructor> GetConstructors(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var constructors = _introspectedType.GetConstructors(bindingFlags);
            return constructors.Select<ConstructorInfo, IConstructor>(x => new ConstructorToken(x)).ToArray();
        }

        public IMethod GetMethod(ScanOptions scanOptions, string methodName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var method = _introspectedType.GetMethod(methodName, bindingFlags);
            return new MethodToken(method);
        }

        public IEnumerable<IMethod> GetMethods(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var methods = _introspectedType.GetMethods(bindingFlags);
            return methods.Select<MethodInfo, IMethod>(x => new MethodToken(x)).ToArray();
        }

        public IProperty GetProperty(ScanOptions scanOptions, string propertyName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var property = _introspectedType.GetProperty(propertyName, bindingFlags);
            return new PropertyToken(property);
        }
        public IProperty GetProperty(Expression<Func<object>> expression)
        {
            var prop = ExtractMember(expression) as PropertyInfo;
            if (prop != null)
            {
                return new PropertyToken(prop);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }


        public IEnumerable<IProperty> GetProperties(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var properties = _introspectedType.GetProperties(bindingFlags)
                .GroupBy(x => x.Name)
                .SelectMany(x => x.Count() == 1 ? x : x.Where(y => y.DeclaringType == this._introspectedType));
            return properties.Select(x => new PropertyToken(x)).Cast<IProperty>().ToArray();
        }

        public IField GetField(ScanOptions scanOptions, string fieldName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var field = _introspectedType.GetField(fieldName, bindingFlags);
            return new FieldToken(field);
        }
        public IField GetField(Expression<Func<object>> expression)
        {
            var field = ExtractMember(expression) as FieldInfo;
            if (field != null)
            {
                return new FieldToken(field);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        public IEnumerable<IField> GetFields(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var fields = _introspectedType.GetFields(bindingFlags)
                .GroupBy(x => x.Name)
                .SelectMany(x => x.Count() == 1 ? x : x.Where(y => y.DeclaringType == this._introspectedType));
            return fields.Select<FieldInfo, IField>(x => new FieldToken(x)).ToArray();
        }

        public IEvent GetEvent(ScanOptions scanOptions, string eventName)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var @event = _introspectedType.GetEvent(eventName, bindingFlags);
            return new EventToken(@event);
        }
        public IEvent GetEvent(Expression<Func<object>> expression)
        {
            var evt = ExtractMember(expression) as EventInfo;
            if (evt != null)
            {
                return new EventToken(evt);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        public IEnumerable<IEvent> GetEvents(ScanOptions scanOptions)
        {
            var bindingFlags = MemberScanOptionsToBindingFlags(scanOptions);
            var events = _introspectedType.GetEvents(bindingFlags)
                .GroupBy(x => x.Name)
                .SelectMany(x => x.Count() == 1 ? x : x.Where(y => y.DeclaringType == _introspectedType));
            return events.Select<EventInfo, IEvent>(x => new EventToken(x)).ToArray();
        }

        public IEnumerable<IMember> GetMembers(ScanOptions scanOptions)
        {
            var ctors = GetConstructors(scanOptions).Cast<IMember>();
            var methods = GetMethods(scanOptions).Cast<IMember>();
            var props = GetProperties(scanOptions).Cast<IMember>();
            var fileds = GetFields(scanOptions).Cast<IMember>();
            var events = GetEvents(scanOptions).Cast<IMember>();
            return ctors.Union(methods).Union(props).Union(fileds).Union(events);
        }

        public Type IntrospectedType => _introspectedType;
    }

    public sealed class DefaultIntrospector<T> : DefaultIntrospector, IIntrospector<T>
    {
        public DefaultIntrospector() : base(typeof(T)) { }

        public IProperty GetProperty(Expression<Func<T, object>> expression)
        {
            var prop = ExtractMember(expression) as PropertyInfo;
            if (prop != null)
            {
                return new PropertyToken(prop);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        public IField GetField(Expression<Func<T, object>> expression)
        {
            var field = ExtractMember(expression) as FieldInfo;
            if (field != null)
            {
                return new FieldToken(field);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }


        public IEvent GetEvent(Expression<Func<T, object>> expression)
        {
            var evt = ExtractMember(expression) as EventInfo;
            if (evt != null)
            {
                return new EventToken(evt);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }
    }
}
