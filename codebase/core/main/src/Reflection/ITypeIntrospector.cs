using System;
using System.Reflection;
#if NETSTANDARD || NET35_OR_NEWER
using System.Linq.Expressions;
#endif


namespace Axle.Reflection
{
    /// <summary>
    /// An interface for a type introspector; that is, an utility to provide reflection information on members of a 
    /// given type.
    /// </summary>
    public interface ITypeIntrospector : IAttributeTarget
    {
        /// <summary>
        /// Looks up a <see cref="IConstructor"/> for the <see cref="IntrospectedType">introspected type</see>
        /// that matches the provided <paramref name="scanOptions"/> and a signature conforming to the specified <paramref name="argumentTypes"/>.
        /// </summary>
        /// <param name="scanOptions">
        /// One of the <see cref="ScanOptions"/> values, representing the reflection lookup flags.
        /// </param>
        /// <param name="argumentTypes">
        /// An array of types to match the signature of the constructor being looked up.
        /// </param>
        /// <returns>
        /// An instance of <see cref="IConstructor"/> representing the reflected constructor, or <c>null</c> if the lookup did not yield any results.
        /// </returns>
        IConstructor GetConstructor(ScanOptions scanOptions, params Type[] argumentTypes);
        IConstructor GetConstructor(ConstructorInfo reflectedConstructor);

        /// <summary>
        /// Looks up a collection of <see cref="IConstructor">constructors</see> for the <see cref="IntrospectedType">introspected type</see>
        /// that match the provided <paramref name="scanOptions"/>.
        /// </summary>
        /// <param name="scanOptions">
        /// One of the <see cref="ScanOptions"/> values, representing the reflection lookup flags.
        /// </param>
        /// <returns>
        /// An array of <see cref="IConstructor"/> representing the reflected constructors, or an empty array if the lookup did not yield any results.
        /// </returns>
        IConstructor[] GetConstructors(ScanOptions scanOptions);

        IMethod GetMethod(ScanOptions scanOptions, string methodName);
        IMethod GetMethod(MethodInfo reflectedMethod);

        IMethod[] GetMethods(ScanOptions scanOptions);

        IProperty GetProperty(ScanOptions scanOptions, string propertyName);
        #if NETSTANDARD || NET35_OR_NEWER
        IProperty GetProperty<TResult>(Expression<Func<TResult>> expression);
        #endif
        IProperty GetProperty(PropertyInfo reflectedProperty);

        IProperty[] GetProperties(ScanOptions scanOptions);

        IField GetField(ScanOptions scanOptions, string fieldName);
        #if NETSTANDARD || NET35_OR_NEWER
        IField GetField<TResult>(Expression<Func<TResult>> expression);
        #endif
        IField GetField(FieldInfo reflectedField);

        IField[] GetFields(ScanOptions scanOptions);

        IEvent GetEvent(ScanOptions scanOptions, string eventName);
        #if NETSTANDARD || NET35_OR_NEWER
        IEvent GetEvent<TResult>(Expression<Func<TResult>> expression);
        #endif
        IEvent GetEvent(EventInfo reflectedEvent);

        IEvent[] GetEvents(ScanOptions scanOptions);

        IMember[] GetMembers(ScanOptions scanOptions);

        /// <summary>
        /// Gets a <see cref="IGenericTypeIntrospector"/> for accessing the type's generic definition,
        /// if the <see cref="IntrospectedType"/> is a generic type, or <c>null</c> otherwise.
        /// </summary>
        /// <returns>
        /// A <see cref="IGenericTypeIntrospector"/> if the <see cref="IntrospectedType"/> is a generic type;
        /// otherwise <c>null</c>
        /// </returns>
        /// <seealso cref="IsGenericType"/>
        /// <seealso cref="IGenericTypeIntrospector"/>
        IGenericTypeIntrospector GetGenericTypeDefinition();

        /// <summary>
        /// The <see cref="Type"/> which the current <see cref="IIntrospector"/> instance provides reflected information for.
        /// </summary>
        Type IntrospectedType { get; }

        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        /// <summary>
        /// Gets the underlying <see cref="TypeCode"/> for the <see cref="IntrospectedType"/>.
        /// </summary>
        TypeCode TypeCode { get; }
        #endif
        
        /// <summary>
        /// Checks whether the specified <see cref="IntrospectedType"/> is a generic type.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> is a generic type; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        bool IsGenericType { get; }

        /// <summary>
        /// Determines if the <see cref="IntrospectedType"/> is a delegate.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the provided <see cref="IntrospectedType"/> is a delegate;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        bool IsDelegate { get; }

        /// <summary>
        /// Checks whether the specified <see cref="IntrospectedType"/> is a nullable type.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> is a nullable type; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        bool IsNullableType { get; }

        /// <summary>
        /// Checks whether the specified <see cref="IntrospectedType"/> is abstract.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> is abstract; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        bool IsAbstract { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IntrospectedType"/> represents an enumeration.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> represents an enumeration; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        bool IsEnum { get; }
    }

    public interface ITypeIntrospector<T> : ITypeIntrospector
    {
        #if NETSTANDARD || NET35_OR_NEWER
        IProperty GetProperty<TResult>(Expression<Func<T, TResult>> expression);

        IField GetField<TResult>(Expression<Func<T, TResult>> expression);

        IEvent GetEvent<TResult>(Expression<Func<T, TResult>> expression);
        #endif
    }
}
