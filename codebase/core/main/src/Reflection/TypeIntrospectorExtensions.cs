#if NETSTANDARD || NET35_OR_NEWER
#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using System.Linq.Expressions;
using System.Reflection;
using Axle.Verification;

namespace Axle.Reflection
{
    public static class TypeIntrospectorExtensions
    {
        #if NETSTANDARD || NET35_OR_NEWER
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
        
        public static IProperty GetProperty<TResult>(this ITypeIntrospector typeIntrospector, Expression<Func<TResult>> expression)
        {
            typeIntrospector.VerifyArgument(nameof(typeIntrospector)).IsNotNull();

            if (ExtractMember(expression) is PropertyInfo prop)
            {
                return typeIntrospector.GetProperty(prop);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        public static IField GetField<TResult>(this ITypeIntrospector typeIntrospector, Expression<Func<TResult>> expression)
        {
            typeIntrospector.VerifyArgument(nameof(typeIntrospector)).IsNotNull();

            if (ExtractMember(expression) is FieldInfo field)
            {
                return typeIntrospector.GetField(field);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        public static IEvent GetEvent<TResult>(this ITypeIntrospector typeIntrospector, Expression<Func<TResult>> expression)
        {
            if (ExtractMember(expression) is EventInfo evt)
            {
                return typeIntrospector.GetEvent(evt);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }


        public static IProperty GetProperty<T, TResult>(
            this ITypeIntrospector<T> typeIntrospector, 
            Expression<Func<T, TResult>> expression)
        {
            typeIntrospector.VerifyArgument(nameof(typeIntrospector)).IsNotNull();

            if (ExtractMember(expression) is PropertyInfo prop)
            {
                return typeIntrospector.GetProperty(prop);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        public static IField GetField<T, TResult>(
            this ITypeIntrospector<T> typeIntrospector,
            Expression<Func<T, TResult>> expression)
        {
            typeIntrospector.VerifyArgument(nameof(typeIntrospector)).IsNotNull();

            if (ExtractMember(expression) is FieldInfo field)
            {
                return typeIntrospector.GetField(field);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid field expression.", expression), nameof(expression));
        }

        public static IEvent GetEvent<T, TResult>(
            this ITypeIntrospector<T> typeIntrospector, 
            Expression<Func<T, TResult>> expression)
        {
            typeIntrospector.VerifyArgument(nameof(typeIntrospector)).IsNotNull();

            if (ExtractMember(expression) is EventInfo evt)
            {
                return typeIntrospector.GetEvent(evt);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid event expression.", expression), nameof(expression));
        }
        #endif
    }
}
#endif
#endif