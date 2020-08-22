#if NETSTANDARD1_5_OR_NEWER || NET35_OR_NEWER
using System;
using System.Linq.Expressions;
using System.Reflection;
using Axle.Verification;

namespace Axle.Reflection
{
    /// <summary>
    /// A static class providing extension methods to instance of the
    /// <see cref="ITypeIntrospector"/> and <see cref="ITypeIntrospector{T}"/> interfaces. 
    /// </summary>
    public static class TypeIntrospectorExtensions
    {
        #if NETSTANDARD || NET35_OR_NEWER
        private static MemberInfo ExtractMember<T>(Expression<T> expression)
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
                            var m = unary.Operand.Type
                                #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER
                                .GetTypeInfo()
                                #endif
                                .GetMember(nameof(Array.Length), BindingFlags.Instance | BindingFlags.Public);
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
        
        /// <summary>
        /// Gets <see cref="IProperty"/> instance representing the reflected property represented by the provided
        /// <paramref name="expression"/>. 
        /// </summary>
        /// <param name="typeIntrospector">
        /// The <see cref="ITypeIntrospector"/> instance responsible for extracting the reflected property. 
        /// </param>
        /// <param name="expression">
        /// An <see cref="Expression{TDelegate}"/> representing the property of interest.
        /// </param>
        /// <typeparam name="TResult">
        /// The member type of the property to be returned. This is inferred from the generic type used in the
        /// <paramref name="expression"/>, but the property's actual type can be another type from the inheritance
        /// hierarchy of <typeparamref name="TResult"/>.
        /// </typeparam>
        /// <returns>
        /// <see cref="IProperty"/> instance representing the reflected property represented by the provided
        /// <paramref name="expression"/>. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="typeIntrospector"/> or <paramref name="expression"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="expression"/> cannot be resolved to a property.
        /// </exception>
        public static IProperty GetProperty<TResult>(
            this ITypeIntrospector typeIntrospector, 
            Expression<Func<TResult>> expression)
        {
            typeIntrospector.VerifyArgument(nameof(typeIntrospector)).IsNotNull();
            expression.VerifyArgument(nameof(expression)).IsNotNull();

            if (ExtractMember(expression) is PropertyInfo prop)
            {
                return typeIntrospector.GetProperty(prop);
            }
            throw new ArgumentException(
                string.Format("Argument {0} is not a valid property expression.", expression), 
                nameof(expression));
        }

        /// <summary>
        /// Gets <see cref="IField"/> instance representing the reflected field represented by the provided
        /// <paramref name="expression"/>. 
        /// </summary>
        /// <param name="typeIntrospector">
        /// The <see cref="ITypeIntrospector"/> instance responsible for extracting the reflected field. 
        /// </param>
        /// <param name="expression">
        /// An <see cref="Expression{TDelegate}"/> representing the field of interest.
        /// </param>
        /// <typeparam name="TResult">
        /// The member type of the field to be returned. This is inferred from the generic type used in the
        /// <paramref name="expression"/>, but the field's actual type can be another type from the inheritance
        /// hierarchy of <typeparamref name="TResult"/>.
        /// </typeparam>
        /// <returns>
        /// <see cref="IField"/> instance representing the reflected field represented by the provided
        /// <paramref name="expression"/>. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="typeIntrospector"/> or <paramref name="expression"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="expression"/> cannot be resolved to a field.
        /// </exception>
        public static IField GetField<TResult>(
            this ITypeIntrospector typeIntrospector, 
            Expression<Func<TResult>> expression)
        {
            typeIntrospector.VerifyArgument(nameof(typeIntrospector)).IsNotNull();
            expression.VerifyArgument(nameof(expression)).IsNotNull();

            if (ExtractMember(expression) is FieldInfo field)
            {
                return typeIntrospector.GetField(field);
            }
            throw new ArgumentException(
                string.Format("Argument {0} is not a valid property expression.", expression), 
                nameof(expression));
        }

        /// <summary>
        /// Gets <see cref="IEvent"/> instance representing the reflected event represented by the provided
        /// <paramref name="expression"/>. 
        /// </summary>
        /// <param name="typeIntrospector">
        /// The <see cref="ITypeIntrospector"/> instance responsible for extracting the reflected event. 
        /// </param>
        /// <param name="expression">
        /// An <see cref="Expression{TDelegate}"/> representing the event of interest.
        /// </param>
        /// <typeparam name="TResult">
        /// The member type of the event to be returned. This is inferred from the generic type used in the
        /// <paramref name="expression"/>, but the event's actual type can be another type from the inheritance
        /// hierarchy of <typeparamref name="TResult"/>.
        /// </typeparam>
        /// <returns>
        /// <see cref="IEvent"/> instance representing the reflected event represented by the provided
        /// <paramref name="expression"/>. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="typeIntrospector"/> or <paramref name="expression"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="expression"/> cannot be resolved to an event.
        /// </exception>
        public static IEvent GetEvent<TResult>(
            this ITypeIntrospector typeIntrospector, 
            Expression<Func<TResult>> expression)
        {
            if (ExtractMember(expression) is EventInfo evt)
            {
                return typeIntrospector.GetEvent(evt);
            }
            throw new ArgumentException(string.Format("Argument {0} is not a valid property expression.", expression), nameof(expression));
        }

