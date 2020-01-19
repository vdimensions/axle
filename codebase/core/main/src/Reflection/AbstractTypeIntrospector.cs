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
        #if NETSTANDARD2_0 || NETFRAMEWORK
        public bool IsEnum => _introspectedType.IsEnum;
        #else
        public bool IsEnum => _introspectedType.GetTypeInfo().IsEnum;
        #endif
        internal static MemberInfo ExtractMember<T>(Expression<T> expression)
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

        #if NETSTANDARD || NET35_OR_NEWER
        /// <inheritdoc />
        public IProperty GetProperty<TResult>(Expression<Func<TResult>> expression)
        {
            if (ExtractMember(expression) is PropertyInfo prop)
            {
                return GetProperty(prop);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }
        #endif

        /// <inheritdoc />
        public abstract IProperty[] GetProperties(ScanOptions scanOptions);

        /// <inheritdoc />
        public abstract IField GetField(ScanOptions scanOptions, string fieldName);

        #if NETSTANDARD || NET35_OR_NEWER
        /// <inheritdoc />
        public IField GetField<TResult>(Expression<Func<TResult>> expression)
        {
            if (ExtractMember(expression) is FieldInfo field)
            {
                return GetField(field);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }
        #endif
        
        /// <inheritdoc />
        public abstract IField GetField(FieldInfo reflectedField);

        /// <inheritdoc />
        public abstract IField[] GetFields(ScanOptions scanOptions);

        /// <inheritdoc />
        public abstract IEvent GetEvent(ScanOptions scanOptions, string eventName);

        /// <inheritdoc />
        public abstract IEvent GetEvent(EventInfo reflectedEvent);
        
        #if NETSTANDARD || NET35_OR_NEWER
        /// <inheritdoc />
        public  IEvent GetEvent<TResult>(Expression<Func<TResult>> expression)
        {
            if (ExtractMember(expression) is EventInfo evt)
            {
                return new EventToken(evt);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }
        #endif

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
            if (!IsGenericType)
            {
                return null;
            }
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            return new GenericTypeIntrospector(_introspectedType.GetGenericTypeDefinition());
            #else
            return new GenericTypeIntrospector(_introspectedType.GetTypeInfo().GetGenericTypeDefinition());
            #endif
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