        /// <summary>
        /// Gets <see cref="IProperty"/> instance representing the reflected property represented by the provided
        /// <paramref name="expression"/>. 
        /// </summary>
        /// <param name="typeIntrospector">
        /// The <see cref="ITypeIntrospector{T}"/> instance responsible for extracting the reflected property. 
        /// </param>
        /// <param name="expression">
        /// An <see cref="Expression{TDelegate}"/> representing the property of interest.
        /// </param>
        /// <typeparam name="T">
        /// The type that contains the reflected member.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The member type of the property to be returned. This is inferred from the generic type used in the
        /// <paramref name="expression"/>, but the property's actual type can be another type from the inheritance
        /// hierarchy of <typeparamref name="TResult"/>.
        /// </typeparam>
        /// <returns>
        /// <see cref="IProperty"/> instance representing the reflected field represented by the provided
        /// <paramref name="expression"/>. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="typeIntrospector"/> or <paramref name="expression"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="expression"/> cannot be resolved to an property.
        /// </exception>
        public static IProperty GetProperty<T, TResult>(
            this ITypeIntrospector<T> typeIntrospector, 
            Expression<Func<T, TResult>> expression)
        {
            typeIntrospector.VerifyArgument(nameof(typeIntrospector)).IsNotNull();

            if (ExtractMember(expression) is PropertyInfo prop)
            {
                return typeIntrospector.GetProperty(prop);
            }
            throw new ArgumentException(
                string.Format("Argument {0} is not a valid property expression.", expression), 
                nameof(expression));
        }

        /// <summary>
        /// Gets <see cref="IField"/> instance representing the reflected field represented by the provided
        /// <paramref name="expression"/>. 
        /// </summary>
        /// <param name="typeIntrospector">
        /// The <see cref="ITypeIntrospector{T}"/> instance responsible for extracting the reflected field. 
        /// </param>
        /// <param name="expression">
        /// An <see cref="Expression{TDelegate}"/> representing the field of interest.
        /// </param>
        /// <typeparam name="T">
        /// The type that contains the reflected member.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The member type of the field to be returned. This is inferred from the generic type used in the
        /// <paramref name="expression"/>, but the field's actual type can be another type from the inheritance
        /// hierarchy of <typeparamref name="TResult"/>.
        /// </typeparam>
        /// <returns>
        /// <see cref="IField"/> instance representing the reflected field represented by the provided
        /// <paramref name="expression"/>. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="typeIntrospector"/> or <paramref name="expression"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="expression"/> cannot be resolved to an field.
        /// </exception>
        public static IField GetField<T, TResult>(
            this ITypeIntrospector<T> typeIntrospector,
            Expression<Func<T, TResult>> expression)
        {
            typeIntrospector.VerifyArgument(nameof(typeIntrospector)).IsNotNull();

            if (ExtractMember(expression) is FieldInfo field)
            {
                return typeIntrospector.GetField(field);
            }
            throw new ArgumentException(
                string.Format("Argument {0} is not a valid field expression.", expression), 
                nameof(expression));
        }

        /// <summary>
        /// Gets <see cref="IEvent"/> instance representing the reflected event represented by the provided
        /// <paramref name="expression"/>. 
        /// </summary>
        /// <param name="typeIntrospector">
        /// The <see cref="ITypeIntrospector{T}"/> instance responsible for extracting the reflected event. 
        /// </param>
        /// <param name="expression">
        /// An <see cref="Expression{TDelegate}"/> representing the event of interest.
        /// </param>
        /// <typeparam name="T">
        /// The type that contains the reflected member.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The member type of the event to be returned. This is inferred from the generic type used in the
        /// <paramref name="expression"/>, but the event's actual type can be another type from the inheritance
        /// hierarchy of <typeparamref name="TResult"/>.
        /// </typeparam>
        /// <returns>
        /// <see cref="IEvent"/> instance representing the reflected event represented by the provided
        /// <paramref name="expression"/>. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="typeIntrospector"/> or <paramref name="expression"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="expression"/> cannot be resolved to an event.
        /// </exception>
        public static IEvent GetEvent<T, TResult>(
            this ITypeIntrospector<T> typeIntrospector, 
            Expression<Func<T, TResult>> expression)
        {
            typeIntrospector.VerifyArgument(nameof(typeIntrospector)).IsNotNull();

            if (ExtractMember(expression) is EventInfo evt)
            {
                return typeIntrospector.GetEvent(evt);
            }
            throw new ArgumentException(
                string.Format("Argument {0} is not a valid event expression.", expression), 
                nameof(expression));
        }
        #endif
    }
}
#endif
